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
// CallingConv.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describes the calling convention of a function type</summary>
	public enum CallingConv
	{
		/// <summary></summary>
		Default = 0,

		/// <summary></summary>
		C = 1,

		/// <summary></summary>
		X86StdCall = 2,

		/// <summary></summary>
		X86FastCall = 3,

		/// <summary></summary>
		X86ThisCall = 4,

		/// <summary></summary>
		X86Pascal = 5,

		/// <summary></summary>
		AAPCS = 6,

		/// <summary></summary>
		AAPCSVFP = 7,

		/// <summary></summary>
		X86RegCall = 8,

		/// <summary></summary>
		IntelOclBicc = 9,

		/// <summary></summary>
		Win64 = 10,

		/// <summary></summary>
		X8664Win64 = 10,

		/// <summary></summary>
		X8664SysV = 11,

		/// <summary></summary>
		X86VectorCall = 12,

		/// <summary></summary>
		Swift = 13,

		/// <summary></summary>
		PreserveMost = 14,

		/// <summary></summary>
		PreserveAll = 15,

		/// <summary></summary>
		Invalid = 100,

		/// <summary></summary>
		Unexposed = 200
	}
}