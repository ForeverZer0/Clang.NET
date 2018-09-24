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
// UnsavedFile.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LibClang
{
	/// <summary>
	///     Provides the contents of a file that has not yet been saved to disk. Each CXUnsavedFile
	///     instance provides the name of a file on the system along with the current contents of that file
	///     that have not yet been saved to disk.
	/// </summary>
	public struct UnsavedFile
	{
		/// <summary>
		///     The file whose contents have not yet been saved. This file must already exist in the file
		///     system.
		/// </summary>
		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public string Filename; // const char*

		private readonly IntPtr _contents; // const char*
		private readonly uint _length;

		/// <summary>A buffer containing the unsaved contents of this file.</summary>
		public string Contents
		{
			get
			{
				var buffer = new byte[_length];
				Marshal.Copy(_contents, buffer, 0, buffer.Length);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}
}