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
// ErrorCode.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     Error codes returned by LibClang routines. <see cref="Success" /> is the only error code
	///     indicating success.  Other error codes, including not yet assigned non-zero values, indicate
	///     errors.
	/// </summary>
	public enum ErrorCode
	{
		/// <summary>No error.</summary>
		Success = 0,

		/// <summary>
		///     A generic error code, no further details are available. Errors of this kind can get their
		///     own specific error codes in future LibClang versions.
		/// </summary>
		Failure = 1,

		/// <summary>LibClang crashed while performing the requested operation.</summary>
		Crashed = 2,

		/// <summary>The function detected that the arguments violate the function contract.</summary>
		InvalidArguments = 3,

		/// <summary>An AST deserialization error has occurred.</summary>
		ASTReadError = 4
	}
}