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
// TypeKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describes the kind of type</summary>
	public enum TypeKind
	{
		/// <summary>Represents an invalid type (e.g., where no type is available).</summary>
		Invalid = 0,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Unexposed = 1,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Void = 2,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Bool = 3,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		CharU = 4,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		UChar = 5,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Char16 = 6,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Char32 = 7,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		UShort = 8,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		UInt = 9,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ULong = 10,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ULongLong = 11,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		UInt128 = 12,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		CharS = 13,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		SChar = 14,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		WChar = 15,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Short = 16,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Int = 17,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Long = 18,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		LongLong = 19,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Int128 = 20,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Float = 21,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Double = 22,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		LongDouble = 23,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		NullPtr = 24,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Overload = 25,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Dependent = 26,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ObjCId = 27,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ObjCClass = 28,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ObjCSel = 29,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Float128 = 30,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Half = 31,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Float16 = 32,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		FirstBuiltin = 2,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		LastBuiltin = 32,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Complex = 100,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Pointer = 101,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		BlockPointer = 102,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		LValueReference = 103,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		RValueReference = 104,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Record = 105,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Enum = 106,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Typedef = 107,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ObjCInterface = 108,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ObjCObjectPointer = 109,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		FunctionNoProto = 110,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		FunctionProto = 111,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		ConstantArray = 112,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Vector = 113,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		IncompleteArray = 114,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		VariableArray = 115,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		DependentSizedArray = 116,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		MemberPointer = 117,

		/// <summary>A type whose specific kind is not exposed via this interface.</summary>
		Auto = 118,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		Elaborated = 119,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		Pipe = 120,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dRO = 121,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dArrayRO = 122,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dBufferRO = 123,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dRO = 124,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayRO = 125,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dDepthRO = 126,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayDepthRO = 127,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dMSAARO = 128,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayMSAARO = 129,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dMSAADepthRO = 130,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayMSAADepthRO = 131,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage3dRO = 132,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dWO = 133,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dArrayWO = 134,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dBufferWO = 135,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dWO = 136,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayWO = 137,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dDepthWO = 138,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayDepthWO = 139,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dMSAAWO = 140,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayMSAAWO = 141,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dMSAADepthWO = 142,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayMSAADepthWO = 143,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage3dWO = 144,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dRW = 145,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dArrayRW = 146,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage1dBufferRW = 147,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dRW = 148,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayRW = 149,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dDepthRW = 150,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayDepthRW = 151,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dMSAARW = 152,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayMSAARW = 153,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dMSAADepthRW = 154,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage2dArrayMSAADepthRW = 155,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLImage3dRW = 156,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLSampler = 157,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLEvent = 158,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLQueue = 159,

		/// <summary>
		///     Represents a type that was referred to using an elaborated type keyword. E.g., struct S,
		///     or via a qualified name, e.g., N::M::type, or both.
		/// </summary>
		OCLReserveID = 160
	}
}