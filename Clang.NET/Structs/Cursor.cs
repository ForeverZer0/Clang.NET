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
// Cursor.cs created on 2018-09-21

#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     A cursor representing some element in the abstract syntax tree for a translation unit.
	///     <para>
	///         The cursor abstraction unifies the different kinds of entities in a program--declaration,
	///         statements, expressions, references to declarations, etc.--under a single "cursor"
	///         abstraction with a common set of operations. Common operation for a cursor include: getting
	///         the physical location in a source file where the cursor points, getting the name associated
	///         with a cursor, and retrieving cursors for any child nodes of a particular cursor. Cursors
	///         can be produced in two specific ways.
	///     </para>
	///     <para>
	///         <see cref="Clang.GetTranslationUnitCursor" /> produces a cursor for a translation unit,
	///         from which one can use <see cref="Clang.VisitChildren" /> to explore the rest of the
	///         translation unit. <see cref="Clang.GetCursor" />maps from a physical source location to the
	///         entity that resides at that location, allowing one to map from the source code into the
	///         AST.
	///     </para>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Cursor : IEquatable<Cursor>
	{
		private readonly CursorKind _kind;
		private readonly int _xdata;
		private readonly IntPtr _data1;
		private readonly IntPtr _data2;
		private readonly IntPtr _data3;

		#region Properties & Indexers

		/// <summary>Retrieve the null cursor, which represents no entity.</summary>
		/// <value>A null cursor.</value>
		public static Cursor Null => Clang.GetNullCursor();

		/// <summary>Returns the access control level for the referenced object.</summary>
		/// <value>The access specifier.</value>
		public CXXAccessSpecifier AccessSpecifier => Clang.GetCXXAccessSpecifier(this);

		/// <summary>Returns an object to enumerate through the arguments.</summary>
		/// <value>The arguments.</value>
		public IEnumerable<Cursor> Arguments
		{
			get
			{
				var count = Clang.CursorGetNumArguments(this);
				for (uint i = 0; i < count; i++)
					yield return Clang.CursorGetArgument(this, i);
			}
		}

		/// <summary>
		///     Determine the availability of the entity that this cursor refers to, taking the current
		///     target platform into account.
		/// </summary>
		/// <value>The availability of the cursor.</value>
		public AvailabilityKind AvailabilityKind => Clang.GetCursorAvailability(this);

		/// <summary>
		///     Retrieve the canonical cursor corresponding to the given cursor.
		///     <para>
		///         In the C family of languages, many kinds of entities can be declared several times within
		///         a single translation unit.
		///     </para>
		/// </summary>
		/// <value>The canonical cursor.</value>
		public Cursor Canonical => Clang.GetCanonicalCursor(this);

		/// <summary>
		///     Assuming cursor represents a declaration, returns the associated comment's source range.
		///     The range may include multiple consecutive comments with whitespace in between.
		/// </summary>
		/// <value>The comment range.</value>
		public SourceRange CommentRange => Clang.CursorGetCommentRange(this);

		/// <summary>Determine whether this cursor that is a macro, is a builtin one.</summary>
		/// <value><c>true</c> if [cursor is macro builtin]; otherwise, <c>false</c>.</value>
		public bool CursorIsMacroBuiltin => Clang.CursorIsMacroBuiltin(this);

		/// <summary>
		///     Retrieve the display name for the entity referenced by this cursor.
		///     <para>
		///         The display name contains extra information that helps identify the cursor, such as the
		///         parameters of a function or template or the arguments of a class template specialization.
		///     </para>
		/// </summary>
		/// <value>The display name.</value>
		public string DisplayName => Clang.GetCursorDisplayName(this);


		/// <summary>Determine if an enum declaration refers to a scoped enum.</summary>
		/// <value><c>true</c> if [enum decl is scoped]; otherwise, <c>false</c>.</value>
		public bool EnumDeclIsScoped => Clang.EnumDeclIsScoped(this);

		/// <summary>
		///     Retrieve the exception specification type associated with this cursor. This only returns a
		///     valid result if the cursor refers to a function or method.
		/// </summary>
		public CursorExceptionKind ExceptionKind => Clang.GetCursorExceptionSpecificationType(this);

		/// <summary>Retrieve the bit width of a bit field declaration as an integer.
		///     <para>If a cursor that is not a bit field declaration is passed in, -1 is returned.</para>
		/// </summary>
		/// <value>The width.</value>
		public int FieldDeclBitWidth => Clang.GetFieldDeclBitWidth(this);

		/// <summary>Determine whether the given cursor has any attributes.</summary>
		/// <value><c>true</c> if this instance has attrs; otherwise, <c>false</c>.</value>
		public bool HasAttrs => Clang.CursorHasAttrs(this);

		/// <summary>
		///     For cursors representing an IB outlet collection attribute, this function returns the
		///     collection element type.
		/// </summary>
		/// <value>The type.</value>
		public Type IBOutletCollectionType => Clang.GetIBOutletCollectionType(this);

		/// <summary>Retrieve the file that is included by this inclusion directive cursor.</summary>
		/// <value>The include file.</value>
		public File IncludeFile => Clang.GetIncludedFile(this);

		/// <summary>
		///     Determine if a C++ record is abstract, i.e. whether a class or struct has a pure virtual
		///     member function.
		/// </summary>
		/// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
		public bool IsAbstract => Clang.RecordIsAbstract(this);

		/// <summary>Determine whether the given cursor represents an anonymous record declaration.</summary>
		/// <value><c>true</c> if this instance is anonymous; otherwise, <c>false</c>.</value>
		public bool IsAnonymous => Clang.CursorIsAnonymous(this);

		/// <summary>Returns non-zero if the cursor specifies a Record member that is a bitfield.</summary>
		/// <value><c>true</c> if this instance is bit field; otherwise, <c>false</c>.</value>
		public bool IsBitField => Clang.CursorIsBitField(this);

		/// <summary>Determine if a C++ member function or member function template is declared 'const'.</summary>
		/// <value><c>true</c> if this instance is constant; otherwise, <c>false</c>.</value>
		public bool IsConst => Clang.MethodIsConst(this);

		/// <summary>Determine if a C++ constructor is a converting constructor.</summary>
		/// <value><c>true</c> if this instance is converting constructor; otherwise, <c>false</c>.</value>
		public bool IsConvertingConstructor => Clang.ConstructorIsConvertingConstructor(this);

		/// <summary>Determine if a C++ constructor is a copy constructor.</summary>
		/// <value><c>true</c> if this instance is copy constructor; otherwise, <c>false</c>.</value>
		public bool IsCopyConstructor => Clang.ConstructorIsCopyConstructor(this);

		/// <summary>Determine if a C++ constructor is the default constructor.</summary>
		/// <value><c>true</c> if this instance is default constructor; otherwise, <c>false</c>.</value>
		public bool IsDefaultConstructor => Clang.ConstructorIsDefaultConstructor(this);

		/// <summary>Determine if a C++ method is declared '= default'.</summary>
		/// <value><c>true</c> if this instance is defaulted; otherwise, <c>false</c>.</value>
		public bool IsDefaulted => Clang.MethodIsDefaulted(this);

		/// <summary>
		///     Determine whether the declaration pointed to by this cursor is also a definition of that
		///     entity.
		/// </summary>
		/// <value><c>true</c> if this instance is definition; otherwise, <c>false</c>.</value>
		public bool IsDefinition => Clang.IsCursorDefinition(this);

		/// <summary>
		///     Given a cursor pointing to a C++ method call or an Objective-C message, returns true if
		///     the method/message is "dynamic", meaning: For a C++ method: the call is virtual. For an
		///     Objective-C message: the receiver is an object instance, not 'super' or a specific class. If
		///     the method/message is "static" or the cursor does not point to a method/message, it will return
		///     false.
		/// </summary>
		/// <value><c>true</c> if this instance is dynamic call; otherwise, <c>false</c>.</value>
		public bool IsDynamicCall => Clang.CursorIsDynamicCall(this);

		/// <summary>Determine whether this cursor that is a function declaration, is an inline declaration.</summary>
		/// <value><c>true</c> if this instance is function inlined; otherwise, <c>false</c>.</value>
		public bool IsFunctionInlined => Clang.CursorIsFunctionInlined(this);


		/// <summary>Determine whether this cursor is a macro, is function like.</summary>
		/// <value><c>true</c> if cursor is macro function like; otherwise, <c>false</c>.</value>
		public bool IsMacroFunctionLike => Clang.CursorIsMacroFunctionLike(this);

		/// <summary>Determine if a C++ constructor is a move constructor.</summary>
		/// <value><c>true</c> if this instance is move constructor; otherwise, <c>false</c>.</value>
		public bool IsMoveConstructor => Clang.ConstructorIsMoveConstructor(this);

		/// <summary>Determine if a C++ field is declared 'mutable'.</summary>
		/// <value><c>true</c> if this instance is mutable; otherwise, <c>false</c>.</value>
		public bool IsMutable => Clang.FieldIsMutable(this);

		/// <summary>Returns flag indicating if cursor is null.</summary>
		/// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
		public bool IsNull => Clang.CursorIsNull(this);

		/// <summary>
		///     Given a cursor that represents an Objective-C method or property declaration, return
		///     non-zero if the declaration was affected by " @ optional".
		/// </summary>
		/// <value><c>true</c> if this instance is object c optional; otherwise, <c>false</c>.</value>
		public bool IsObjCOptional => Clang.CursorIsObjCOptional(this);

		/// <summary>Determine if a C++ member function or member function template is pure virtual.</summary>
		/// <value><c>true</c> if this instance is pure virtual; otherwise, <c>false</c>.</value>
		public bool IsPureVirtual => Clang.MethodIsPureVirtual(this);

		/// <summary>Determine if a C++ member function or member function template is declared 'static'.</summary>
		/// <value><c>true</c> if this instance is static; otherwise, <c>false</c>.</value>
		public bool IsStatic => Clang.MethodIsStatic(this);


		/// <summary>Returns non-zero if the given cursor is a variadic function or method.</summary>
		/// <value><c>true</c> if this instance is variadic; otherwise, <c>false</c>.</value>
		public bool IsVariadic => Clang.CursorIsVariadic(this);

		/// <summary>
		///     Determine if a C++ member function or member function template is explicitly declared
		///     'virtual' or if it overrides a virtual method from one of the base classes.
		/// </summary>
		/// <value><c>true</c> if this instance is virtual; otherwise, <c>false</c>.</value>
		public bool IsVirtual => Clang.MethodIsVirtual(this);

		/// <summary>Returns <c>true</c> if the base class specified by the cursor is virtual.</summary>
		/// <value><c>true</c> if this instance is virtual base; otherwise, <c>false</c>.</value>
		public bool IsVirtualBase => Clang.IsVirtualBase(this);

		/// <summary>Retrieve the kind of the given cursor.</summary>
		/// <value>The cursor kind.</value>
		public CursorKind Kind => Clang.GetCursorKind(this);

		/// <summary>Determine the "language" of the entity referred to by this cursor.</summary>
		/// <value>The language specifier.</value>
		public LanguageKind Language => Clang.GetCursorLanguage(this);

		/// <summary>Determine the lexical parent of the this cursor.</summary>
		public Cursor LexicalParent => Clang.GetCursorLexicalParent(this);

		/// <summary>Determine the linkage of the entity referred to by this cursor.</summary>
		public LinkageKind Linkage => Clang.GetCursorLinkage(this);

		/// <summary>
		///     Retrieve the physical location of the source constructor referenced by this cursor.
		///     <para>
		///         The location of a declaration is typically the location of the name of that declaration,
		///         where the name of that declaration would occur if it is unnamed, or some keyword that
		///         introduces that particular declaration.
		///     </para>
		///     <para>The location of a reference is where that reference occurs within the source code.</para>
		/// </summary>
		/// <value>The location.</value>
		public SourceLocation Location => Clang.GetCursorLocation(this);

		/// <summary>Returns the Objective-C type encoding for the this declaration cursor.</summary>
		/// <value>The Objective-C type encoding.</value>
		public string ObjCTypeEncoding => Clang.GetDeclObjCTypeEncoding(this);

		/// <summary>Gets the overloaded declarations.</summary>
		/// <value>The overloaded declarations.</value>
		public IEnumerable<Cursor> OverloadedDecls
		{
			get
			{
				var count = Clang.GetNumOverloadedDecls(this);
				for (uint i = 0; i < count; i++)
					yield return Clang.GetOverloadedDecl(this, i);
			}
		}

		/// <summary>Gets the overloaded declarations count.</summary>
		/// <value>The overloaded declarations count.</value>
		public int OverloadedDeclsCount => Convert.ToInt32(Clang.GetNumOverloadedDecls(this));

		/// <summary>
		///     Assuming cursor represents a declaration, return the associated comment text, including
		///     comment markers.
		/// </summary>
		/// <value>The raw comment text.</value>
		public string RawCommentText => Clang.CursorGetRawCommentText(this);

		/// <summary>
		///     Retrieve the return type associated with a given cursor. This only returns a valid type if
		///     the cursor refers to a function or method.
		/// </summary>
		/// <value>The type.</value>
		public Type ResultType => Clang.GetCursorResultType(this);

		/// <summary>Determine the semantic parent of the given cursor.</summary>
		/// <value>The semantic parent.</value>
		public Cursor SemanticParent => Clang.GetCursorSemanticParent(this);

		/// <summary>Retrieve a name for the entity referenced by this cursor.</summary>
		public string Spelling => Clang.GetCursorSpelling(this);

		/// <summary>
		///     Given a cursor that represents a template, determine the cursor kind of the
		///     specializations would be generated by instantiating the template.
		/// </summary>
		/// <value>The kind of the template.</value>
		public CursorKind TemplateKind => Clang.GetTemplateCursorKind(this);

		/// <summary>
		///     Determine the "thread-local storage (TLS) kind" of the declaration referred to by a
		///     cursor.
		/// </summary>
		/// <value>The kind.</value>
		public TLSKind TLSKind => Clang.GetCursorTLSKind(this);

		/// <summary>Returns the translation unit that the cursor originated from.</summary>
		/// <value>The translation unit.</value>
		public TranslationUnit TranslationUnit => Clang.CursorGetTranslationUnit(this);

		/// <summary>Retrieve the type of this cursor (if any).</summary>
		/// <value>The type.</value>
		public Type Type => Clang.GetCursorType(this);

		/// <summary>
		///     Retrieve a Unified Symbol Resolution (USR) for the entity referenced by the given cursor.
		///     <para>
		///         A Unified Symbol Resolution (USR) is a string that identifies a particular entity
		///         (function, class, variable, etc.) within a program. USRs can be compared across translation
		///         units to determine, e.g., when references in one translation refer to an entity defined in
		///         another translation unit.
		///     </para>
		/// </summary>
		public string USR => Clang.GetCursorUSR(this);

		/// <summary>
		///     Describe the visibility of the entity referred to by a cursor.
		///     <para>
		///         This returns the default visibility if not explicitly specified by a visibility
		///         attribute. The default visibility may be changed by commandline arguments. The cursor to
		///         query.
		///     </para>
		/// </summary>
		/// <value>The visibility.</value>
		public VisibilityKind Visibility => Clang.GetCursorVisibility(this);

		#endregion

		#region IEquatable<Cursor> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(Cursor other) => Clang.EqualCursors(this, other);

		#endregion

		#region Methods

		/// <summary>
		///     Given a cursor that represents a property declaration, return the associated property
		///     attributes.
		/// </summary>
		/// <returns>The bits-field attributes.</returns>
		public ObjCPropertyAttrKind CursorGetObjCPropertyAttributes() => Clang.CursorGetObjCPropertyAttributes(this, 0);

		/// <summary>Disposes the overridden cursors.</summary>
		public void DisposeOverridden()
		{
			Clang.GetOverriddenCursors(this, out var ptr, out var dummy);
			Clang.DisposeOverriddenCursors(ptr);
		}

		/// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
		///     otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			return obj is Cursor other && Equals(other);
		}

		/// <summary>
		///     If cursor is a statement declaration tries to evaluate the statement and if its variable,
		///     tries to evaluate its initializer, into its corresponding type.
		/// </summary>
		/// <returns>The result of the evaluation.</returns>
		public EvalResult Evaluate() => Clang.CursorEvaluate(this);

		/// <summary>Find references of a declaration in a specific file.</summary>
		/// <param name="file">The file to search for #import/#include directives. </param>
		/// <param name="visitor">
		///     The callback that will receive pairs of Cursor/CXSourceRange for each
		///     directive found.
		/// </param>
		/// <returns>The result of the enumerators.</returns>
		public Result FindReferencesInFile(File file, CursorAndRangeVisitor visitor) =>
			Clang.FindReferencesInFile(this, file, visitor);

		/// <summary>
		///     Retrieve the argument cursor of a function or method. The argument cursor can be
		///     determined for calls as well as for declarations of functions or methods. For other cursors and
		///     for invalid indices, an invalid cursor is returned.
		/// </summary>
		/// <param name="index">The index of the argument to retrieve.</param>
		/// <returns>The cursor representing the specified argument.</returns>
		public Cursor GetArgument(uint index) => Clang.CursorGetArgument(this, index);

		/// <summary>
		///     Retrieve the argument cursor of a function or method. The argument cursor can be
		///     determined for calls as well as for declarations of functions or methods. For other cursors and
		///     for invalid indices, an invalid cursor is returned.
		/// </summary>
		/// <param name="index">The index of the argument to retrieve.</param>
		/// <returns>The cursor representing the specified argument.</returns>
		public Cursor GetArgument(int index) => Clang.CursorGetArgument(this, Convert.ToUInt32(index));

		/// <summary>
		///     Retrieve the number of non-variadic arguments associated with a given cursor. The number
		///     of arguments can be determined for calls as well as for declarations of functions or methods.
		///     For other cursors -1 is returned.
		/// </summary>
		/// <returns>The number of arguments.</returns>
		public int GetArgumentsCount() => Clang.CursorGetNumArguments(this);

		/// <summary>
		///     Assuming this cursor represents a documentable entity (e.g., declaration), return the
		///     associated brief paragraph; otherwise return the first paragraph.
		/// </summary>
		/// <returns>The comment text.</returns>
		public string GetBriefCommentText() => Clang.CursorGetBriefCommentText(this);

		/// <summary>Retrieve a completion string for an arbitrary declaration or macro definition cursor.</summary>
		/// <returns>
		///     A non-context-sensitive completion string for declaration and macro definition cursors, or
		///     null <see cref="CompletionString" /> for other kinds of cursors.
		/// </returns>
		public CompletionString GetCompletionString() => Clang.GetCursorCompletionString(this);

		/// <summary>
		///     Retrieve the Strings representing the mangled symbols of the C++ constructor or destructor
		///     at the cursor.
		/// </summary>
		/// <returns>The string set of manglings.</returns>
		public StringSet GetCppManglings() => Clang.CursorGetCXXManglings(this);

		/// <summary>
		///     Given a cursor that references something else, return the source range covering that
		///     reference.
		/// </summary>
		/// <param name="nameFlags">The name flags.</param>
		/// <param name="pieceIndex">Index of the piece.</param>
		/// <returns>The source range.</returns>
		public SourceRange GetCursorReferenceNameRange(NameRefFlags nameFlags, uint pieceIndex) =>
			Clang.GetCursorReferenceNameRange(this, nameFlags, pieceIndex);

		/// <summary>
		///     For a cursor that is either a reference to or a declaration of some entity, retrieve a
		///     cursor that describes the definition of that entity. Some entities can be declared multiple
		///     times within a translation unit, but only one of those declarations can also be a definition.
		/// </summary>
		/// <returns>The definition cursor.</returns>
		public Cursor GetDefinition() => Clang.GetCursorDefinition(this);

		/// <summary>
		///     Retrieve the integer type of an enum declaration. If the cursor does not reference an enum
		///     declaration, an invalid type is returned.
		/// </summary>
		/// <returns>The enum integer type.</returns>
		public Type GetEnumIntegerType() => Clang.GetEnumDeclIntegerType(this);

		/// <summary>
		///     Retrieve the integer value of an enum constant declaration as a <see cref="ulong" />.
		///     <para>
		///         If the cursor does not reference an enum constant declaration,
		///         <see cref="ulong.MaxValue" /> is returned. Since this is also potentially a valid constant
		///         value, the kind of the cursor must be verified before calling this function.
		///     </para>
		/// </summary>
		/// <returns>The value.</returns>
		public ulong GetEnumUnsignedValue() => Clang.GetEnumConstantDeclUnsignedValue(this);

		/// <summary>
		///     Retrieve the integer value of an enum constant declaration as a <c>long</c>.
		///     <para>
		///         If the cursor does not reference an enum constant declaration,
		///         <see cref="long.MinValue" /> is returned. Since this is also potentially a valid constant
		///         value, the kind of the cursor must be verified before calling this function.
		///     </para>
		/// </summary>
		/// <returns>The value.</returns>
		public long GetEnumValue() => Clang.GetEnumConstantDeclValue(this);

		/// <summary>
		///     Retrieve the physical extent of the source construct referenced by the given cursor. The
		///     extent of a cursor starts with the file/line/column pointing at the first character within the
		///     source construct that the cursor refers to and ends with the last character within that source
		///     construct. For a declaration, the extent covers the declaration itself. For a reference, the
		///     extent covers the location of the reference (e.g., where the referenced entity was actually
		///     used).
		/// </summary>
		/// <returns>The extent.</returns>
		public SourceRange GetExtent() => Clang.GetCursorExtent(this);

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>
		///     A hash code for this instance, suitable for use in hashing algorithms and data structures
		///     like a hash table.
		/// </returns>
		public override int GetHashCode() => unchecked((int) Clang.HashCursor(this));

		/// <summary>Gets the spelling for the cursor's <see cref="CursorKind" />.</summary>
		/// <returns></returns>
		public string GetKindSpelling() => Clang.GetCursorKindSpelling(_kind);

		/// <summary>Retrieve the string representing the mangled name of the cursor.</summary>
		public string GetMangling() => Clang.CursorGetMangling(this);

		/// <summary>Assuming cursor is a module declaration cursor, returns the associated module.</summary>
		/// <returns>The associated module.</returns>
		public Module GetModule() => Clang.CursorGetModule(this);

		/// <summary>
		///     Given a cursor that represents an Objective-C method or parameter declaration, return the
		///     associated Objective-C qualifiers for the return type or the parameter respectively.
		/// </summary>
		/// <returns>The bits-field qualifiers.</returns>
		public ObjCDeclQualifierKind GetObjCDeclQualifiers() => Clang.CursorGetObjCDeclQualifiers(this);

		/// <summary>
		///     Retrieve the Strings representing the mangled symbols of the ObjC class interface or
		///     implementation at the cursor.
		/// </summary>
		/// <returns>The string set of manglings.</returns>
		public StringSet GetObjCManglings() => Clang.CursorGetObjCManglings(this);

		/// <summary>
		///     If the cursor points to a selector identifier in an Objective-C method or message expression,
		///     this returns the selector index. This can be called to determine if the location points to a
		///     selector identifier.
		///     <para>
		///         The selector index if the cursor is an Objective-C method or message expression and the
		///         cursor is pointing to a selector identifier, or -1 otherwise.
		///     </para>
		/// </summary>
		/// <returns>The index.</returns>
		public int GetObjCSelectorIndex() => Clang.CursorGetObjCSelectorIndex(this);

		/// <summary>
		///     Return the offset of the field represented by the Cursor. If the cursor is not a field
		///     declaration, -1 is returned.
		/// </summary>
		/// <returns>The offset of the field.</returns>
		public long GetOffsetOfField() => Clang.CursorGetOffsetOfField(this);

		/// <summary>Retrieve a cursor for one of the overloaded declarations at the specified index.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The associated cursor.</returns>
		public Cursor GetOverloadedDecl(uint index) => Clang.GetOverloadedDecl(this, index);

		/// <summary>Retrieve a cursor for one of the overloaded declarations at the specified index.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The associated cursor.</returns>
		public Cursor GetOverloadedDecl(int index) => Clang.GetOverloadedDecl(this, Convert.ToUInt32(index));

		/// <summary>Gets the set of methods that are overridden by the given method.</summary>
		/// <returns></returns>
		public Cursor[] GetOverridden()
		{
			Clang.GetOverriddenCursors(this, out var ptr, out var count);
			var size = Marshal.SizeOf<Cursor>();
			var cursors = new Cursor[count];
			for (var i = 0; i < count; i++)
				cursors[i] = Marshal.PtrToStructure<Cursor>(ptr + i * size);
			return cursors;
		}

		/// <summary>
		///     Given a cursor that represents a documentable entity (e.g., declaration), return the
		///     associated parsed comment as a full-comment AST node.
		/// </summary>
		/// <returns>The parsed comment.</returns>
		public Comment GetParsedComment() => Clang.CursorGetParsedComment(this);


		/// <summary>
		///     Assuming cursor is pointing to an Objective-C message or property reference, or C++ method
		///     call, returns the Type of the receiver.
		/// </summary>
		/// <returns>The receiver type.</returns>
		public Type GetReceiverType() => Clang.CursorGetReceiverType(this);

		/// <summary>
		///     For a cursor that is a reference, retrieve a cursor representing the entity that it
		///     references.
		/// </summary>
		/// <returns>The reference cursor.</returns>
		public Cursor GetReferenced() => Clang.GetCursorReferenced(this);

		/// <summary>
		///     Given a cursor that may represent a specialization or instantiation of a template,
		///     retrieve the cursor that represents the template that it specializes or from which it was
		///     instantiated.
		/// </summary>
		/// <returns>The cursor.</returns>
		public Cursor GetSpecializedCursorTemplate() => Clang.GetSpecializedCursorTemplate(this);

		/// <summary>
		///     Retrieve a range for a piece that forms the cursors spelling name. Most of the times there
		///     is only one range for the complete spelling but for Objective-C methods and Objective-C message
		///     expressions, there are multiple pieces for each selector identifier.
		/// </summary>
		/// <param name="pieceIndex">
		///     The index of the spelling name piece. If this is greater than the actual
		///     number of pieces, it will return an invalid) range
		/// </param>
		/// <returns>The range.</returns>
		public SourceRange GetSpellingNameRange(uint pieceIndex) =>
			Clang.CursorGetSpellingNameRange(this, pieceIndex, 0);

		/// <summary>
		///     Retrieve a range for a piece that forms the cursors spelling name. Most of the times there
		///     is only one range for the complete spelling but for Objective-C methods and Objective-C message
		///     expressions, there are multiple pieces for each selector identifier.
		/// </summary>
		/// <param name="pieceIndex">
		///     The index of the spelling name piece. If this is greater than the actual
		///     number of pieces, it will return an invalid) range
		/// </param>
		/// <returns>The range.</returns>
		public SourceRange GetSpellingNameRange(int pieceIndex) =>
			Clang.CursorGetSpellingNameRange(this, Convert.ToUInt32(pieceIndex), 0);

		/// <summary>Returns the storage class for a function or variable declaration.</summary>
		/// <returns>The storage class.</returns>
		public StorageClass GetStorageClass() => Clang.CursorGetStorageClass(this);

		/// <summary>Retrieve the kind of the specified template argument of the cursor.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The kind.</returns>
		public TemplateArgumentKind GetTemplateArgumentKind(uint index) =>
			Clang.CursorGetTemplateArgumentKind(this, index);

		/// <summary>Retrieve the kind of the specified template argument of the cursor.</summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The kind.</returns>
		public TemplateArgumentKind GetTemplateArgumentKind(int index) =>
			Clang.CursorGetTemplateArgumentKind(this, Convert.ToUInt32(index));

		/// <summary>
		///     Returns the number of template args of a function decl representing a template
		///     specialization. If the argument cursor cannot be converted into a template function
		///     declaration, -1 is returned.
		/// </summary>
		/// <returns>The number of template arguments.</returns>
		public int GetTemplateArgumentsCount() => Clang.CursorGetNumTemplateArguments(this);

		/// <summary>
		///     Retrieve a Type representing the type of a TemplateArgument of a function decl
		///     representing a template specialization.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The type.</returns>
		public Type GetTemplateArgumentType(uint index) => Clang.CursorGetTemplateArgumentType(this, index);

		/// <summary>
		///     Retrieve a Type representing the type of a TemplateArgument of a function decl
		///     representing a template specialization.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The type.</returns>
		public Type GetTemplateArgumentType(int index) => GetTemplateArgumentType(Convert.ToUInt32(index));

		/// <summary>
		///     Retrieve the value of an integral TemplateArgument (of a function decl representing a template
		///     specialization) as a <c>ulong</c>.
		///     <para>
		///         It is undefined to call this function on a Cursor that does not represent a FunctionDecl
		///         or whose specified index template argument is not an integral value.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The value of the argument.</returns>
		public ulong GetTemplateArgumentUnsignedValue(uint index) =>
			Clang.CursorGetTemplateArgumentUnsignedValue(this, index);

		/// <summary>
		///     Retrieve the value of an integral TemplateArgument (of a function decl representing a template
		///     specialization) as a <c>ulong</c>.
		///     <para>
		///         It is undefined to call this function on a Cursor that does not represent a FunctionDecl
		///         or whose specified index template argument is not an integral value.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The value of the argument.</returns>
		public ulong GetTemplateArgumentUnsignedValue(int index) =>
			GetTemplateArgumentUnsignedValue(Convert.ToUInt32(index));

		/// <summary>
		///     Retrieve the value of an integral TemplateArgument (of a function decl representing a template
		///     specialization) as a <c>long</c>.
		///     <para>
		///         It is undefined to call this function on a Cursor that does not represent a FunctionDecl
		///         or whose specified index template argument is not an integral value.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The value of the argument.</returns>
		public long GetTemplateArgumentValue(uint index) => Clang.CursorGetTemplateArgumentValue(this, index);

		/// <summary>
		///     Retrieve the value of an integral TemplateArgument (of a function decl representing a template
		///     specialization) as a <c>long</c>.
		///     <para>
		///         It is undefined to call this function on a Cursor that does not represent a FunctionDecl
		///         or whose specified index template argument is not an integral value.
		///     </para>
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The value of the argument.</returns>
		public long GetTemplateArgumentValue(int index) => GetTemplateArgumentValue(Convert.ToUInt32(index));

		/// <summary>
		///     Retrieve the underlying type of a typedef declaration. If the cursor does not reference a
		///     typedef declaration, an invalid type is returned.
		/// </summary>
		/// <returns>The underlying type.</returns>
		public Type GetTypedefUnderlyingType() => Clang.GetTypedefDeclUnderlyingType(this);

		/// <summary>
		///     Returns <c>true</c> if the given cursor points to a symbol marked with
		///     external_source_symbol attribute.
		/// </summary>
		/// <param name="language">The language.</param>
		/// <param name="definedIn">The defined in.</param>
		/// <param name="isGenerated"><c>true</c> if is generated.</param>
		/// <returns>
		///     <c>true</c> if the given cursor points to a symbol marked with external_source_symbol
		///     attribute; otherwise, <c>false</c>.
		/// </returns>
		public bool IsExternalSymbol(out String language, out String definedIn, out bool isGenerated) =>
			Clang.CursorIsExternalSymbol(this, out language, out definedIn, out isGenerated);

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Clang.GetCursorSpelling(this);

		/// <summary>
		///     Visit the children of a particular cursor. This function visits all the direct children of
		///     the cursor, invoking the given <paramref name="visitor" /> function with the cursors of each
		///     visited child.
		/// </summary>
		/// <param name="visitor">The visitor function to invoke.</param>
		public void VisitChildren(CursorVisitor visitor) => VisitChildren(visitor, ClientData.Null);

		/// <summary>
		///     Visit the children of a particular cursor. This function visits all the direct children of
		///     the cursor, invoking the given <paramref name="visitor" /> function with the cursors of each
		///     visited child.
		/// </summary>
		/// <param name="visitor">The visitor function to invoke.</param>
		/// <param name="data">Any client data to pass as an argument.</param>
		public void VisitChildren(CursorVisitor visitor, ClientData data) => Clang.VisitChildren(this, visitor, data);

		#endregion

		#region Operators

		public static bool operator ==(Cursor left, Cursor right) => left.Equals(right);

		public static bool operator !=(Cursor left, Cursor right) => !left.Equals(right);

		#endregion
	}
}