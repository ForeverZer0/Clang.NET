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
// Diagnostic.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct Diagnostic : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="Diagnostic" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public Diagnostic(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Gets the null (invalid) <see cref="Diagnostic" />.</summary>
		/// <value>A null <see cref="Diagnostic" />.</value>
		public static Diagnostic Null => new Diagnostic(IntPtr.Zero);

		/// <summary>Retrieve the category number for this diagnostic.</summary>
		/// <value>The category number.</value>
		public uint Category => Clang.GetDiagnosticCategory(this);

		/// <summary>Retrieve the diagnostic category text for a given diagnostic.</summary>
		/// <value>The text of the given diagnostic category.</value>
		public string CategoryText => Clang.GetDiagnosticCategoryText(this);

		/// <summary>Retrieve the child diagnostics of <see cref="Diagnostic" />.</summary>
		/// <value>The child diagnostics.</value>
		public DiagnosticSet Children => Clang.GetChildDiagnostics(this);

		/// <summary>Determine the number of fix-it hints associated with the given diagnostic.</summary>
		/// <value>The fix it count.</value>
		public int FixItCount => Convert.ToInt32(Clang.GetDiagnosticNumFixIts(this));

		/// <summary>
		///     Retrieve the source location of the given diagnostic.
		///     <para>
		///         This location is where Clang would print the caret ('^') when displaying the diagnostic
		///         on the command line.
		///     </para>
		/// </summary>
		/// <value>The location.</value>
		public SourceLocation Location => Clang.GetDiagnosticLocation(this);

		/// <summary>Determine the number of source ranges associated with the given diagnostic.</summary>
		/// <value>The range count.</value>
		public int RangeCount => Convert.ToInt32(Clang.GetDiagnosticNumRanges(this));

		/// <summary>Determine the severity of the given diagnostic.</summary>
		/// <value>The severity.</value>
		public DiagnosticSeverity Severity => Clang.GetDiagnosticSeverity(this);

		/// <summary>Retrieve the text of the given diagnostic.</summary>
		/// <value>The spelling.</value>
		public string Spelling => ToString();

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeDiagnostic(this);

		#endregion

		#region Methods

		/// <summary>
		///     Format the given diagnostic in a manner that is suitable for display. This routine will
		///     format the given diagnostic to a string, rendering the diagnostic according to the various
		///     options given.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns>The formatted diagnostic string.</returns>
		public string Format(DiagnosticDisplayOptions options) => Clang.FormatDiagnostic(this, options);

		/// <summary>Retrieve the name of the command-line option that enabled this diagnostic.</summary>
		/// <param name="disable">
		///     The string that will be set to the option that disables this diagnostic (if
		///     any). A string that contains the command-line option used to enable this warning, such as
		///     "-Wconversion" or "-pedantic".
		/// </param>
		/// <returns>The option string.</returns>
		public string GetDiagnosticOption(ref String disable) => Clang.GetDiagnosticOption(this, ref disable);

		/// <summary>Retrieve the name of the command-line option that enabled this diagnostic.</summary>
		/// <returns>The option string.</returns>
		public string GetDiagnosticOption() => Clang.GetDiagnosticOption(this, IntPtr.Zero);

		/// <summary>
		///     Retrieve the replacement information for a given fix-it. Fix-its are described in terms of
		///     a source range whose contents should be replaced by a string.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <param name="range">The range of text to replace.</param>
		/// <returns>The fix-it string.</returns>
		public string GetFixIt(uint index, out SourceRange range) => Clang.GetDiagnosticFixIt(this, index, out range);

		/// <summary>
		///     Retrieve the replacement information for a given fix-it. Fix-its are described in terms of
		///     a source range whose contents should be replaced by a string.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <param name="range">The range of text to replace.</param>
		/// <returns>The fix-it string.</returns>
		public string GetFixIt(int index, out SourceRange range) => GetFixIt((uint) index, out range);

		/// <summary>Retrieve a source range associated with the diagnostic.</summary>
		/// <param name="index">The zero-based index specifying which range to the requested source range.</param>
		/// <returns>The specified <see cref="SourceRange" />.</returns>
		public SourceRange GetRange(uint index) => Clang.GetDiagnosticRange(this, index);

		/// <summary>Retrieve a source range associated with the diagnostic.</summary>
		/// <param name="index">The zero-based index specifying which range to the requested source range.</param>
		/// <returns>The specified <see cref="SourceRange" />.</returns>
		public SourceRange GetRange(int index) => Clang.GetDiagnosticRange(this, Convert.ToUInt32(index));

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Clang.GetDiagnosticSpelling(this);

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="Diagnostic" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(Diagnostic instance) => instance._pointer;

		#endregion
	}
}