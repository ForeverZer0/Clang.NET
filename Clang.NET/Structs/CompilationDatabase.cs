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
// CompilationDatabase.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct CompilationDatabase : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="CompilationDatabase" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public CompilationDatabase(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the null (invalid) <see cref="CompilationDatabase" />.</summary>
		/// <value>A null <see cref="CompilationDatabase" />.</value>
		public static CompilationDatabase Null => new CompilationDatabase(IntPtr.Zero);

		/// <summary>Get all the compile commands in this compilation database.</summary>
		/// <value>The commands.</value>
		public CompileCommands Commands => Clang.CompilationDatabaseGetAllCompileCommands(this);

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.CompilationDatabaseDispose(this);

		#endregion

		#region Methods

		/// <summary>Creates a compilation database from the database found in directory build directory.</summary>
		/// <param name="buildDir">The build directory.</param>
		/// <param name="error">The error code result.</param>
		/// <returns>A newly created <see cref="CompilationDatabase" />.</returns>
		public static CompilationDatabase FromDirectory(string buildDir, out CompilationDatabaseError error) =>
			Clang.CompilationDatabaseFromDirectory(buildDir, out error);

		/// <summary>Find the compile commands used for a file.</summary>
		/// <param name="filename">The file to query.</param>
		/// <returns>The commands for the specified file.</returns>
		public CompileCommands GetCompileCommands(string filename) =>
			Clang.CompilationDatabaseGetCompileCommands(this, filename);

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="CompilationDatabase" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(CompilationDatabase instance) => instance._pointer;

		#endregion
	}
}