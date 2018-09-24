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
// CursorExceptionSpecificationKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describes the exception specification of a cursor.
	///     <para>A negative value indicates that the cursor is not a function declaration.</para>
	/// </summary>
	public enum CursorExceptionKind
	{
		/// <summary>The cursor has no exception specification.</summary>
		None = 0,

		/// <summary>The cursor has exception specification throw()</summary>
		DynamicNone = 1,

		/// <summary>The cursor has exception specification throw(T1, T2)</summary>
		Dynamic = 2,

		/// <summary>The cursor has exception specification throw(...).</summary>
		MSAny = 3,

		/// <summary>The cursor has exception specification basic noexcept.</summary>
		BasicNoexcept = 4,

		/// <summary>The cursor has exception specification computed noexcept.</summary>
		ComputedNoexcept = 5,

		/// <summary>The exception specification has not yet been evaluated.</summary>
		Unevaluated = 6,

		/// <summary>The exception specification has not yet been instantiated.</summary>
		Uninstantiated = 7,

		/// <summary>The exception specification has not been parsed yet.</summary>
		Unparsed = 8
	}
}