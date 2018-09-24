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
// IdxIncludedFileInfo.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	/// <summary>Data for ppIncludedFile callback.</summary>
	public struct IdxIncludedFileInfo
	{
		/// <summary>Location of '#' in the # include/ # import directive.</summary>
		public IdxLoc HashLoc;

		/// <summary>Filename as written in the # include/ # import directive.</summary>
		public IntPtr Filename; // const char*

		/// <summary>The actual file that the # include/ # import directive resolved to.</summary>
		public File File;

		/// <summary></summary>
		public int IsImport;

		/// <summary></summary>
		public int IsAngled;

		/// <summary>Non-zero if the directive was automatically turned into a module import.</summary>
		public int IsModuleImport;
	}
}