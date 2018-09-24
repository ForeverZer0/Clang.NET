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
// Comment.cs created on 2018-09-21

#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>A parsed comment.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Comment
	{
		public readonly IntPtr AstNode; // const void *
		public readonly TranslationUnit TranslationUnit;

		#region Properties & Indexers

		/// <summary>Gets the children comments.</summary>
		/// <value>The children.</value>
		public IEnumerable<Comment> Children
		{
			get
			{
				var count = Clang.CommentGetNumChildren(this);
				for (uint i = 0; i < count; i++)
					yield return Clang.CommentGetChild(this, i);
			}
		}

		/// <summary>Gets the number of children comments.</summary>
		/// <value>The children count.</value>
		public int ChildrenCount => Convert.ToInt32(Clang.CommentGetNumChildren(this));

		/// <summary>Gets the parameter passing direction.</summary>
		/// <value>The direction.</value>
		public CommentParamPassDirection Direction => Clang.ParamCommandCommentGetDirection(this);

		/// <summary>Gets the HTML attribute count.</summary>
		/// <value>The HTML attribute count.</value>
		public int HtmlAttrCount => Convert.ToInt32(Clang.HTMLStartTagGetNumAttrs(this));

		/// <summary>Gets the inline argument count.</summary>
		/// <value>The inline argument count.</value>
		public int InlineArgumentCount => Convert.ToInt32(Clang.InlineCommandCommentGetNumArgs(this));

		/// <summary>Gets the name of the inline command.</summary>
		/// <value>The name of the inline command.</value>
		public string InlineCommandName => Clang.InlineCommandCommentGetCommandName(this);

		/// <summary>
		///     <c>true</c> if <see cref="Comment" /> is inline content and has a newline immediately
		///     following it in the comment text.
		///     <para>Newlines between paragraphs do not count.</para>
		/// </summary>
		/// <value><c>true</c> if inline comment has trailing newline; otherwise, <c>false</c>.</value>
		public bool InlineCommentHasTrailingNewline => Clang.InlineContentCommentHasTrailingNewline(this);

		/// <summary>Gets the most appropriate rendering mode, chosen on command semantics in Doxygen.</summary>
		/// <value>The kind of the inline render kind.</value>
		public CommentInlineCommandRenderKind InlineRenderKind => Clang.InlineCommandCommentGetRenderKind(this);

		/// <summary>Gets a value indicating whether this instance is direction explicit.</summary>
		/// <value><c>true</c> if this instance is direction explicit; otherwise, <c>false</c>.</value>
		public bool IsDirectionExplicit => Clang.ParamCommandCommentIsDirectionExplicit(this);

		/// <summary>Gets a value indicating whether this instance is parameter index valid.</summary>
		/// <value><c>true</c> if this instance is parameter index valid; otherwise, <c>false</c>.</value>
		public bool IsParamIndexValid => Clang.ParamCommandCommentIsParamIndexValid(this);

		/// <summary>Gets a value indicating whether this TParam is parameter position valid.</summary>
		/// <value><c>true</c> if this instance is parameter position valid; otherwise, <c>false</c>.</value>
		public bool IsParamPositionValid => Clang.TParamCommandCommentIsParamPositionValid(this);

		/// <summary>Gets a value indicating whether this instance is self closing HTML tag.</summary>
		/// <value><c>true</c> if this instance is self closing HTML tag; otherwise, <c>false</c>.</value>
		public bool IsSelfClosingHtmlTag => Clang.HTMLStartTagCommentIsSelfClosing(this);

		/// <summary>
		///     A CommentParagraph node is considered whitespace if it contains only text nodes that are
		///     empty or whitespace.
		/// </summary>
		/// <value><c>true</c> if this instance is whitespace; otherwise, <c>false</c>.</value>
		public bool IsWhitespace => Clang.CommentIsWhitespace(this);

		/// <summary>Gets the kind of the comment.</summary>
		/// <value>The kind.</value>
		public CommentKind Kind => Clang.CommentGetKind(this);

		#endregion

		#region Methods

		/// <summary>Gets the number of word-like arguments</summary>
		/// <returns>The argument count.</returns>
		public int GetArgumentCount() => Convert.ToInt32(Clang.BlockCommandCommentGetNumArgs(this));

		/// <summary>Gets the block command argument text of the specified word-like argument.</summary>
		/// <param name="index">The index of the argument.</param>
		/// <returns>The associated text.</returns>
		public string GetBlockCommandArgText(uint index) => Clang.BlockCommandCommentGetArgText(this, index);

		/// <summary>Gets the block command argument text of the specified word-like argument.</summary>
		/// <param name="index">The index of the argument.</param>
		/// <returns>The associated text.</returns>
		public string GetBlockCommandArgText(int index) => GetBlockCommandArgText(Convert.ToUInt32(index));

		/// <summary>Gets the block command paragraph argument of the block command</summary>
		/// <returns>The paragraph.</returns>
		public Comment GetBlockCommandParagraph() => Clang.BlockCommandCommentGetParagraph(this);

		/// <summary>Gets the child comment at the specified index.</summary>
		/// <param name="index">The index of the child comment to retrieve.</param>
		/// <returns>The child comment</returns>
		public Comment GetChildAt(uint index) => Clang.CommentGetChild(this, index);

		/// <summary>Gets the child comment at the specified index.</summary>
		/// <param name="index">The index of the child comment to retrieve.</param>
		/// <returns>The child comment</returns>
		public Comment GetChildAt(int index) => Clang.CommentGetChild(this, Convert.ToUInt32(index));

		/// <summary>Gets the name of the block command.</summary>
		/// <returns>The name of the block command.</returns>
		public string GetCommandName() => Clang.BlockCommandCommentGetCommandName(this);

		/// <summary>Gets the name of the HTML tag attribute.</summary>
		/// <param name="index">The index of the attribute.</param>
		/// <returns>The name of the specified attribute.</returns>
		public string GetHtmlTagAttrName(uint index) => Clang.HTMLStartTagGetAttrName(this, index);

		/// <summary>Gets the name of the HTML tag attribute.</summary>
		/// <param name="index">The index of the attribute.</param>
		/// <returns>The name of the specified attribute.</returns>
		public string GetHtmlTagAttrName(int index) => Clang.HTMLStartTagGetAttrName(this, Convert.ToUInt32(index));

		/// <summary>Gets the value of the HTML tag attribute.</summary>
		/// <param name="index">The index of the attribute.</param>
		/// <returns>The value of the specified attribute.</returns>
		public string GetHtmlTagAttrValue(uint index) => Clang.HTMLStartTagGetAttrValue(this, index);

		/// <summary>Gets the value of the HTML tag attribute.</summary>
		/// <param name="index">The index of the attribute.</param>
		/// <returns>The value of the specified attribute.</returns>
		public string GetHtmlTagAttrValue(int index) => Clang.HTMLStartTagGetAttrValue(this, Convert.ToUInt32(index));

		/// <summary>Gets the name of the HTML tag.</summary>
		/// <returns>The HTML tag name.</returns>
		public string GetHtmlTagName() => Clang.HTMLTagCommentGetTagName(this);

		/// <summary>Gets the inline argument text.</summary>
		/// <param name="index">The specified index to retrieve.</param>
		/// <returns>The argument text.</returns>
		public string GetInlineArgText(uint index) => Clang.InlineCommandCommentGetArgText(this, index);

		/// <summary>Gets the inline argument text.</summary>
		/// <param name="index">The specified index to retrieve.</param>
		/// <returns>The argument text.</returns>
		public string GetInlineArgText(int index) => GetInlineArgText(Convert.ToUInt32(index));

		/// <summary>Gets the parameter depth.</summary>
		/// <returns></returns>
		public uint GetParamDepth() => Clang.TParamCommandCommentGetDepth(this);

		/// <summary>Gets the index of the parameter.</summary>
		/// <param name="depth">The depth.</param>
		/// <returns>The index.</returns>
		public uint GetParamIndex(uint depth) => Clang.TParamCommandCommentGetIndex(this, depth);

		/// <summary>Gets the index of the parameter.</summary>
		/// <returns>The index.</returns>
		public uint GetParamIndex() => Clang.ParamCommandCommentGetParamIndex(this);

		/// <summary>Gets the name of the template parameter.</summary>
		/// <returns>The name.</returns>
		public string GetParamName() => Clang.TParamCommandCommentGetParamName(this);

		/// <summary>Gets the parameter text.</summary>
		/// <returns>The text.</returns>
		public string GetParamText() => Clang.ParamCommandCommentGetParamName(this);

		/// <summary>Gets the text for.</summary>
		/// <returns>The text.</returns>
		public string GetText() => Clang.TextCommentGetText(this);

		/// <summary>Gets the verbatim block text.</summary>
		/// <returns></returns>
		public string GetVerbatimBlockText() => Clang.VerbatimBlockLineCommentGetText(this);

		/// <summary>Gets the verbatim line text.</summary>
		/// <returns></returns>
		public string GetVerbatimLineText() => Clang.VerbatimLineCommentGetText(this);

		/// <summary>Convert an HTML tag AST node to string.</summary>
		/// <returns>The HTML string.</returns>
		public string HtmlTagToString() => Clang.HTMLTagCommentGetAsString(this);

		/// <summary>Convert a given full parsed comment to an HTML fragment.</summary>
		/// <returns>An HTML string.</returns>
		public string ToHtml() => Clang.FullCommentGetAsHTML(this);

		/// <summary>Convert a given full parsed comment to an XML document.</summary>
		/// <returns>An XML string.</returns>
		public string ToXml() => Clang.FullCommentGetAsXML(this);

		#endregion
	}
}