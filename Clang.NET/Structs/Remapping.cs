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
// Remapping.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Remapping : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="Remapping" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public Remapping(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the count of remapped files.</summary>
		/// <value>The file count.</value>
		public int FileCount => Convert.ToInt32(Clang.RemapGetNumFiles(this));

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.RemapDispose(this);

		#endregion

		#region Methods

		/// <summary>Retrieve a remapping.</summary>
		/// <param name="file">The path that contains metadata about remappings.</param>
		/// <returns>The requested remapping.</returns>
		public static Remapping FromFile(string file) => Clang.GetRemappings(file);

		/// <summary>Retrieve a remapping from files containing remapping info.</summary>
		/// <param name="files">The files.</param>
		/// <returns>A newly created <see cref="Remapping" /> object.</returns>
		public static Remapping FromFiles(params string[] files) =>
			Clang.GetRemappingsFromFileList(files, Convert.ToUInt32(files.Length));

		/// <summary>Get the original and the associated filename from the remapping.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <param name="original">The original filename.</param>
		/// <param name="transformed">The transformed filename.</param>
		public void GetFilenames(uint index, out string original, out string transformed)
		{
			Clang.RemapGetFilenames(this, index, out var orig, out var trans);
			original = orig.ToString();
			transformed = trans.ToString();
		}

		/// <summary>Get the original and the associated filename from the remapping.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <param name="original">The original filename.</param>
		/// <param name="transformed">The transformed filename.</param>
		public void GetFilenames(int index, out string original, out string transformed)
		{
			Clang.RemapGetFilenames(this, Convert.ToUInt32(index), out var orig, out var trans);
			original = orig.ToString();
			transformed = trans.ToString();
		}

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="Remapping" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(Remapping instance) => instance._pointer;

		#endregion
	}
}