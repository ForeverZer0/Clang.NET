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
// DiagnosticSet.cs created on 2018-09-21

#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace LibClang
{
	public struct DiagnosticSet : IDisposable, IEnumerable<Diagnostic>
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="DiagnosticSet" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public DiagnosticSet(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the <see cref="Diagnostic" /> at the specified index.</summary>
		/// <value>The <see cref="Diagnostic" />.</value>
		/// <param name="index">The index.</param>
		/// <returns>The specified <see cref="Diagnostic" /></returns>
		public Diagnostic this[uint index] => Clang.GetDiagnosticInSet(this, index);

		/// <summary>Gets the <see cref="Diagnostic" /> at the specified index.</summary>
		/// <value>The <see cref="Diagnostic" />.</value>
		/// <param name="index">The index.</param>
		/// <returns>The specified <see cref="Diagnostic" /></returns>
		public Diagnostic this[int index] => Clang.GetDiagnosticInSet(this, Convert.ToUInt32(index));

		/// <summary>Gets the null (invalid) <see cref="DiagnosticSet" />.</summary>
		/// <value>A null <see cref="DiagnosticSet" />.</value>
		public static DiagnosticSet Null => new DiagnosticSet(IntPtr.Zero);

		/// <summary>Determine the number of diagnostics in a <see cref="DiagnosticSet" /></summary>
		/// <value>The count.</value>
		public int Count => Convert.ToInt32(Clang.GetNumDiagnosticsInSet(this));

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeDiagnosticSet(this);

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region IEnumerable<Diagnostic> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<Diagnostic> GetEnumerator()
		{
			var size = Clang.GetNumDiagnosticsInSet(this);
			for (uint i = 0; i < size; i++)
				yield return Clang.GetDiagnosticInSet(this, i);
		}

		#endregion

		#region Methods

		/// <summary>Deserialize a set of diagnostics from a Clang diagnostics bitcode file.</summary>
		/// <param name="filename">The filename to load.</param>
		/// <param name="error">The error code result.</param>
		/// <param name="errorString">The error string.</param>
		/// <returns>The loaded <see cref="DiagnosticSet" />.</returns>
		public static DiagnosticSet Load(string filename, out LoadDiagError error, out string errorString)
		{
			var set = Clang.LoadDiagnostics(filename, out error, out var str);
			errorString = str.ToString();
			return set;
		}

		/// <summary>Retrieve a diagnostic associated with the given DiagnosticSet.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The specified <see cref="Diagnostic" />.</returns>
		public Diagnostic GetDiagnostic(uint index) => Clang.GetDiagnosticInSet(this, index);

		/// <summary>Retrieve a diagnostic associated with the given DiagnosticSet.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The specified <see cref="Diagnostic" />.</returns>
		public Diagnostic GetDiagnostic(int index) => Clang.GetDiagnosticInSet(this, Convert.ToUInt32(index));

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="DiagnosticSet" /> to <see cref="IntPtr" />
		///     .
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(DiagnosticSet instance) => instance._pointer;

		#endregion
	}
}