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
// CompletionContext.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>
	///     Bits that represent the context under which completion is occurring. The enumerators in
	///     this enumeration may be bitwise-OR'd together if multiple contexts are occurring
	///     simultaneously.
	/// </summary>
	public enum CompletionContext
	{
		/// <summary>
		///     The context for completions is unexposed, as only Clang results should be included. (This
		///     is equivalent to having no context bits set.)
		/// </summary>
		Unexposed = 0,

		/// <summary>Completions for any possible type should be included in the results.</summary>
		AnyType = 1,

		/// <summary>
		///     Completions for any possible value (variables, function calls, etc.) should be included in
		///     the results.
		/// </summary>
		AnyValue = 2,

		/// <summary>
		///     Completions for values that resolve to an Objective-C object should be included in the
		///     results.
		/// </summary>
		ObjCObjectValue = 4,

		/// <summary>
		///     Completions for values that resolve to an Objective-C selector should be included in the
		///     results.
		/// </summary>
		ObjCSelectorValue = 8,

		/// <summary>Completions for values that resolve to a C++ class type should be included in the results.</summary>
		CXXClassTypeValue = 16,

		/// <summary>
		///     Completions for fields of the member being accessed using the dot operator should be
		///     included in the results.
		/// </summary>
		DotMemberAccess = 32,

		/// <summary>
		///     Completions for fields of the member being accessed using the arrow operator should be
		///     included in the results.
		/// </summary>
		ArrowMemberAccess = 64,

		/// <summary>
		///     Completions for properties of the Objective-C object being accessed using the dot operator
		///     should be included in the results.
		/// </summary>
		ObjCPropertyAccess = 128,

		/// <summary>Completions for enum tags should be included in the results.</summary>
		EnumTag = 256,

		/// <summary>Completions for union tags should be included in the results.</summary>
		UnionTag = 512,

		/// <summary>Completions for struct tags should be included in the results.</summary>
		StructTag = 1024,

		/// <summary>Completions for C++ class names should be included in the results.</summary>
		ClassTag = 2048,

		/// <summary>Completions for C++ namespaces and namespace aliases should be included in the results.</summary>
		Namespace = 4096,

		/// <summary>Completions for C++ nested name specifiers should be included in the results.</summary>
		NestedNameSpecifier = 8192,

		/// <summary>Completions for Objective-C interfaces (classes) should be included in the results.</summary>
		ObjCInterface = 16384,

		/// <summary>Completions for Objective-C protocols should be included in the results.</summary>
		ObjCProtocol = 32768,

		/// <summary>Completions for Objective-C categories should be included in the results.</summary>
		ObjCCategory = 65536,

		/// <summary>Completions for Objective-C instance messages should be included in the results.</summary>
		ObjCInstanceMessage = 131072,

		/// <summary>Completions for Objective-C class messages should be included in the results.</summary>
		ObjCClassMessage = 262144,

		/// <summary>Completions for Objective-C selector names should be included in the results.</summary>
		ObjCSelectorName = 524288,

		/// <summary>Completions for preprocessor macro names should be included in the results.</summary>
		MacroName = 1048576,

		/// <summary>Natural language completions should be included in the results.</summary>
		NaturalLanguage = 2097152,

		/// <summary>The current context is unknown, so set all contexts.</summary>
		Unknown = 4194303
	}
}