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
// TranslationUnit.cs created on 2018-09-22

#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LibClang
{
	public struct TranslationUnit : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="TranslationUnit" /> struct.</summary>
		/// <param name="address">The address of the struct in memory.</param>
		public TranslationUnit(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>
		///     Returns the set of flags that is suitable for parsing a translation unit that is being
		///     edited.
		/// </summary>
		/// <value>The default editing options.</value>
		public static uint DefaultEditingOptions => Clang.DefaultEditingTranslationUnitOptions();

		/// <summary>Gets a null (invalid) <see cref="TranslationUnit" />.</summary>
		/// <value>A null <see cref="TranslationUnit" />.</value>
		public static TranslationUnit Null => new TranslationUnit(IntPtr.Zero);

		/// <summary>Returns the set of flags that is suitable for reparsing a translation unit.</summary>
		/// <value>The default reparse options.</value>
		public uint DefaultReparseOptions => Clang.DefaultReparseOptions(this);

		/// <summary>Returns the set of flags that is suitable for saving a translation unit.</summary>
		/// <value>The default save options.</value>
		public uint DefaultSaveOptions => Clang.DefaultSaveOptions(this);

		/// <summary>Gets the diagnostics for this <see cref="TranslationUnit" />.</summary>
		/// <value>The diagnostics.</value>
		public IEnumerable<Diagnostic> Diagnostics
		{
			get
			{
				var count = Clang.GetNumDiagnostics(this);
				for (uint i = 0; i < count; i++)
					yield return Clang.GetDiagnostic(this, i);
			}
		}

		/// <summary>Determine the number of diagnostics produced for this translation unit.</summary>
		/// <value>The diagnostics count.</value>
		public int DiagnosticsCount => Convert.ToInt32(Clang.GetNumDiagnostics(this));

		/// <summary>Get the original translation unit source file name.</summary>
		/// <value>The spelling.</value>
		public string Spelling => ToString();

		/// <summary>
		///     Get target information for this translation unit.
		///     <para>
		///         The <see cref="TargetInfo" /> object cannot outlive the <see cref="TranslationUnit" />.
		///         object.
		///     </para>
		/// </summary>
		/// <value>The target information.</value>
		public TargetInfo TargetInfo => Clang.GetTranslationUnitTargetInfo(this);

		#endregion

		#region Methods

		/// <summary>
		///     Annotate the given set of tokens by providing cursors for each token that can be mapped to
		///     a specific entity within the abstract syntax tree.
		/// </summary>
		/// <param name="tokens">The tokens to annotate.</param>
		/// <returns>The cursors for each token.</returns>
		public Cursor[] AnnotateTokens(params Token[] tokens)
		{
			var cursors = new Cursor[tokens.Length];
			Clang.AnnotateTokens(this, tokens, (uint) tokens.Length, cursors);
			return cursors;
		}

		/// <summary>Perform code completion at a given location in a translation unit.</summary>
		/// <param name="filename">
		///     The name of the source file where code completion should be performed. This
		///     filename may be any file included in the translation unit.
		/// </param>
		/// <param name="line">The line at which code-completion should occur.</param>
		/// <param name="column">
		///     The column at which code-completion should occur. Note that the column should
		///     point just after the syntactic construct that initiated code completion, and not in the middle
		///     of a lexical token.
		/// </param>
		/// <param name="unsaved">
		///     The files that have not yet been saved to disk but may be required for
		///     parsing or code completion, including the contents of those files.
		/// </param>
		/// <param name="options">Extra options that control the behavior of code completion.</param>
		/// <returns>The code-completion results</returns>
		public CodeCompleteResults CodeCompleteAt(string filename, uint line, uint column, UnsavedFile[] unsaved,
			CodeCompleteFlags options) =>
			Clang.CodeCompleteAt(this, filename, line, column, unsaved, (uint) unsaved.Length, options);

		/// <summary>Perform code completion at a given location in a translation unit.</summary>
		/// <param name="filename">
		///     The name of the source file where code completion should be performed. This
		///     filename may be any file included in the translation unit.
		/// </param>
		/// <param name="line">The line at which code-completion should occur.</param>
		/// <param name="column">
		///     The column at which code-completion should occur. Note that the column should
		///     point just after the syntactic construct that initiated code completion, and not in the middle
		///     of a lexical token.
		/// </param>
		/// <param name="options">Extra options that control the behavior of code completion.</param>
		/// <returns>The code-completion results</returns>
		public CodeCompleteResults CodeCompleteAt(string filename, uint line, uint column, CodeCompleteFlags options) =>
			Clang.CodeCompleteAt(this, filename, line, column, new UnsavedFile[0], 0, options);

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.DisposeTranslationUnit(this);

		/// <summary>///
		///     <summary>Free the given set of tokens.</summary>
		/// </summary>
		/// <param name="tokens">The tokens to dispose.</param>
		public void DisposeTokens(params Token[] tokens)
		{
			Clang.DisposeTokens(this, tokens, Convert.ToUInt32(tokens.Length));
		}

		/// <summary>Find #import/#include directives in a specific file.</summary>
		/// <param name="file">The file to search for #import/#include directives. </param>
		/// <param name="visitor">
		///     The callback that will receive pairs of Cursor/CXSourceRange for each
		///     directive found.
		/// </param>
		/// <returns>The result of the enumerators.</returns>
		public Result FindIncludesInFile(File file, CursorAndRangeVisitor visitor) =>
			Clang.FindIncludesInFile(this, file, visitor);

		/// <summary>
		///     Retrieve all ranges from all files that were skipped by the preprocessor. The preprocessor
		///     will skip lines when they are surrounded by an if/ifdef/ifndef directive whose condition does
		///     not evaluate to true.
		/// </summary>
		/// <returns>The list of skipped ranges.</returns>
		public SourceRangeList GetAllSkippedRanges() => Clang.GetAllSkippedRanges(this);

		/// <summary>
		///     Retrieve the cursor that represents the given translation unit.
		///     <para>
		///         The translation unit cursor can be used to start traversing the various declarations
		///         within the given translation unit.
		///     </para>
		/// </summary>
		/// <returns></returns>
		public Cursor GetCursor() => Clang.GetTranslationUnitCursor(this);

		/// <summary>
		///     Map a source location to the cursor that describes the entity at that location in the
		///     source code.
		/// </summary>
		/// <param name="location">The location.</param>
		/// <returns>The cursor the specified location.</returns>
		public Cursor GetCursor(SourceLocation location) => Clang.GetCursor(this, location);

		/// <summary>Retrieve a diagnostic associated with the <see cref="TranslationUnit" />.</summary>
		/// <param name="index">The zero-based diagnostic number to retrieve.</param>
		/// <returns>The requested diagnostic</returns>
		public Diagnostic GetDiagnostic(uint index) => Clang.GetDiagnostic(this, index);

		/// <summary>Retrieve a diagnostic associated with the <see cref="TranslationUnit" />.</summary>
		/// <param name="index">The zero-based diagnostic number to retrieve.</param>
		/// <returns>The requested diagnostic</returns>
		public Diagnostic GetDiagnostic(int index) => Clang.GetDiagnostic(this, Convert.ToUInt32(index));

		/// <summary>Retrieve the complete set of diagnostics associated with a translation unit.</summary>
		/// <returns>The associated <see cref="DiagnosticSet" />.</returns>
		public DiagnosticSet GetDiagnosticSet() => Clang.GetDiagnosticSetFromTU(this);

		/// <summary>Retrieve a file handle within this translation unit.</summary>
		/// <param name="filename">The name of the file to retrieve.</param>
		/// <returns>The file.</returns>
		public File GetFile(string filename) => Clang.GetFile(this, filename);

		/// <summary>Gets the file contents.</summary>
		/// <param name="file">The file.</param>
		/// <returns>The contents of the file.</returns>
		public string GetFileContents(File file) => System.IO.File.ReadAllText(file.Name);

		/// <summary>Visit the set of preprocessor inclusions in a translation unit.</summary>
		/// <param name="visitor">
		///     The visitor function to be called with the provided data for every included
		///     file.
		/// </param>
		public void GetInclusions(InclusionVisitor visitor) => GetInclusions(visitor, ClientData.Null);

		/// <summary>Visit the set of preprocessor inclusions in a translation unit.</summary>
		/// <param name="visitor">
		///     The visitor function to be called with the provided data for every included
		///     file.
		/// </param>
		/// <param name="data">Any client data to be passed along as an argument.</param>
		public void GetInclusions(InclusionVisitor visitor, ClientData data) =>
			Clang.GetInclusions(this, visitor, data);

		/// <summary>
		///     Retrieves the source location associated with a given file/line/column in a particular
		///     translation unit.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="line">The line number.</param>
		/// <param name="column">The column number.</param>
		/// <returns>The location</returns>
		public SourceLocation GetLocation(File file, uint line, uint column) =>
			Clang.GetLocation(this, file, line, column);


		/// <summary>
		///     Retrieves the source location associated with a given file/line/column in a particular
		///     translation unit.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="line">The line number.</param>
		/// <param name="column">The column number.</param>
		/// <returns>The location</returns>
		public SourceLocation GetLocation(File file, int line, int column) =>
			GetLocation(file, (uint) line, (uint) column);

		/// <summary>Given a file header file, return the module that contains it, if one exists.</summary>
		/// <param name="file">The file header.</param>
		/// <returns>The <see cref="Module" /> if one exists.</returns>
		public Module GetModule(File file) => Clang.GetModuleForFile(this, file);

		/// <summary>a module object. the number of top level headers associated with this module.</summary>
		/// <param name="module">The module to query.</param>
		/// <returns>The number of top-level headers.</returns>
		public int GetModuleTopLevelHeadersCount(Module module) =>
			Convert.ToInt32(Clang.ModuleGetNumTopLevelHeaders(this, module));

		/// <summary>Return the memory usage of a translation unit.</summary>
		/// <returns>The resource usage.</returns>
		public TUResourceUsage GetResourceUsage() => Clang.GetCXTUResourceUsage(this);

		/// <summary>Retrieve a source range that covers the given token.</summary>
		/// <param name="token">The token to query.</param>
		/// <returns>The extent of the token.</returns>
		public SourceRange GetTokenExtent(Token token) => Clang.GetTokenExtent(this, token);

		/// <summary>Retrieve the source location of the given token.</summary>
		/// <param name="token">The token to get location of.</param>
		/// <returns>The location of the token.</returns>
		public SourceLocation GetTokenLocation(Token token) => Clang.GetTokenLocation(this, token);

		/// <summary>
		///     Determine the spelling of the given token. The spelling of a token is the textual
		///     representation of that token, e.g., the text of an identifier or keyword.
		/// </summary>
		/// <param name="token">The token to get spelling of.</param>
		/// <returns>The token spelling</returns>
		public string GetTokenSpelling(Token token) => Clang.GetTokenSpelling(this, token);

		/// <summary>Gets the top level header for a module.</summary>
		/// <param name="module">The module to query.</param>
		/// <param name="index">The top level header index (zero-based).</param>
		/// <returns>The specified top level header associated with the module.</returns>
		public File GetTopLevelHeader(Module module, uint index) => Clang.ModuleGetTopLevelHeader(this, module, index);

		/// <summary>Gets the top level header for a module.</summary>
		/// <param name="module">The module to query.</param>
		/// <param name="index">The top level header index (zero-based).</param>
		/// <returns>The specified top level header associated with the module.</returns>
		public File GetTopLevelHeader(Module module, int index) =>
			Clang.ModuleGetTopLevelHeader(this, module, Convert.ToUInt32(index));

		/// <summary>
		///     Determine whether the given header is guarded against multiple inclusions, either with the
		///     conventional # ifndef/ # define/ # endif macro guards or with # pragma once.
		/// </summary>
		/// <param name="file">The file to query</param>
		/// <returns><c>true</c> if file is include guarded, otherwise <c>false</c>.</returns>
		public bool IsFileIncludeGuarded(File file) => Clang.IsFileMultipleIncludeGuarded(this, file);

		/// <summary>
		///     Retrieves the source location associated with a given character offset in this translation
		///     unit.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="offset">The offset.</param>
		/// <returns>The location.</returns>
		public SourceLocation LocationOffset(File file, uint offset) => Clang.GetLocationForOffset(this, file, offset);

		/// <summary>Reparse the source files that produced this translation unit.</summary>
		/// <param name="options">Options to apply.</param>
		/// <param name="unsaved">Unsaved files to pass.</param>
		/// <returns>The result code.</returns>
		public ErrorCode Reparse(ReparseFlags options, UnsavedFile[] unsaved) =>
			Clang.ReparseTranslationUnit(this, (uint) unsaved.Length, unsaved, options);

		/// <summary>Reparse the source files that produced this translation unit.</summary>
		/// <param name="options">Options to apply.</param>
		/// <returns>The result code.</returns>
		public ErrorCode Reparse(ReparseFlags options) => Reparse(options, new UnsavedFile[0]);

		/// <summary>
		///     Saves a translation unit into a serialized representation of that translation unit on
		///     disk. Any translation unit that was parsed without error can be saved into a file.
		/// </summary>
		/// <param name="filename">The filename to save to.</param>
		/// <param name="options">The options to apply.</param>
		/// <returns>The result code.</returns>
		public SaveError Save(string filename, SaveTranslationUnitFlags options) =>
			Clang.SaveTranslationUnit(this, filename, options);

		/// <summary>
		///     Retrieve all ranges that were skipped by the preprocessor. The preprocessor will skip
		///     lines when they are surrounded by an if/ifdef/ifndef directive whose condition does not
		///     evaluate to true.
		/// </summary>
		/// <param name="file">The file to query.</param>
		/// <returns>The list of skipped ranges.</returns>
		public SourceRangeList SkippedRanges(File file) => Clang.GetSkippedRanges(this, file);

		/// <summary>
		///     Suspend a translation unit in order to free memory associated with it.
		///     <para>A suspended translation unit uses significantly less memory.</para>
		/// </summary>
		/// <returns></returns>
		public uint Suspend() => Clang.SuspendTranslationUnit(this);

		/// <summary>Tokenize the source code described by the given range into raw lexical tokens.</summary>
		/// <param name="range">The source range in which text should be tokenized.</param>
		/// <returns>The tokens within the range.</returns>
		public Token[] Tokenize(SourceRange range)
		{
			Clang.Tokenize(this, range, out var ptr, out var count);
			var tokens = new Token[count];
			var size = Marshal.SizeOf<Token>();
			for (var i = 0; i < count; i++)
				tokens[i] = Marshal.PtrToStructure<Token>(ptr + i * size);
			return tokens;
		}

		/// <summary>Gets the top-level headers for a <see cref="Module" />.</summary>
		/// <param name="module">The module to query.</param>
		/// <returns>The top-level headers.</returns>
		public IEnumerable<File> TopLevelHeaders(Module module)
		{
			var count = Clang.ModuleGetNumTopLevelHeaders(this, module);
			for (uint i = 0; i < count; i++)
				yield return Clang.ModuleGetTopLevelHeader(this, module, i);
		}

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Clang.GetTranslationUnitSpelling(this);

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="TranslationUnit" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="unit">The <see cref="TranslationUnit" /> instance to be converted.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(TranslationUnit unit) => unit._pointer;

		#endregion
	}
}