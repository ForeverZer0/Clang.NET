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
// PrimitiveType.cs created on 2018-09-23

#endregion

using System;
using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Enumeration of bit-flags describing a C-style type/entity as a language-independent type.</summary>
	[DataContract(Namespace = "clang", Name = "primitive"), Flags]
	public enum PrimitiveType : uint
	{
		/// <summary>An invalid/undefined type that cannot be interpreted as a basic type.</summary>
		Invalid = 0x00000000,

		/// <summary>No defined type.</summary>
		Void = 0x00000001,

		/// <summary>A pointer type.</summary>
		Pointer = 0x00000002,

		/// <summary>A boolean (true/false) type.</summary>
		Boolean = 0x00000004,

		/// <summary>An 8-bit signed integer.</summary>
		Int8 = 0x00000008,

		/// <summary>A 16-bit signed integer.</summary>
		Int16 = 0x00000010,

		/// <summary>A 32-bit signed integer.</summary>
		Int32 = 0x00000020,

		/// <summary>A 64-bit signed integer.</summary>
		Int64 = 0x00000040,

		/// <summary>A 128-bit signed integer.</summary>
		Int128 = 0x00000080,

		/// <summary>An 8-bit unsigned integer.</summary>
		UInt8 = 0x00000100,

		/// <summary>A 16-bit unsigned integer.</summary>
		UInt16 = 0x00000200,

		/// <summary>A 32-bit unsigned integer.</summary>
		UInt32 = 0x00000400,

		/// <summary>A 64-bit unsigned integer.</summary>
		UInt64 = 0x00000800,

		/// <summary>A 128-bit unsigned integer.</summary>
		UInt128 = 0x00001000,

		/// <summary>A 16-bit floating point number.</summary>
		Float16 = 0x00002000,

		/// <summary>A 32-bit floating point number.</summary>
		Float32 = 0x00004000,

		/// <summary>A 64-bit floating point number.</summary>
		Float64 = 0x00008000,

		/// <summary>A 128-bit floating point number.</summary>
		Float128 = 0x00010000,

		/// <summary>A fixed-size constant array buffer.</summary>
		ConstantArray = 0x04000000,

		/// <summary>A macro.</summary>
		Macro = 0x08000000,

		/// <summary>An enum (any base integer type)</summary>
		Enum = 0x10000000,

		/// <summary>A struct.</summary>
		Struct = 0x20000000,

		/// <summary>A function.</summary>
		Function = 0x40000000,

		/// <summary>A typedef.</summary>
		TypeDef = 0x80000000,

		/// <summary>All flags.</summary>
		All = 0xFFFFFFFF
	}
}