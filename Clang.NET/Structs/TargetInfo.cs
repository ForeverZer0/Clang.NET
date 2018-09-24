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
// TargetInfo.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	[StructLayout(LayoutKind.Sequential)]
	public struct TargetInfo : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="TargetInfo" /> struct.</summary>
		/// <param name="address">The address of the struct in memory.</param>
		public TargetInfo(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets a null (invalid) instance of a <see cref="TargetInfo" />.</summary>
		/// <value>A null <see cref="TargetInfo" />.</value>
		public static TargetInfo Null => new TargetInfo(IntPtr.Zero);

		/// <summary>Get the pointer width of the target in bits. Returns -1 in case of error.</summary>
		/// <value>The width of the pointer.</value>
		public int PointerWidth => Clang.TargetInfoGetPointerWidth(this);

		/// <summary>Get the normalized target triple as a string.
		///     <para>Returns the empty string in case of any error.</para>
		/// </summary>
		/// <value>The triple.</value>
		public string Triple => Clang.TargetInfoGetTriple(this);

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.TargetInfoDispose(this);

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="TargetInfo" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(TargetInfo instance) => instance._pointer;

		#endregion
	}
}