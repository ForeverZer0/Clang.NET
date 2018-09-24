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
// DiagnosticDisplayOptions.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	/// <summary>
	///     Options to control the display of diagnostics. The values in this enum are meant to be
	///     combined to customize the behavior of <see cref="Clang.FormatDiagnostic" />.
	/// </summary>
	[Flags]
	public enum DiagnosticDisplayOptions
	{
		/// <summary>Display the source-location information where the diagnostic was located.</summary>
		SourceLocation = 1,

		/// <summary>
		///     If displaying the source-location information of the diagnostic, also include the column
		///     number.
		/// </summary>
		Column = 2,

		/// <summary>
		///     If displaying the source-location information of the diagnostic, also include information
		///     about source ranges in a machine-parsable format.
		/// </summary>
		SourceRanges = 4,

		/// <summary>
		///     Display the option name associated with this diagnostic, if any. The option name displayed
		///     (e.g., -Wconversion) will be placed in brackets after the diagnostic text. This option
		///     corresponds to the clang flag -fdiagnostics-show-option.
		/// </summary>
		Option = 8,

		/// <summary>
		///     Display the category number associated with this diagnostic, if any. The category number
		///     is displayed within brackets after the diagnostic text. This option corresponds to the clang
		///     flag -fdiagnostics-show-category=id.
		/// </summary>
		CategoryId = 16,

		/// <summary>
		///     Display the category name associated with this diagnostic, if any. The category name is
		///     displayed within brackets after the diagnostic text. This option corresponds to the clang flag
		///     -fdiagnostics-show-category=name.
		/// </summary>
		CategoryName = 32
	}
}