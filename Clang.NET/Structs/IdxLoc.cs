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
// IdxLoc.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>Source location passed to index callbacks.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct IdxLoc
	{
		private readonly IntPtr PtrData1;
		private readonly IntPtr PtrData2;
		private readonly uint IntData;

		#region Properties & Indexers

		/// <summary>Retrieves the location represented by this <see cref="IdxLoc" />.</summary>
		/// <value>The location.</value>
		public SourceLocation Location => Clang.IndexLocGetCXSourceLocation(this);

		#endregion

		#region Methods

		/// <summary>
		///     Retrieve the IdxFile, file, line, column, and offset represented by the given IdxLoc. If
		///     the location refers into a macro expansion, retrieves the location of the macro expansion and
		///     if it refers into a macro argument retrieves the location of the argument.
		/// </summary>
		/// <param name="indexFile">The index file.</param>
		/// <param name="file">The file.</param>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <param name="offset">The offset.</param>
		public void GetFileLocation(out IdxClientFile indexFile, out File file, out uint line, out uint column,
			out uint offset)
		{
			Clang.IndexLocGetFileLocation(this, out indexFile, out file, out line, out column, out offset);
		}

		#endregion
	}
}