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
// IdxEntityInfo.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary></summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct IdxEntityInfo
	{
		/// <summary></summary>
		public IdxEntityKind Kind;

		/// <summary></summary>
		public IdxEntityCXXTemplateKind TemplateKind;

		/// <summary></summary>
		public IdxEntityLanguage Lang;

		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public string Name; // const char*

		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public string USR; // const char*

		/// <summary></summary>
		public Cursor Cursor;

		/// <summary></summary>
		public IntPtr Attributes; // const CXIdxAttrInfo *const *

		/// <summary></summary>
		public uint NumAttributes;
	}
}