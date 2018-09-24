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
// TypeLayoutError.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     List the possible error codes for clang_Type_getSizeOf, clang_Type_getAlignOf,
	///     clang_Type_getOffsetOf and clang_Cursor_getOffsetOf. A value of this enumeration type can be
	///     returned if the target type is not a valid argument to sizeof, alignof or offsetof.
	/// </summary>
	public enum TypeLayoutError
	{
		/// <summary>Type is of kind CXType_Invalid.</summary>
		Invalid = -1,

		/// <summary>The type is an incomplete Type.</summary>
		Incomplete = -2,

		/// <summary>The type is a dependent Type.</summary>
		Dependent = -3,

		/// <summary>The type is not a constant size type.</summary>
		NotConstantSize = -4,

		/// <summary>The Field name is not valid for this record.</summary>
		InvalidFieldName = -5
	}
}