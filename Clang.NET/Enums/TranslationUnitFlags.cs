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
// TranslationUnitFlags.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     Flags that control the creation of translation units. The enumerators in this enumeration
	///     type are meant to be bitwise ORed together to specify which options should be used when
	///     constructing the translation unit.
	/// </summary>
	public enum TranslationUnitFlags
	{
		/// <summary>Used to indicate that no special translation-unit options are needed.</summary>
		None = 0,

		/// <summary>
		///     Used to indicate that the parser should construct a "detailed" preprocessing record,
		///     including all macro definitions and instantiations. Constructing a detailed preprocessing
		///     record requires more memory and time to parse, since the information contained in the record is
		///     usually not retained. However, it can be useful for applications that require more detailed
		///     information about the behavior of the preprocessor.
		/// </summary>
		DetailedPreprocessingRecord = 1,

		/// <summary>
		///     Used to indicate that the translation unit is incomplete. When a translation unit is
		///     considered "incomplete", semantic analysis that is typically performed at the end of the
		///     translation unit will be suppressed. For example, this suppresses the completion of tentative
		///     declarations in C and of instantiation of implicitly-instantiation function templates in C++.
		///     This option is typically used when parsing a header with the intent of producing a precompiled
		///     header.
		/// </summary>
		Incomplete = 2,

		/// <summary>
		///     Used to indicate that the translation unit should be built with an implicit precompiled
		///     header for the preamble. An implicit precompiled header is used as an optimization when a
		///     particular translation unit is likely to be reparsed many times when the sources aren't
		///     changing that often. In this case, an implicit precompiled header will be built containing all
		///     of the initial includes at the top of the main file (what we refer to as the "preamble" of the
		///     file). In subsequent parses, if the preamble or the files in it have not changed,
		///     clang_reparseTranslationUnit() will re-use the implicit precompiled header to improve parsing
		///     performance.
		/// </summary>
		PrecompiledPreamble = 4,

		/// <summary>
		///     Used to indicate that the translation unit should cache some code-completion results with
		///     each reparse of the source file. Caching of code-completion results is a performance
		///     optimization that introduces some overhead to reparsing but improves the performance of
		///     code-completion operations.
		/// </summary>
		CacheCompletionResults = 8,

		/// <summary>
		///     Used to indicate that the translation unit will be serialized with
		///     clang_saveTranslationUnit. This option is typically used when parsing a header with the intent
		///     of producing a precompiled header.
		/// </summary>
		ForSerialization = 16,

		/// <summary>
		///     DEPRECATED: Enabled chained precompiled preambles in C++. Note: this is a *temporary*
		///     option that is available only while we are testing C++ precompiled preamble support. It is
		///     deprecated.
		/// </summary>
		CXXChainedPCH = 32,

		/// <summary>
		///     Used to indicate that function/method bodies should be skipped while parsing. This option
		///     can be used to search for declarations/definitions while ignoring the usages.
		/// </summary>
		SkipFunctionBodies = 64,

		/// <summary>
		///     Used to indicate that brief documentation comments should be included into the set of code
		///     completions returned from this translation unit.
		/// </summary>
		IncludeBriefCommentsInCodeCompletion = 128,

		/// <summary>
		///     Used to indicate that the precompiled preamble should be created on the first parse.
		///     Otherwise it will be created on the first reparse. This trades runtime on the first parse
		///     (serializing the preamble takes time) for reduced runtime on the second parse (can now reuse
		///     the preamble).
		/// </summary>
		CreatePreambleOnFirstParse = 256,

		/// <summary>
		///     Do not stop processing when fatal errors are encountered. When fatal errors are
		///     encountered while parsing a translation unit, semantic analysis is typically stopped early when
		///     compiling code. A common source for fatal errors are unresolvable include files. For the
		///     purposes of an IDE, this is undesirable behavior and as much information as possible should be
		///     reported. Use this flag to enable this behavior.
		/// </summary>
		KeepGoing = 512,

		/// <summary>Sets the preprocessor in a mode for parsing a single file only.</summary>
		SingleFileParse = 1024
	}
}