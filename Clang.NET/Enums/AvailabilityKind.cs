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
// AvailabilityKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     Describes the availability of a particular entity, which indicates whether the use of this
	///     entity will result in a warning or error due to it being deprecated or unavailable.
	/// </summary>
	public enum AvailabilityKind
	{
		/// <summary>The entity is available.</summary>
		Available = 0,

		/// <summary>The entity is available, but has been deprecated (and its use is not recommended).</summary>
		Deprecated = 1,

		/// <summary>The entity is not available; any use of it will be an error.</summary>
		NotAvailable = 2,

		/// <summary>The entity is available, but not accessible; any use of it will be an error.</summary>
		NotAccessible = 3
	}
}