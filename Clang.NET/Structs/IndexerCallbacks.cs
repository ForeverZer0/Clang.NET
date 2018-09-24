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
// IndexerCallbacks.cs created on 2018-09-23

#endregion

using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     A group of callbacks used by <see cref="Clang.IndexSourceFile" /> and
	///     <see cref="Clang.IndexTranslationUnit" />.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct IndexerCallbacks
	{
		/// <summary>Called periodically to check whether indexing should be aborted.</summary>
		public AbortQueryCallback AbortQuery;

		/// <summary>Called at the end of indexing; passes the complete diagnostic set.</summary>
		public DiagnosticCallback Diagnostic;

		/// <summary>Called when the main file is entered.</summary>
		public EnterMainFileCallback EnteredMainFile;

		/// <summary>Called when a file gets #included/#imported</summary>
		public IncludedFileCallback IncludedFile;

		/// <summary>Called when a AST file(PCH or module) gets imported.</summary>
		public ImportedASTFileCallback ImportedASTFile;

		/// <summary>Called at the beginning of indexing a translation unit.</summary>
		public StartedTranslationUnitCallback StartedTranslationUnit;

		/// <summary>Called when a declaration is encountered.</summary>
		public IndexDeclarationCallback IndexDeclaration;

		/// <summary>Called to index a reference of an entity.</summary>
		public IndexEntityReferenceCallback IndexEntityReference;
	}
}