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
// TUResourceUsageKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Categorizes how memory is being used by a translation unit.</summary>
	public enum TUResourceUsageKind
	{
		/// <summary></summary>
		AST = 1,

		/// <summary></summary>
		Identifiers = 2,

		/// <summary></summary>
		Selectors = 3,

		/// <summary></summary>
		GlobalCompletionResults = 4,

		/// <summary></summary>
		SourceManagerContentCache = 5,

		/// <summary></summary>
		ASTSideTables = 6,

		/// <summary></summary>
		SourceManagerMembufferMalloc = 7,

		/// <summary></summary>
		SourceManagerMembufferMMap = 8,

		/// <summary></summary>
		ExternalASTSourceMembufferMalloc = 9,

		/// <summary></summary>
		ExternalASTSourceMembufferMMap = 10,

		/// <summary></summary>
		Preprocessor = 11,

		/// <summary></summary>
		PreprocessingRecord = 12,

		/// <summary></summary>
		SourceManagerDataStructures = 13,

		/// <summary></summary>
		PreprocessorHeaderSearch = 14,

		/// <summary></summary>
		MEMORYINBYTESBEGIN = 1,

		/// <summary></summary>
		MEMORYINBYTESEND = 14,

		/// <summary></summary>
		First = 1,

		/// <summary></summary>
		Last = 14
	}
}