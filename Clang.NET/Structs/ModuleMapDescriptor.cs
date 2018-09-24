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
// ModuleMapDescriptor.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	public struct ModuleMapDescriptor : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="ModuleMapDescriptor" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public ModuleMapDescriptor(IntPtr address)
		{
			_pointer = address;
		}

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.ModuleMapDescriptorDispose(this);

		#endregion

		#region Methods

		/// <summary>Creates a <see cref="ModuleMapDescriptor" /> object.</summary>
		/// <returns>A newly created <see cref="ModuleMapDescriptor" /> object.</returns>
		public static ModuleMapDescriptor Create() => Clang.ModuleMapDescriptorCreate();

		/// <summary>Sets the framework module name that the module map describes</summary>
		/// <param name="name">The name.</param>
		/// <returns>The result code.</returns>
		public ErrorCode SetFrameworkModuleName(string name) =>
			Clang.ModuleMapDescriptorSetFrameworkModuleName(this, name);

		/// <summary>Sets the umbrella header name that the module map describes.</summary>
		/// <param name="name">The name.</param>
		/// <returns>The result code.</returns>
		public ErrorCode SetUmbrellaHeader(string name) => Clang.ModuleMapDescriptorSetUmbrellaHeader(this, name);

		/// <summary>Write out the <see cref="ModuleMapDescriptor" /> object to a buffer.</summary>
		/// <param name="buffer">The buffer that received the output.</param>
		/// <returns>Flag indicating the success or failure of the operation.</returns>
		public ErrorCode WriteToBuffer(out byte[] buffer)
		{
			var code = Clang.ModuleMapDescriptorWriteToBuffer(this, 0, out var ptr, out var size);
			buffer = new byte[size];
			Marshal.Copy(ptr, buffer, 0, (int) size);
			Clang.Free(ptr);
			return code;
		}

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="ModuleMapDescriptor" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(ModuleMapDescriptor instance) => instance._pointer;

		#endregion
	}
}