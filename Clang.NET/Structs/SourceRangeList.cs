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
// SourceRangeList.cs created on 2018-09-21

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>Identifies an array of ranges.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SourceRangeList : IDisposable, IEnumerable<SourceRange>
	{
		private readonly uint _count;
		private readonly IntPtr _ranges; // SourceRange *

		#region Properties & Indexers

		/// <summary>The number of <see cref="SourceRange" /> objects in the <see cref="SourceRangeList" />.</summary>
		public int Count => Convert.ToInt32(_count);

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeSourceRangeList(ref this);

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region IEnumerable<SourceRange> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<SourceRange> GetEnumerator()
		{
			var size = Marshal.SizeOf<SourceRange>();
			for (var i = 0; i < _count; i++)
				yield return Marshal.PtrToStructure<SourceRange>(_ranges + size * i);
		}

		#endregion
	}
}