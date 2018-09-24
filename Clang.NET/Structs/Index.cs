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
// Index.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct Index : IDisposable
	{
		private readonly IntPtr _pointer;

		public Index(IntPtr address)
		{
			_pointer = address;
		}

		public Index(bool exclude, bool display)
		{
			_pointer = Clang.CreateIndex(exclude, display);
		}

		#region Properties & Indexers

		public static Index Null => new Index(IntPtr.Zero);

		/// <summary>Gets or sets general options associated with a index.</summary>
		/// <value>The global options.</value>
		public GlobalOptFlags GlobalOptions
		{
			get => Clang.IndexGetGlobalOptions(this);
			set => Clang.IndexSetGlobalOptions(this, value);
		}

		#endregion

		#region IDisposable Implementation

		public void Dispose() => Clang.DisposeIndex(this);

		#endregion

		#region Methods

		public IndexAction CreateAction() => Clang.IndexActionCreate(this);

		public TranslationUnit CreateTranslationUnit(string filename) => Clang.CreateTranslationUnit(this, filename);

		public TranslationUnit CreateTranslationUnit(string filename, out ErrorCode code)
		{
			code = Clang.CreateTranslationUnit2(this, filename, out var unit);
			return unit;
		}

		public TranslationUnit CreateTranslationUnit(string filename, string[] args, out ErrorCode code)
		{
			code = Clang.CreateTranslationUnit2(this, filename, out var unit);
			return unit;
		}

		public TranslationUnit CreateTranslationUnit(string filename, string[] args, UnsavedFile[] unsaved) =>
			Clang.CreateTranslationUnitFromSourceFile(this, filename, args.Length, args, (uint) unsaved.Length,
				unsaved);

		public TranslationUnit ParseTranslationUnit(string filename) =>
			ParseTranslationUnit(filename, new string[0], 0);

		public TranslationUnit ParseTranslationUnit(string filename, string[] cmdLineArgs) =>
			ParseTranslationUnit(filename, cmdLineArgs, 0);

		public TranslationUnit ParseTranslationUnit(string filename, string[] cmdLineArgs, uint options,
			params UnsavedFile[] unsaved) =>
			Clang.ParseTranslationUnit(this, filename, cmdLineArgs, cmdLineArgs.Length, unsaved,
				Convert.ToUInt32(unsaved.Length), options);

		public ErrorCode ParseTranslationUnit(string filename, out TranslationUnit unit) =>
			ParseTranslationUnit(filename, new string[0], 0, out unit);

		public ErrorCode ParseTranslationUnit(string filename, string[] cmdLineArgs, out TranslationUnit unit) =>
			ParseTranslationUnit(filename, cmdLineArgs, 0, out unit);

		public ErrorCode ParseTranslationUnit(string filename, string[] cmdLineArgs, uint options,
			out TranslationUnit unit, params UnsavedFile[] unsaved) =>
			Clang.ParseTranslationUnit2(this, filename, cmdLineArgs, cmdLineArgs.Length, unsaved,
				Convert.ToUInt32(unsaved.Length), options, out unit);

		public ErrorCode
			ParseTranslationUnitFullArgv(string filename, string[] cmdLineArgs, out TranslationUnit unit) =>
			ParseTranslationUnitFullArgv(filename, cmdLineArgs, 0, out unit);

		public ErrorCode ParseTranslationUnitFullArgv(string filename, string[] cmdLineArgs, uint options,
			out TranslationUnit unit, params UnsavedFile[] unsaved) =>
			Clang.ParseTranslationUnit2FullArgv(this, filename, cmdLineArgs, cmdLineArgs.Length, unsaved,
				Convert.ToUInt32(unsaved.Length), options, out unit);

		/// <summary>
		///     Sets the invocation emission path option. The invocation emission path specifies a path
		///     which will contain log files for certain LibClang invocations.
		///     <para>A <c>null</c> value (default) implies that LibClang invocations are not logged.</para>
		/// </summary>
		/// <param name="path">The path.</param>
		public void SetInvocationEmissionPathOption(string path) =>
			Clang.IndexSetInvocationEmissionPathOption(this, path);

		#endregion

		#region Operators

		public static implicit operator IntPtr(Index instance) => instance._pointer;

		#endregion
	}
}