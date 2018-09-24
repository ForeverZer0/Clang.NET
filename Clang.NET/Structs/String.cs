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
// String.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     A character string. The String type is used to return strings from the interface when the
	///     ownership of that string might differ from one call to the next.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct String : IDisposable
	{
		private readonly IntPtr _data;
		private readonly uint _privateFlags;

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeString(this);

		#endregion

		#region Methods

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		///     <para>
		///         This method automatically calls <see cref="Dispose" /> for the underlying native object
		///         after marshalling.
		///     </para>
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString()
		{
			var str = Clang.GetCString(this);
			Dispose();
			return str;
		}

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="String" /> to <see cref="System.String" />
		///     .
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator string(String str) => str.ToString();

		#endregion
	}
}