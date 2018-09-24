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
// CompileCommands.cs created on 2018-09-21

#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace LibClang
{
	public struct CompileCommands : IDisposable, IEnumerable<CompileCommand>
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="CompileCommands" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public CompileCommands(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the <see cref="CompileCommand" /> at the specified index.</summary>
		/// <value>The <see cref="CompileCommand" />.</value>
		/// <param name="index">The index.</param>
		/// <returns>The specified <see cref="CompileCommand" />.</returns>
		public CompileCommand this[int index] => Clang.CompileCommandsGetCommand(this, Convert.ToUInt32(index));

		/// <summary>Gets the <see cref="CompileCommand" /> at the specified index.</summary>
		/// <value>The <see cref="CompileCommand" />.</value>
		/// <param name="index">The index.</param>
		/// <returns>The specified <see cref="CompileCommand" />.</returns>
		public CompileCommand this[uint index] => Clang.CompileCommandsGetCommand(this, index);

		/// <summary>Gets the null (invalid) <see cref="CompileCommands" />.</summary>
		/// <value>A null <see cref="CompileCommands" /> object.</value>
		public static CompileCommands Null => new CompileCommands(IntPtr.Zero);

		/// <summary>Get the number of <see cref="CompileCommand" /> objects for a file.</summary>
		/// <value>The command count.</value>
		public int Count => Convert.ToInt32(Clang.CompileCommandsGetSize(this));

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.CompileCommandsDispose(this);

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region IEnumerable<CompileCommand> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<CompileCommand> GetEnumerator()
		{
			var size = Clang.CompileCommandsGetSize(this);
			for (uint i = 0; i < size; i++)
				yield return Clang.CompileCommandsGetCommand(this, i);
		}

		#endregion

		#region Methods

		/// <summary>Get the specified <see cref="CompileCommand" /> for a file.</summary>
		/// <param name="index">The index of the command to retrieve.</param>
		/// <returns>The command.</returns>
		public CompileCommand GetCommand(uint index) => Clang.CompileCommandsGetCommand(this, index);

		/// <summary>Get the specified <see cref="CompileCommand" /> for a file.</summary>
		/// <param name="index">The index of the command to retrieve.</param>
		/// <returns>The command.</returns>
		public CompileCommand GetCommand(int index) => Clang.CompileCommandsGetCommand(this, Convert.ToUInt32(index));

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="CompileCommands" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(CompileCommands instance) => instance._pointer;

		#endregion
	}
}