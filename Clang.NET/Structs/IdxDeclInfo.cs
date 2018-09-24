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
// IdxDeclInfo.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	/// <summary></summary>
	public struct IdxDeclInfo
	{
		/// <summary></summary>
		public IntPtr EntityInfo; // const CXIdxEntityInfo *

		/// <summary></summary>
		public Cursor Cursor;

		/// <summary></summary>
		public IdxLoc Loc;

		/// <summary></summary>
		public IntPtr SemanticContainer; // const CXIdxContainerInfo *

		/// <summary>
		///     Generally same as #semanticContainer but can be different in cases like out-of-line C++
		///     member functions.
		/// </summary>
		public IntPtr LexicalContainer; // const CXIdxContainerInfo *

		/// <summary></summary>
		public int IsRedeclaration;

		/// <summary></summary>
		public int IsDefinition;

		/// <summary></summary>
		public int IsContainer;

		/// <summary></summary>
		public IntPtr DeclAsContainer; // const CXIdxContainerInfo *

		/// <summary>
		///     Whether the declaration exists in code or was created implicitly by the compiler, e.g.
		///     implicit Objective-C methods for properties.
		/// </summary>
		public int IsImplicit;

		/// <summary></summary>
		public IntPtr Attributes; // const CXIdxAttrInfo *const *

		/// <summary></summary>
		public uint NumAttributes;

		/// <summary></summary>
		public uint Flags;
	}
}