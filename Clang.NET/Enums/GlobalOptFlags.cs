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
// GlobalOptFlags.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary></summary>
	public enum GlobalOptFlags
	{
		/// <summary>Used to indicate that no special CXIndex options are needed.</summary>
		None = 0,

		/// <summary>
		///     Used to indicate that threads that LibClang creates for indexing purposes should use
		///     background priority. Affects #clang_indexSourceFile, #clang_indexTranslationUnit,
		///     #clang_parseTranslationUnit, #clang_saveTranslationUnit.
		/// </summary>
		ThreadBackgroundPriorityForIndexing = 1,

		/// <summary>
		///     Used to indicate that threads that LibClang creates for editing purposes should use
		///     background priority. Affects #clang_reparseTranslationUnit, #clang_codeCompleteAt,
		///     #clang_annotateTokens
		/// </summary>
		ThreadBackgroundPriorityForEditing = 2,

		/// <summary>Used to indicate that all threads that LibClang creates should use background priority.</summary>
		ThreadBackgroundPriorityForAll = 3
	}
}