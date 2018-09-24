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
// Module.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct Module
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="Module" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public Module(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the null (invalid) <see cref="Module" />.</summary>
		/// <value>A null <see cref="Module" />.</value>
		public static Module Null => new Module(IntPtr.Zero);

		/// <summary>Gets the module file where the provided module object came from..</summary>
		/// <value>The module file where the provided module object came from..</value>
		public File ASTFile => Clang.ModuleGetASTFile(this);

		/// <summary>Gets the full name of the module, e.g. "std.vector".</summary>
		/// <value>The full name.</value>
		public string FullName => Clang.ModuleGetFullName(this);

		/// <summary>Gets a value indicating whether this instance is system module.</summary>
		/// <value><c>true</c> if this instance is system; otherwise, <c>false</c>.</value>
		public bool IsSystem => Clang.ModuleIsSystem(this);

		/// <summary>
		///     a module object. the name of the module, e.g. for the 'std.vector' sub-module it will
		///     return "vector".
		/// </summary>
		/// <value>The name.</value>
		public string Name => Clang.ModuleGetName(this);

		/// <summary>Gets the parent module.
		///     <para>If the given module is top-level, e.g. for 'std.vector' it will return the 'std' module.</para>
		/// </summary>
		/// <value>The parent.</value>
		public Module Parent => Clang.ModuleGetParent(this);

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="Module" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(Module instance) => instance._pointer;

		#endregion
	}
}