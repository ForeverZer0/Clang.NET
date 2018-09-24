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
// DiagnosticSeverity.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describes the severity of a particular diagnostic.</summary>
	public enum DiagnosticSeverity
	{
		/// <summary>A diagnostic that has been suppressed, e.g., by a command-line option.</summary>
		Ignored = 0,

		/// <summary>This diagnostic is a note that should be attached to the previous (non-note) diagnostic.</summary>
		Note = 1,

		/// <summary>This diagnostic indicates suspicious code that may not be wrong.</summary>
		Warning = 2,

		/// <summary>This diagnostic indicates that the code is ill-formed.</summary>
		Error = 3,

		/// <summary>
		///     This diagnostic indicates that the code is ill-formed such that future parser recovery is
		///     unlikely to produce useful results.
		/// </summary>
		Fatal = 4
	}
}