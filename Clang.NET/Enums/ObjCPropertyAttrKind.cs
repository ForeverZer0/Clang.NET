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
// ObjCPropertyAttrKind.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	/// <summary>Property attributes for a ObjCPropertyDecl.</summary>
	[Flags]
	public enum ObjCPropertyAttrKind
	{
		/// <summary></summary>
		Noattr = 0,

		/// <summary></summary>
		Readonly = 1,

		/// <summary></summary>
		Getter = 2,

		/// <summary></summary>
		Assign = 4,

		/// <summary></summary>
		Readwrite = 8,

		/// <summary></summary>
		Retain = 16,

		/// <summary></summary>
		Copy = 32,

		/// <summary></summary>
		Nonatomic = 64,

		/// <summary></summary>
		Setter = 128,

		/// <summary></summary>
		Atomic = 256,

		/// <summary></summary>
		Weak = 512,

		/// <summary></summary>
		Strong = 1024,

		/// <summary></summary>
		UnsafeUnretained = 2048,

		/// <summary></summary>
		Class = 4096
	}
}