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
// CodeCompleteFlags.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	/// <summary>
	///     Flags that can be passed to  <see cref="Clang.CodeCompleteAt" /> to modify its behavior.
	///     <para>
	///         The enumerators in this enumeration can be bitwise-OR'd together to provide multiple
	///         options to <see cref="Clang.CodeCompleteAt" />.
	///     </para>
	/// </summary>
	[Flags]
	public enum CodeCompleteFlags
	{
		/// <summary>Whether to include macros within the set of code completions returned.</summary>
		IncludeMacros = 1,

		/// <summary>
		///     Whether to include code patterns for language constructs within the set of code
		///     completions, e.g., for loops.
		/// </summary>
		IncludeCodePatterns = 2,

		/// <summary>Whether to include brief documentation within the set of code completions returned.</summary>
		IncludeBriefComments = 4
	}
}