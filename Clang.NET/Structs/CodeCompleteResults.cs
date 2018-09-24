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
// CodeCompleteResults.cs created on 2018-09-21

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     Contains the results of code-completion. This data structure contains the results of code
	///     completion, as produced by clang_codeCompleteAt(). Its contents must be freed by
	///     clang_disposeCodeCompleteResults.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct CodeCompleteResults : IEnumerable<CompletionResult>, IDisposable
	{
		private readonly IntPtr _results;
		private readonly uint _count;

		#region Properties & Indexers

		/// <summary>The number of code-completion results stored in the results array.</summary>
		public int Count => Convert.ToInt32(_count);

		/// <summary>Gets the diagnostics.</summary>
		/// <value>The diagnostics.</value>
		public IEnumerable<Diagnostic> Diagnostics
		{
			get
			{
				var count = Clang.CodeCompleteGetNumDiagnostics(ref this);
				for (uint i = 0; i < count; i++)
					yield return Clang.CodeCompleteGetDiagnostic(ref this, i);
			}
		}

		/// <summary>
		///     Determine the number of diagnostics produced prior to the location where code completion
		///     was performed.
		/// </summary>
		/// <value>The diagnostics count.</value>
		public int DiagnosticsCount => Convert.ToInt32(Clang.CodeCompleteGetNumDiagnostics(ref this));

		/// <summary>Gets the Objective-C selector.</summary>
		/// <value>The Objective-C selector.</value>
		public string ObjCSelector => Clang.CodeCompleteGetObjCSelector(ref this);

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeCodeCompleteResults(ref this);

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region IEnumerable<CompletionResult> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<CompletionResult> GetEnumerator()
		{
			var size = Marshal.SizeOf<CompletionResult>();
			for (var i = 0; i < _count; i += size)
				yield return Marshal.PtrToStructure<CompletionResult>(_results + i);
		}

		#endregion

		#region Methods

		/// <summary>Returns the cursor kind for the container for the current code completion context.</summary>
		/// <param name="incomplete"><c>true</c> if information is incomplete, otherwise <c>false</c>.</param>
		/// <returns>The container kind.</returns>
		public CursorKind GetContainerKind(out bool incomplete) =>
			Clang.CodeCompleteGetContainerKind(ref this, out incomplete);

		/// <summary>
		///     Returns the USR for the container for the current code completion context. If there is not
		///     a container for the current context, this function will return the empty string. the code
		///     completion results to query the USR for the container
		/// </summary>
		/// <returns>The USR string.</returns>
		public string GetContainerUSR() => Clang.CodeCompleteGetContainerUSR(ref this);

		/// <summary>Determines what completions are appropriate for the context the given code completion.</summary>
		/// <returns>
		///     The kinds of completions that are appropriate for use along with the given code completion
		///     results.
		/// </returns>
		public ulong GetContexts() => Clang.CodeCompleteGetContexts(ref this);

		/// <summary>Retrieve a diagnostic associated with the this code completion.</summary>
		/// <param name="index">The zero-based diagnostic number to retrieve.</param>
		/// <returns>The requested diagnostic.</returns>
		public Diagnostic GetDiagnostic(uint index) => Clang.CodeCompleteGetDiagnostic(ref this, index);

		/// <summary>Retrieve a diagnostic associated with the this code completion.</summary>
		/// <param name="index">The zero-based diagnostic number to retrieve.</param>
		/// <returns>The requested diagnostic.</returns>
		public Diagnostic GetDiagnostic(int index) =>
			Clang.CodeCompleteGetDiagnostic(ref this, Convert.ToUInt32(index));

		#endregion
	}
}