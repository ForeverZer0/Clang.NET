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
// IndexOptFlags.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary></summary>
	public enum IndexOptFlags
	{
		/// <summary>Used to indicate that no special indexing options are needed.</summary>
		None = 0,

		/// <summary>
		///     Used to indicate that IndexerCallbacks#indexEntityReference should be invoked for only one
		///     reference of an entity per source file that does not also include a declaration/definition of
		///     the entity.
		/// </summary>
		SuppressRedundantRefs = 1,

		/// <summary>
		///     Function-local symbols should be indexed. If this is not set function-local symbols will
		///     be ignored.
		/// </summary>
		IndexFunctionLocalSymbols = 2,

		/// <summary>
		///     Implicit function/class template instantiations should be indexed. If this is not set,
		///     implicit instantiations will be ignored.
		/// </summary>
		IndexImplicitTemplateInstantiations = 4,

		/// <summary>Suppress all compiler warnings when parsing for indexing.</summary>
		SuppressWarnings = 8,

		/// <summary>
		///     Skip a function/method body that was already parsed during an indexing session associated
		///     with a CXIndexAction object. Bodies in system headers are always skipped.
		/// </summary>
		SkipParsedBodiesInSession = 16
	}
}