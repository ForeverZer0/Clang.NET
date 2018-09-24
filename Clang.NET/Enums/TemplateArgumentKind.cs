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
// TemplateArgumentKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     Describes the kind of a template argument. See the definition of
	///     llvm::clang::TemplateArgument::ArgKind for full element descriptions.
	/// </summary>
	public enum TemplateArgumentKind
	{
		/// <summary></summary>
		Null = 0,

		/// <summary></summary>
		Type = 1,

		/// <summary></summary>
		Declaration = 2,

		/// <summary></summary>
		NullPtr = 3,

		/// <summary></summary>
		Integral = 4,

		/// <summary></summary>
		Template = 5,

		/// <summary></summary>
		TemplateExpansion = 6,

		/// <summary></summary>
		Expression = 7,

		/// <summary></summary>
		Pack = 8,

		/// <summary></summary>
		Invalid = 9
	}
}