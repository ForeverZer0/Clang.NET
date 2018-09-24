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
// IdxEntityKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary></summary>
	public enum IdxEntityKind
	{
		/// <summary></summary>
		Unexposed = 0,

		/// <summary></summary>
		Typedef = 1,

		/// <summary></summary>
		Function = 2,

		/// <summary></summary>
		Variable = 3,

		/// <summary></summary>
		Field = 4,

		/// <summary></summary>
		EnumConstant = 5,

		/// <summary></summary>
		ObjCClass = 6,

		/// <summary></summary>
		ObjCProtocol = 7,

		/// <summary></summary>
		ObjCCategory = 8,

		/// <summary></summary>
		ObjCInstanceMethod = 9,

		/// <summary></summary>
		ObjCClassMethod = 10,

		/// <summary></summary>
		ObjCProperty = 11,

		/// <summary></summary>
		ObjCIvar = 12,

		/// <summary></summary>
		Enum = 13,

		/// <summary></summary>
		Struct = 14,

		/// <summary></summary>
		Union = 15,

		/// <summary></summary>
		CXXClass = 16,

		/// <summary></summary>
		CXXNamespace = 17,

		/// <summary></summary>
		CXXNamespaceAlias = 18,

		/// <summary></summary>
		CXXStaticVariable = 19,

		/// <summary></summary>
		CXXStaticMethod = 20,

		/// <summary></summary>
		CXXInstanceMethod = 21,

		/// <summary></summary>
		CXXConstructor = 22,

		/// <summary></summary>
		CXXDestructor = 23,

		/// <summary></summary>
		CXXConversionFunction = 24,

		/// <summary></summary>
		CXXTypeAlias = 25,

		/// <summary></summary>
		CXXInterface = 26
	}
}