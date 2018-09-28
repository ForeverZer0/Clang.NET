#region MIT License

// Copyright 2018 Eric Freed
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// Parser.cs created on 2018-09-23

#endregion

using System.Text;
using System.Text.RegularExpressions;

namespace LibClang
{
	public class Parser
	{
		protected ParserData Data;

		public Parser()
		{
			Data = new ParserData();
			CommandLineArgs = new string[0];
			SourceFiles = new string[0];
		}

		#region Properties & Indexers

		/// <summary>Gets or sets the command line arguments to be passed to the compiler.</summary>
		/// <value>The command line arguments.</value>
		public string[] CommandLineArgs { get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether comments will attempt to be parsed with each
		///     entity.
		/// </summary>
		/// <value><c>true</c> if comments will be included; otherwise, <c>false</c>.</value>
		public bool IncludeComments { get; set; } = true;

		/// <summary>Gets or sets the source files to be parsed.</summary>
		/// <value>The source files.</value>
		public string[] SourceFiles { get; set; }

		#endregion

		#region Methods

		/// <summary>Parses using the configured options, and returns a uniformly build data structure.</summary>
		/// <returns>The parsed data.</returns>
		public ParserData Parse()
		{
			Data.Clear();
			using (var index = new Index(false, true))
			{
				foreach (var sourceFile in SourceFiles)
				{
					var code = index.ParseTranslationUnit(sourceFile, CommandLineArgs, out var unit);
					CheckErrors(unit, code, true);
					var cursor = Clang.GetTranslationUnitCursor(unit);
					cursor.VisitChildren(ParseStructs);
					cursor.VisitChildren(ParseEnums);
					cursor.VisitChildren(ParseTypeDefs);
					cursor.VisitChildren(ParseFunctions);
					cursor.VisitChildren(ParseMacros);
					unit.Dispose();
				}
			}

			return Data;
		}

		protected virtual bool CheckErrors(TranslationUnit unit, ErrorCode code, bool exception)
		{
			if (code == ErrorCode.Success)
				return true;
			var buffer = new StringBuilder();
			foreach (var diagnostic in unit.Diagnostics)
				buffer.AppendLine(diagnostic.ToString());
			if (exception)
				throw new ParseError(buffer.ToString());
			return false;
		}

		protected virtual ChildVisitResult ParseEnums(Cursor cursor, Cursor parent, ClientData data)
		{
			if (cursor.Location.IsInSystemHeader || cursor.Kind != CursorKind.EnumDecl)
				return ChildVisitResult.Continue;
			var name = cursor.Spelling;
			if (string.IsNullOrEmpty(name))
				name = cursor.Type.Spelling;
			if (Data.Enums.ContainsName(name))
				return ChildVisitResult.Recurse;
			var intType = cursor.GetEnumIntegerType();
			var unsigned = Regex.IsMatch(intType.CanonicalType.Spelling, "^unsigned ");
			var cEnum = new CEnum(name, intType);
			cursor.VisitChildren((memberCursor, _, __) =>
			{
				if (unsigned)
					cEnum.Add(memberCursor.Spelling, memberCursor.GetEnumUnsignedValue());
				else
					cEnum.Add(memberCursor.Spelling, memberCursor.GetEnumValue());
				return ChildVisitResult.Continue;
			}, data);
			Data.Enums.Add(cEnum);
			return ChildVisitResult.Recurse;
		}

		protected virtual ChildVisitResult ParseFunctions(Cursor cursor, Cursor parent, ClientData data)
		{
			if (cursor.Location.IsInSystemHeader || cursor.Kind != CursorKind.FunctionDecl)
				return ChildVisitResult.Continue;
			var name = cursor.Spelling;
			if (Data.Functions.ContainsName(name))
				return ChildVisitResult.Recurse;
			var cFunction = new CFunction(name, cursor.ResultType);
			cursor.VisitChildren((funcCursor, _, __) =>
			{
				if (funcCursor.Kind == CursorKind.ParmDecl)
				{
					var paramName = funcCursor.Spelling;
					if (string.IsNullOrEmpty(paramName))
						paramName = $"param{cFunction.Arguments.Count + 1}";
					cFunction.Add(paramName, funcCursor.Type);
				}

				return ChildVisitResult.Continue;
			}, data);
			Data.Functions.Add(cFunction);
			return ChildVisitResult.Continue;
		}

		protected virtual ChildVisitResult ParseMacros(Cursor cursor, Cursor parent, ClientData data) =>
			ChildVisitResult.Continue;

		protected virtual ChildVisitResult ParseStructs(Cursor cursor, Cursor parent, ClientData data)
		{
			if (cursor.Location.IsInSystemHeader || cursor.Kind != CursorKind.StructDecl)
				return ChildVisitResult.Continue;
			var name = cursor.Spelling;
			if (string.IsNullOrEmpty(name))
				name = cursor.Type.Spelling;
			if (Data.Structs.ContainsName(name))
				return ChildVisitResult.Recurse;
			var cStruct = new CStruct(name);
			cursor.VisitChildren((fieldCursor, _, __) =>
			{
				cStruct.Add(fieldCursor.Spelling, fieldCursor.Type);
				return ChildVisitResult.Continue;
			}, data);
			Data.Structs.Add(cStruct);
			return ChildVisitResult.Recurse;
		}

		protected virtual ChildVisitResult ParseTypeDefs(Cursor cursor, Cursor parent, ClientData data)
		{
			if (cursor.Location.IsInSystemHeader || cursor.Kind != CursorKind.TypedefDecl)
				return ChildVisitResult.Continue;
			var name = cursor.Spelling;
			if (Data.TypeDefs.ContainsName(name))
				return ChildVisitResult.Recurse;
			if (Data.Structs.ContainsName(name))
				return ChildVisitResult.Continue;
			var type = cursor.GetTypedefUnderlyingType();
			if (type.Kind == TypeKind.Pointer)
			{
				var pointee = type.CanonicalType.PointeeType;
				if (pointee.Kind == TypeKind.FunctionProto)
				{
					var func = new CFunction(name, pointee.ResultType);
					cursor.VisitChildren((proto, _, __) =>
					{
						if (proto.Kind != CursorKind.ParmDecl)
							return ChildVisitResult.Continue;
						var argName = proto.Spelling;
						if (string.IsNullOrEmpty(argName))
							argName = $"param#{func.Arguments.Count + 1}";
						func.Add(argName, proto.Type);
						return ChildVisitResult.Continue;
					}, data);
					Data.Delegates.Add(func);
					return ChildVisitResult.Continue;
				}
			}

			Data.TypeDefs.Add(new CTypeDef(name, type));
			return ChildVisitResult.Continue;
		}

		#endregion
	}
}