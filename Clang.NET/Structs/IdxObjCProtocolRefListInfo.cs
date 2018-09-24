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
// IdxObjCProtocolRefListInfo.cs created on 2018-09-21

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LibClang
{
	[StructLayout(LayoutKind.Sequential)]
	public struct IdxObjCProtocolRefListInfo : IEnumerable<IdxObjCProtocolRefInfo>
	{
		private readonly IntPtr Protocols; // const CXIdxObjCProtocolRefInfo *const *
		private readonly uint NumProtocols;

		/// <summary>
		/// Gets the of <see cref="IdxObjCProtocolRefInfo"/> objects in the collection.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public int Count => Convert.ToInt32(NumProtocols);

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		/// <exception cref="NotImplementedException"></exception>
		public IEnumerator<IdxObjCProtocolRefInfo> GetEnumerator()
		{
			var size = Marshal.SizeOf<IdxObjCProtocolRefInfo>();
			var count = Convert.ToInt32(NumProtocols);
			for (var i = 0; i < count; i++)
				yield return Marshal.PtrToStructure<IdxObjCProtocolRefInfo>(Protocols + (i * size));
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}