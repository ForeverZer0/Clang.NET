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
// LinkageKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describe the linkage of the entity referred to by a cursor.</summary>
	public enum LinkageKind
	{
		/// <summary>This value indicates that no linkage information is available for a provided CXCursor.</summary>
		Invalid = 0,

		/// <summary>
		///     This is the linkage for variables, parameters, and so on that have automatic storage. This
		///     covers normal (non-extern) local variables.
		/// </summary>
		NoLinkage = 1,

		/// <summary>This is the linkage for static variables and static functions.</summary>
		Internal = 2,

		/// <summary>
		///     This is the linkage for entities with external linkage that live in C++ anonymous
		///     namespaces.
		/// </summary>
		UniqueExternal = 3,

		/// <summary>This is the linkage for entities with true, external linkage.</summary>
		External = 4
	}
}