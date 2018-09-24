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
// CompletionChunkKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     Describes a single piece of text within a code-completion string. Each "chunk" within a
	///     code-completion string ( CXCompletionString) is either a piece of text with a specific "kind"
	///     that describes how that text should be interpreted by the client or is another completion
	///     string.
	/// </summary>
	public enum CompletionChunkKind
	{
		/// <summary>
		///     A code-completion string that describes "optional" text that could be a part of the
		///     template (but is not required).
		/// </summary>
		Optional = 0,

		/// <summary>Text that a user would be expected to type to get this code-completion result.</summary>
		TypedText = 1,

		/// <summary>
		///     Text that should be inserted as part of a code-completion result.
		///     <para>
		///         A "text" chunk represents text that is part of the template to be inserted into user code
		///         should this particular code-completion result be selected.
		///     </para>
		/// </summary>
		Text = 2,

		/// <summary>Placeholder text that should be replaced by the user.</summary>
		Placeholder = 3,

		/// <summary>Informative text that should be displayed but never inserted as</summary>
		Informative = 4,

		/// <summary>
		///     Text that describes the current parameter when code-completion is referring to function
		///     call, message send, or template specialization.
		/// </summary>
		CurrentParameter = 5,

		/// <summary>
		///     A left parenthesis ('('), used to initiate a function call or signal the beginning of a
		///     function parameter list.
		/// </summary>
		LeftParen = 6,

		/// <summary>
		///     A right parenthesis (')'), used to finish a function call or signal the end of a function
		///     parameter list.
		/// </summary>
		RightParen = 7,

		/// <summary>A left bracket ('[').</summary>
		LeftBracket = 8,

		/// <summary>A right bracket (']').</summary>
		RightBracket = 9,

		/// <summary>A left brace ('{').</summary>
		LeftBrace = 10,

		/// <summary>A right brace ('}').</summary>
		RightBrace = 11,

		/// <summary>A left angle bracket</summary>
		LeftAngle = 12,

		/// <summary>A right angle bracket</summary>
		RightAngle = 13,

		/// <summary>A comma separator (',').</summary>
		Comma = 14,

		/// <summary>Text that specifies the result type of a given result.</summary>
		ResultType = 15,

		/// <summary>A colon (':').</summary>
		Colon = 16,

		/// <summary>A semicolon (';').</summary>
		SemiColon = 17,

		/// <summary>An '=' sign.</summary>
		Equal = 18,

		/// <summary>Horizontal space (' ').</summary>
		HorizontalSpace = 19,

		/// <summary>Vertical space</summary>
		VerticalSpace = 20
	}
}