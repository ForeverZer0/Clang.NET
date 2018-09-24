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
// PlatformAvailability.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     Describes the availability of a given entity on a particular platform, e.g., a particular
	///     class might only be available on Mac OS 10.7 or newer.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct PlatformAvailability : IDisposable
	{
		/// <summary>
		///     A string that describes the platform for which this structure provides availability
		///     information. Possible values are "ios" or "macos".
		/// </summary>
		public readonly String Platform;

		/// <summary>The version number in which this entity was introduced.</summary>
		public readonly Version Introduced;

		/// <summary>The version number in which this entity was deprecated (but is still available).</summary>
		public readonly Version Deprecated;

		/// <summary>
		///     The version number in which this entity was obsoleted, and therefore is no longer
		///     available.
		/// </summary>
		public readonly Version Obsoleted;

		/// <summary>Whether the entity is unconditionally unavailable on this platform.</summary>
		[MarshalAs(UnmanagedType.Bool)]
		public readonly bool Unavailable;

		/// <summary>An optional message to provide to a user of this API, e.g., to suggest replacement APIs.</summary>
		public readonly String Message;

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeCXPlatformAvailability(ref this);

		#endregion
	}
}