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
// ClientData.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	/// <summary>
	/// Pointer type representing arbitrary client data.
	/// <para>Can be used interchangeably with a <see cref="IntPtr"/> type.</para>
	/// </summary>
	public struct ClientData
	{
		private readonly IntPtr _pointer;

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientData"/> struct.
		/// </summary>
		/// <param name="address">The address in memory.</param>
		public ClientData(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>
		/// Gets the null <see cref="ClientData"/> value.
		/// </summary>
		/// <value>
		/// The null data.
		/// </value>
		public static ClientData Null => new ClientData(IntPtr.Zero);

		#endregion

		#region Operators

		/// <summary>
		/// Performs an implicit conversion from <see cref="ClientData"/> to <see cref="IntPtr"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator IntPtr(ClientData instance) => instance._pointer;

		/// <summary>
		/// Performs an implicit conversion from <see cref="IntPtr"/> to <see cref="ClientData"/>.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator ClientData(IntPtr instance) => new ClientData(instance);

		#endregion
	}
}