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
// VirtualFileOverlay.cs created on 2018-09-22

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	public struct VirtualFileOverlay : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="VirtualFileOverlay" /> struct.</summary>
		/// <param name="address">The address of the struct in memory.</param>
		public VirtualFileOverlay(IntPtr address)
		{
			_pointer = address;
		}

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.VirtualFileOverlayDispose(this);

		#endregion

		#region Methods

		/// <summary>Create a <see cref="VirtualFileOverlay" /> object.</summary>
		/// <returns>A newly created object.</returns>
		public static VirtualFileOverlay Create() => Clang.VirtualFileOverlayCreate(0);

		/// <summary>
		///     Map an absolute virtual file path to an absolute real one. The virtual path must be
		///     canonicalized (not contain "."/"..").
		/// </summary>
		/// <param name="virtualPath">The virtual path to map.</param>
		/// <param name="realPath">The real path.</param>
		/// <returns>Flag indicating the success or failure of the operation.</returns>
		public ErrorCode AddFileMapping(string virtualPath, string realPath) =>
			Clang.VirtualFileOverlayAddFileMapping(this, virtualPath, realPath);

		/// <summary>
		///     Set the case sensitivity for the <see cref="VirtualFileOverlay" /> object.
		///     <para>
		///         The <see cref="VirtualFileOverlay" /> object is case-sensitive by default, this option
		///         can be used to override the default.
		///     </para>
		/// </summary>
		/// <param name="caseSensitive">
		///     <c>true</c> to set as case-sensitive, or <c>false</c> for
		///     case-insensitivity.
		/// </param>
		/// <returns>Flag indicating the success or failure of the operation.</returns>
		public ErrorCode SetCodeSensitivity(bool caseSensitive) =>
			Clang.VirtualFileOverlaySetCaseSensitivity(this, caseSensitive);

		/// <summary>Write out the <see cref="VirtualFileOverlay" /> object to a buffer.</summary>
		/// <param name="buffer">The buffer that received the output.</param>
		/// <returns>Flag indicating the success or failure of the operation.</returns>
		public ErrorCode WriteToBuffer(out byte[] buffer)
		{
			var code = Clang.VirtualFileOverlayWriteToBuffer(this, 0, out var ptr, out var size);
			buffer = new byte[size];
			Marshal.Copy(ptr, buffer, 0, (int) size);
			Clang.Free(ptr);
			return code;
		}

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="VirtualFileOverlay" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(VirtualFileOverlay instance) => instance._pointer;

		#endregion
	}
}