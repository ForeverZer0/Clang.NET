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
// CompileCommand.cs created on 2018-09-21

#endregion

using System;
using System.Collections.Generic;

namespace LibClang
{
	public struct CompileCommand
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="CompileCommand" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public CompileCommand(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the null (invalid) <see cref="CompileCommand" />.</summary>
		/// <value>A null <see cref="CompileCommand" />.</value>
		public static CompileCommand Null => new CompileCommand(IntPtr.Zero);

		/// <summary>Get the number of arguments in the compiler invocation.</summary>
		/// <value>The argument count.</value>
		public int ArgumentCount => Convert.ToInt32(Clang.CompileCommandGetNumArgs(this));

		/// <summary>Gets the arguments associated with this <see cref="CompileCommand" />.</summary>
		/// <value>The arguments.</value>
		public IEnumerable<string> Arguments
		{
			get
			{
				var size = Clang.CompileCommandGetNumArgs(this);
				for (uint i = 0; i < size; i++)
					yield return Clang.CompileCommandGetArg(this, i);
			}
		}

		/// <summary>Get the working directory where the <see cref="CompileCommand" /> was executed from</summary>
		/// <value>The directory.</value>
		public string Directory => Clang.CompileCommandGetDirectory(this);

		/// <summary>Get the filename associated with the <see cref="CompileCommand" />.</summary>
		/// <value>The filename.</value>
		public string Filename => Clang.CompileCommandGetFilename(this);

		/// <summary>Get the number of source mappings for the compiler invocation.</summary>
		/// <value>The mapped sources count.</value>
		public int MappedSourcesCount => Convert.ToInt32(Clang.CompileCommandGetNumMappedSources(this));

		#endregion

		#region Methods

		/// <summary>Get the specified argument value in the compiler invocations.
		///     <para>For invariant, argument 0 is the compiler executable.</para>
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public string GetArgument(uint index) => Clang.CompileCommandGetArg(this, index);

		/// <summary>Get the specified argument value in the compiler invocations.
		///     <para>For invariant, argument 0 is the compiler executable.</para>
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public string GetArgument(int index) => Clang.CompileCommandGetArg(this, Convert.ToUInt32(index));

		/// <summary>Get the specified mapped source content for the compiler invocation.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The specified content.</returns>
		public string GetMappedSourceContent(uint index) => Clang.CompileCommandGetMappedSourceContent(this, index);

		/// <summary>Get the specified mapped source content for the compiler invocation.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The specified content.</returns>
		public string GetMappedSourceContent(int index) => GetMappedSourceContent(Convert.ToUInt32(index));

		/// <summary>Get the specified mapped source path for the compiler invocation.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The specified source.</returns>
		public string GetMappedSourcePath(uint index) => Clang.CompileCommandGetMappedSourcePath(this, index);

		/// <summary>Get the specified mapped source path for the compiler invocation.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The specified source.</returns>
		public string GetMappedSourcePath(int index) => GetMappedSourcePath(Convert.ToUInt32(index));

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Clang.CompileCommandGetFilename(this);

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="CompileCommand" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(CompileCommand instance) => instance._pointer;

		#endregion
	}
}