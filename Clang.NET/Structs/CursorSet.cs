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
// CursorSet.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct CursorSet : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="CursorSet" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public CursorSet(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the null (invalid) <see cref="CursorSet" />.</summary>
		/// <value>A null <see cref="CursorSet" />.</value>
		public static CursorSet Null => new CursorSet(IntPtr.Zero);

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeCursorSet(this);

		#endregion

		#region Methods

		/// <summary>Creates an empty <see cref="CursorSet" />.</summary>
		/// <returns>A newly created <see cref="CursorSet" />.</returns>
		public static CursorSet Create() => Clang.CreateCursorSet();

		/// <summary>Queries a <see cref="CursorSet" /> to see if it contains a specific <see cref="Cursor" />.</summary>
		/// <param name="cursor">The cursor to query.</param>
		/// <returns><c>true</c> if set contains the specified cursor; otherwise, <c>false</c>.</returns>
		public bool Contains(Cursor cursor) => Clang.CursorSetContains(this, cursor);

		/// <summary>Inserts the specified cursor into the <see cref="CursorSet" />.</summary>
		/// <param name="cursor">The cursor to insert.</param>
		/// <returns>
		///     <c>false</c> if the <see cref="Cursor" /> was already in the set, and <c>true</c>
		///     otherwise.
		/// </returns>
		public bool Insert(Cursor cursor) => Clang.CursorSetInsert(this, cursor);

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="CursorSet" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(CursorSet instance) => instance._pointer;

		#endregion
	}
}