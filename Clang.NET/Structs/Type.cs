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
// Type.cs created on 2018-09-22

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>The type of an element in the abstract syntax tree.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Type : IEquatable<Type>
	{
		public readonly TypeKind Kind;
		private readonly IntPtr Data1;
		private readonly IntPtr Data2;

		#region Properties & Indexers

		/// <summary>Gets the address space of this <see cref="Type" />.</summary>
		/// <value>The address space.</value>
		public uint AddressSpace => Clang.GetAddressSpace(this);

		/// <summary>Return the alignment of a type in bytes as per C++[expr.alignof] standard.</summary>
		/// <value>The alignment.</value>
		public long AlignOf => Clang.TypeGetAlignOf(this);

		/// <summary>
		///     Retrieve the number of non-variadic parameters associated with a function type.
		///     <para>If a non-function type is passed in, -1 is returned.</para>
		/// </summary>
		/// <value>The argument type count.</value>
		public int ArgumentTypeCount => Clang.GetNumArgTypes(this);

		/// <summary>Return the element type of an array type.
		///     <para>If a non-array type is passed in, an invalid type is returned.</para>
		/// </summary>
		/// <value>The type of the array element.</value>
		public Type ArrayElementType => Clang.GetArrayElementType(this);

		/// <summary>Return the array size of a constant array.
		///     <para>If a non-array type is passed in, -1 is returned.</para>
		/// </summary>
		/// <value>The size of the array.</value>
		public long ArraySize => Clang.GetArraySize(this);

		/// <summary>
		///     Retrieve the calling convention associated with a function type.
		///     <para>If this is non-function type, <see cref="CallingConv.Invalid" /> is returned.</para>
		/// </summary>
		/// <value>The calling convention.</value>
		public CallingConv CallingConvention => Clang.GetFunctionTypeCallingConv(this);

		/// <summary>
		///     Return the canonical type for a <see cref="Type" />.
		///     <para>
		///         Clang's type system explicitly models typedefs and all the ways a specific type can be
		///         represented.
		///     </para>
		///     <para>
		///         The canonical type is the underlying type with all the "sugar" removed.  For example, if
		///         'T' is a typedef for <c>int</c>, the canonical type for 'T' would be <c>int</c>.
		///     </para>
		/// </summary>
		/// <value>The canonical type.</value>
		public Type CanonicalType => Clang.GetCanonicalType(this);

		/// <summary>
		///     Return the class type of an member pointer type.
		///     <para>If this is a non-member-pointer type, an invalid type is returned.</para>
		/// </summary>
		/// <value>The type of the class.</value>
		public Type ClassType => Clang.TypeGetClassType(this);

		/// <summary>
		///     Return the number of elements of an array or vector type.
		///     <para>If a type is passed in that is not an array or vector type, -1 is returned.</para>
		/// </summary>
		/// <value>The elements count.</value>
		public long ElementsCount => Clang.GetNumElements(this);

		/// <summary>
		///     Return the element type of an array, complex, or vector type.
		///     <para>
		///         If a type is passed in that is not an array, complex, or vector type, an invalid type is
		///         returned.
		///     </para>
		/// </summary>
		/// <value>The type of the element.</value>
		public Type ElementType => Clang.GetElementType(this);

		/// <summary>Retrieve the exception specification type associated with a function type.</summary>
		/// <value>The kind of the exception.</value>
		public CursorExceptionKind ExceptionKind => Clang.GetExceptionSpecificationType(this);

		/// <summary>
		///     Determine whether a Type has the "const" qualifier set, without looking through typedefs
		///     that may have added "const" at a different level.
		/// </summary>
		/// <value><c>true</c> if this instance is constant qualified; otherwise, <c>false</c>.</value>
		public bool IsConstQualified => Clang.IsConstQualifiedType(this);

		/// <summary>
		///     Return <c>true</c> if the <see cref="Type" /> is a variadic function type, and
		///     <c>false</c> otherwise.
		/// </summary>
		/// <value><c>true</c> if this instance is function variadic; otherwise, <c>false</c>.</value>
		public bool IsFunctionVariadic => Clang.IsFunctionTypeVariadic(this);

		/// <summary>
		///     Return <c>true</c> if the <see cref="Type" /> is a POD (plain old data) type, and
		///     <c>false</c> otherwise.
		/// </summary>
		/// <value><c>true</c> if this instance is POD type; otherwise, <c>false</c>.</value>
		public bool IsPODType => Clang.IsPODType(this);

		/// <summary>
		///     Determine whether a Type has the "restrict" qualifier set, without looking through
		///     typedefs that may have added "restrict" at a different level.
		/// </summary>
		/// <value><c>true</c> if this instance is restrict qualified; otherwise, <c>false</c>.</value>
		public bool IsRestrictQualified => Clang.IsRestrictQualifiedType(this);

		/// <summary>
		///     Gets a value indicating whether this instance is transparent tag typedef.
		///     <para>
		///         A typedef is considered 'transparent' if it shares a name and spelling location with its
		///         underlying tag type, as is the case with the NS_ENUM macro.
		///     </para>
		/// </summary>
		/// <value><c>true</c> if this instance is transparent tag typedef; otherwise, <c>false</c>.</value>
		public bool IsTransparentTagTypedef => Clang.TypeIsTransparentTagTypedef(this);

		/// <summary>
		///     Determine whether a Type has the "volatile" qualifier set, without looking through
		///     typedefs that may have added "volatile" at a different level.
		/// </summary>
		/// <value><c>true</c> if this instance is volatile qualified; otherwise, <c>false</c>.</value>
		public bool IsVolatileQualified => Clang.IsVolatileQualifiedType(this);

		/// <summary>
		///     Retrieve the type named by the qualified-id.
		///     <para>If this is a non-elaborated type, an invalid type is returned.</para>
		/// </summary>
		/// <value>The type.</value>
		public Type NamedType => Clang.TypeGetNamedType(this);

		/// <summary>Returns the Objective-C type encoding for the specified Type.</summary>
		/// <value>The Objective-C encoding.</value>
		public string ObjCEncoding => Clang.TypeGetObjCEncoding(this);

		/// <summary>For pointer types, returns the type of the pointee.</summary>
		/// <value>The pointee type.</value>
		public Type PointeeType => Clang.GetPointeeType(this);

		/// <summary>
		///     Retrieve the ref-qualifier kind of a function or method. The ref-qualifier is returned for
		///     C++ functions or methods.
		///     <para>
		///         For other types or non-C++ declarations, <see cref="RefQualifierKind.None" /> is
		///         returned.
		///     </para>
		/// </summary>
		public RefQualifierKind RefQualifier => Clang.TypeGetCXXRefQualifier(this);

		/// <summary>
		///     Retrieve the return type associated with a function type.
		///     <para>If a non-function type is passed in, an invalid type is returned.</para>
		/// </summary>
		/// <value>The type of the result.</value>
		public Type ResultType => Clang.GetResultType(this);

		/// <summary>Return the size of a type in bytes as per C++[expr.sizeof] standard.</summary>
		/// <value>The size in bytes.</value>
		public long SizeOf => Clang.TypeGetSizeOf(this);

		/// <summary>
		///     Pretty-print the underlying type using the rules of the language of the translation unit
		///     from which it came. If the type is invalid, an empty string is returned.
		/// </summary>
		/// <value>The spelling.</value>
		public string Spelling => ToString();

		/// <summary>
		///     Returns the number of template arguments, or -1 if type T is not a template
		///     specialization.
		/// </summary>
		/// <value>The template argument count.</value>
		public int TemplateArgumentCount => Clang.TypeGetNumTemplateArguments(this);

		/// <summary>Returns the typedef name of the given type.</summary>
		/// <value>The name of the typedef.</value>
		public string TypedefName => Clang.GetTypedefName(this);

		#endregion

		#region IEquatable<Type> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(Type other) => Clang.EqualTypes(this, other);

		#endregion

		#region Methods

		/// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
		///     otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Type other && Equals(other);
		}

		/// <summary>
		///     Retrieve the type of a parameter of a function type.
		///     <para>
		///         If a non-function type is passed in or the function does not have enough parameters, an
		///         invalid type is returned.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The type of the specified argument.</returns>
		public Type GetArgumentType(uint index) => Clang.GetArgType(this, index);

		/// <summary>
		///     Retrieve the type of a parameter of a function type.
		///     <para>
		///         If a non-function type is passed in or the function does not have enough parameters, an
		///         invalid type is returned.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The type of the specified argument.</returns>
		public Type GetArgumentType(int index) => Clang.GetArgType(this, Convert.ToUInt32(index));

		/// <summary>Return the cursor for the declaration of the this type.</summary>
		/// <returns>The name.</returns>
		public Cursor GetDeclaration() => Clang.GetTypeDeclaration(this);

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>
		///     A hash code for this instance, suitable for use in hashing algorithms and data structures
		///     like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (int) Kind;
				hashCode = (hashCode * 233) ^ Data1.GetHashCode();
				hashCode = (hashCode * 281) ^ Data2.GetHashCode();
				return hashCode;
			}
		}

		/// <summary>
		///     Returns the type template argument of a template class specialization at given index.
		///     <para>
		///         This property only returns template type arguments and does not handle template template
		///         arguments or variadic packs.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The type.</returns>
		public Type GetTemplateArgAsType(uint index) => Clang.TypeGetTemplateArgumentAsType(this, index);

		/// <summary>
		///     Returns the type template argument of a template class specialization at given index.
		///     <para>
		///         This property only returns template type arguments and does not handle template template
		///         arguments or variadic packs.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The type.</returns>
		public Type GetTemplateArgAsType(int index) =>
			Clang.TypeGetTemplateArgumentAsType(this, Convert.ToUInt32(index));

		/// <summary>Return the offset of the field with the specified name.</summary>
		/// <param name="name">The name of the field.</param>
		/// <returns>The offset of the field.</returns>
		public long OffsetOf(string name) => Clang.TypeGetOffsetOf(this, name);

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Clang.GetTypeSpelling(this);

		/// <summary>
		///     Visit the fields of a particular type. This function visits all the direct fields of the
		///     given cursor, invoking the given visitor function with the cursors of each visited field.
		/// </summary>
		/// <param name="visitor">The visitor function to call for each field.</param>
		/// <returns>Visitor result returned by the function.</returns>
		public VisitorResult VisitFields(FieldVisitor visitor) => VisitFields(visitor, ClientData.Null);

		/// <summary>
		///     Visit the fields of a particular type. This function visits all the direct fields of the
		///     given cursor, invoking the given visitor function with the cursors of each visited field.
		/// </summary>
		/// <param name="visitor">The visitor function to call for each field.</param>
		/// <param name="data">Ant additional data.</param>
		/// <returns>Visitor result returned by the function.</returns>
		public VisitorResult VisitFields(FieldVisitor visitor, ClientData data) =>
			Clang.TypeVisitFields(this, visitor, data);

		#endregion

		#region Operators

		public static bool operator ==(Type left, Type right) => left.Equals(right);

		public static bool operator !=(Type left, Type right) => !left.Equals(right);

		#endregion
	}
}