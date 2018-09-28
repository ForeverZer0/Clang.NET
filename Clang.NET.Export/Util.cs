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
// Util.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	internal static class Util
	{
		#region Methods

		public static PrimitiveType TypeToPrimitive(Type type)
		{
			switch (type.Kind)
			{
				case TypeKind.Invalid: 
				case TypeKind.Unexposed:
					return PrimitiveType.Invalid;
				case TypeKind.Void:
					return PrimitiveType.Void;
				case TypeKind.Bool:
					return PrimitiveType.Boolean;
				case TypeKind.CharS:
				case TypeKind.SChar:
					return PrimitiveType.Int8;
				case TypeKind.CharU:
				case TypeKind.UChar:
					return PrimitiveType.UInt8;
				case TypeKind.Char16:
				case TypeKind.Short:
					return PrimitiveType.Int16;
				case TypeKind.UShort:
					return PrimitiveType.UInt16;
				case TypeKind.Int:
				case TypeKind.Long:
				case TypeKind.Char32:
					return PrimitiveType.Int32;
				case TypeKind.UInt:
				case TypeKind.ULong:
					return PrimitiveType.UInt32;
				case TypeKind.LongLong:
					return PrimitiveType.Int64;
				case TypeKind.ULongLong:
					return PrimitiveType.UInt64;
				case TypeKind.Int128:
					return PrimitiveType.Int128;
				case TypeKind.UInt128:
					return PrimitiveType.UInt128;
				case TypeKind.Half:
				case TypeKind.Float16:
					return PrimitiveType.Float16;
				case TypeKind.Float:
					return PrimitiveType.Float32;
				case TypeKind.Double:
					return PrimitiveType.Float64;
				case TypeKind.LongDouble:
				case TypeKind.Float128:
					return PrimitiveType.Float128;
				case TypeKind.Pointer:
				case TypeKind.NullPtr:
				case TypeKind.BlockPointer:
				case TypeKind.MemberPointer:
					return PrimitiveType.Pointer;
				case TypeKind.FunctionNoProto:
				case TypeKind.FunctionProto:
					return PrimitiveType.Function;
				case TypeKind.Record:
					return PrimitiveType.Struct;
				case TypeKind.Enum:
					return PrimitiveType.Enum;
				case TypeKind.Typedef:
					return PrimitiveType.TypeDef;
				case TypeKind.ConstantArray:
					return PrimitiveType.ConstantArray;
				case TypeKind.WChar:
				case TypeKind.Complex:
				case TypeKind.LValueReference:
				case TypeKind.RValueReference:
				
				case TypeKind.Vector:

				default:
					Console.WriteLine(type.Kind);
					return PrimitiveType.Invalid;

			}
		}

		#endregion
	}
}