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
// CursorKind.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describes the kind of entity that a cursor refers to.</summary>
	public enum CursorKind
	{
		/// <summary>A declaration whose specific kind is not exposed via this interface.</summary>
		UnexposedDecl = 1,

		/// <summary>A C or C++ struct.</summary>
		StructDecl = 2,

		/// <summary>A C or C++ union.</summary>
		UnionDecl = 3,

		/// <summary>A C++ class.</summary>
		ClassDecl = 4,

		/// <summary>An enumeration.</summary>
		EnumDecl = 5,

		/// <summary>A field (in C) or non-static data member (in C++) in a struct, union, or C++ class.</summary>
		FieldDecl = 6,

		/// <summary>An enumerator constant.</summary>
		EnumConstantDecl = 7,

		/// <summary>A function.</summary>
		FunctionDecl = 8,

		/// <summary>A variable.</summary>
		VarDecl = 9,

		/// <summary>A function or method parameter.</summary>
		ParmDecl = 10,

		/// <summary>An Objective-C @ interface.</summary>
		ObjCInterfaceDecl = 11,

		/// <summary>An Objective-C @ interface for a category.</summary>
		ObjCCategoryDecl = 12,

		/// <summary>An Objective-C @ protocol declaration.</summary>
		ObjCProtocolDecl = 13,

		/// <summary>An Objective-C @ property declaration.</summary>
		ObjCPropertyDecl = 14,

		/// <summary>An Objective-C instance variable.</summary>
		ObjCIvarDecl = 15,

		/// <summary>An Objective-C instance method.</summary>
		ObjCInstanceMethodDecl = 16,

		/// <summary>An Objective-C class method.</summary>
		ObjCClassMethodDecl = 17,

		/// <summary>An Objective-C @ implementation.</summary>
		ObjCImplementationDecl = 18,

		/// <summary>An Objective-C @ implementation for a category.</summary>
		ObjCCategoryImplDecl = 19,

		/// <summary>A typedef.</summary>
		TypedefDecl = 20,

		/// <summary>A C++ class method.</summary>
		CXXMethod = 21,

		/// <summary>A C++ namespace.</summary>
		Namespace = 22,

		/// <summary>A linkage specification, e.g. 'extern "C"'.</summary>
		LinkageSpec = 23,

		/// <summary>A C++ constructor.</summary>
		Constructor = 24,

		/// <summary>A C++ destructor.</summary>
		Destructor = 25,

		/// <summary>A C++ conversion function.</summary>
		ConversionFunction = 26,

		/// <summary>A C++ template type parameter.</summary>
		TemplateTypeParameter = 27,

		/// <summary>A C++ non-type template parameter.</summary>
		NonTypeTemplateParameter = 28,

		/// <summary>A C++ template template parameter.</summary>
		TemplateTemplateParameter = 29,

		/// <summary>A C++ function template.</summary>
		FunctionTemplate = 30,

		/// <summary>A C++ class template.</summary>
		ClassTemplate = 31,

		/// <summary>A C++ class template partial specialization.</summary>
		ClassTemplatePartialSpecialization = 32,

		/// <summary>A C++ namespace alias declaration.</summary>
		NamespaceAlias = 33,

		/// <summary>A C++ using directive.</summary>
		UsingDirective = 34,

		/// <summary>A C++ using declaration.</summary>
		UsingDeclaration = 35,

		/// <summary>A C++ alias declaration</summary>
		TypeAliasDecl = 36,

		/// <summary>An Objective-C @ synthesize definition.</summary>
		ObjCSynthesizeDecl = 37,

		/// <summary>An Objective-C @ dynamic definition.</summary>
		ObjCDynamicDecl = 38,

		/// <summary>An access specifier.</summary>
		CXXAccessSpecifier = 39,

		/// <summary>An access specifier.</summary>
		FirstDecl = 1,

		/// <summary>An access specifier.</summary>
		LastDecl = 39,

		/// <summary>An access specifier.</summary>
		FirstRef = 40,

		/// <summary>An access specifier.</summary>
		ObjCSuperClassRef = 40,

		/// <summary>An access specifier.</summary>
		ObjCProtocolRef = 41,

		/// <summary>An access specifier.</summary>
		ObjCClassRef = 42,

		/// <summary>
		///     A reference to a type declaration. A type reference occurs anywhere where a type is named
		///     but not declared. For example, given: typedef unsigned size_type; size_type size; The typedef
		///     is a declaration of size_type (CXCursor_TypedefDecl), while the type of the variable "size" is
		///     referenced. The cursor referenced by the type of size is the typedef for size_type.
		/// </summary>
		TypeRef = 43,

		/// <summary>
		///     A reference to a type declaration. A type reference occurs anywhere where a type is named
		///     but not declared. For example, given: typedef unsigned size_type; size_type size; The typedef
		///     is a declaration of size_type (CXCursor_TypedefDecl), while the type of the variable "size" is
		///     referenced. The cursor referenced by the type of size is the typedef for size_type.
		/// </summary>
		CXXBaseSpecifier = 44,

		/// <summary>
		///     A reference to a class template, function template, template template parameter, or class
		///     template partial specialization.
		/// </summary>
		TemplateRef = 45,

		/// <summary>A reference to a namespace or namespace alias.</summary>
		NamespaceRef = 46,

		/// <summary>
		///     A reference to a member of a struct, union, or class that occurs in some non-expression
		///     context, e.g., a designated initializer.
		/// </summary>
		MemberRef = 47,


		/// <summary>A reference to a labeled statement. ///</summary>
		LabelRef = 48,

		/// ///
		/// <summary>A reference to an overloaded declaration.</summary>
		OverloadedDeclRef = 49,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		VariableRef = 50,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		LastRef = 50,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		FirstInvalid = 70,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		InvalidFile = 70,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		NoDeclFound = 71,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		NotImplemented = 72,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		InvalidCode = 73,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		LastInvalid = 73,

		/// <summary>
		///     A reference to a variable that occurs in some non-expression context, e.g., a C++ lambda
		///     capture list.
		/// </summary>
		FirstExpr = 100,

		/// <summary>An expression whose specific kind is not exposed via this interface.</summary>
		UnexposedExpr = 100,

		/// <summary>
		///     An expression that refers to some value declaration, such as a function, variable, or
		///     enumerator.
		/// </summary>
		DeclRefExpr = 101,

		/// <summary>An expression that refers to a member of a struct, union, class, Objective-C class, etc.</summary>
		MemberRefExpr = 102,

		/// <summary>An expression that calls a function.</summary>
		CallExpr = 103,

		/// <summary>An expression that sends a message to an Objective-C object or class.</summary>
		ObjCMessageExpr = 104,

		/// <summary>An expression that represents a block literal.</summary>
		BlockExpr = 105,

		/// <summary>An integer literal.</summary>
		IntegerLiteral = 106,

		/// <summary>A floating point number literal.</summary>
		FloatingLiteral = 107,

		/// <summary>An imaginary number literal.</summary>
		ImaginaryLiteral = 108,

		/// <summary>A string literal.</summary>
		StringLiteral = 109,

		/// <summary>A character literal.</summary>
		CharacterLiteral = 110,

		/// <summary>A parenthesized expression, e.g. "(1)".
		///     <para>This AST node is only formed if full location information is requested.</para>
		/// </summary>
		ParenExpr = 111,

		/// <summary>This represents the unary-expression's (except sizeof and alignof).</summary>
		UnaryOperator = 112,

		/// <summary>[C99 6.5.2.1] Array Subscripting.</summary>
		ArraySubscriptExpr = 113,

		/// <summary>
		///     A builtin binary operation expression such as "x + y" or "x
		///     <
		///     = y".
		/// </summary>
		BinaryOperator = 114,

		/// <summary>Compound assignment such as "+=".</summary>
		CompoundAssignOperator = 115,

		/// <summary>The ?: ternary operator.</summary>
		ConditionalOperator = 116,

		/// <summary>
		///     An explicit cast in C (C99 6.5.4) or a C-style cast in C++ (C++ [expr.cast]), which uses
		///     the syntax (Type)expr. For example: (int)f.
		/// </summary>
		CStyleCastExpr = 117,

		/// <summary>[C99 6.5.2.5]</summary>
		CompoundLiteralExpr = 118,

		/// <summary>Describes an C or C++ initializer list.</summary>
		InitListExpr = 119,

		/// <summary>The GNU address of label extension, representing & &label .</summary>
		AddrLabelExpr = 120,

		/// <summary>This is the GNU Statement Expression extension: ({int X=4; X;})</summary>
		StmtExpr = 121,

		/// <summary>Represents a C11 generic selection.</summary>
		GenericSelectionExpr = 122,

		/// <summary>
		///     Implements the GNU __null extension, which is a name for a null pointer constant that has
		///     integral type (e.g., int or long) and is the same size and alignment as a pointer.
		/// </summary>
		GNUNullExpr = 123,

		/// <summary>C++'s static_cast expression.</summary>
		CXXStaticCastExpr = 124,

		/// <summary>C++'s dynamic_cast expression.</summary>
		CXXDynamicCastExpr = 125,

		/// <summary>C++'s reinterpret_cast expression.</summary>
		CXXReinterpretCastExpr = 126,

		/// <summary>C++'s const_cast expression.</summary>
		CXXConstCastExpr = 127,

		/// <summary>
		///     Represents an explicit C++ type conversion that uses "functional" notion (C++
		///     [expr.type.conv]).
		/// </summary>
		CXXFunctionalCastExpr = 128,

		/// <summary>A C++ typeid expression (C++ [expr.typeid]).</summary>
		CXXTypeidExpr = 129,

		/// <summary>[C++ 2.13.5] C++ Boolean Literal.</summary>
		CXXBoolLiteralExpr = 130,

		/// <summary>[C++0x 2.14.7] C++ Pointer Literal.</summary>
		CXXNullPtrLiteralExpr = 131,

		/// <summary>Represents the "this" expression in C++</summary>
		CXXThisExpr = 132,

		/// <summary>
		///     [C++ 15] C++ Throw Expression. This handles 'throw' and 'throw' assignment-expression.
		///     When assignment-expression isn't present, Op will be null.
		/// </summary>
		CXXThrowExpr = 133,

		/// <summary>A new expression for memory allocation and constructor calls, e.g: "new CXXNewExpr(foo)".</summary>
		CXXNewExpr = 134,

		/// <summary>A delete expression for memory deallocation and destructor calls, e.g. "delete[] pArray".</summary>
		CXXDeleteExpr = 135,

		/// <summary>A unary expression. (noexcept, sizeof, or other traits)</summary>
		UnaryExpr = 136,

		/// <summary>An Objective-C string literal i.e. " foo".</summary>
		ObjCStringLiteral = 137,

		/// <summary>An Objective-C @ encode expression.</summary>
		ObjCEncodeExpr = 138,

		/// <summary>An Objective-C @ selector expression.</summary>
		ObjCSelectorExpr = 139,

		/// <summary>An Objective-C @ protocol expression.</summary>
		ObjCProtocolExpr = 140,

		/// <summary>
		///     An Objective-C "bridged" cast expression, which casts between Objective-C pointers and C
		///     pointers, transferring ownership in the process. NSString *str = (__bridge_transfer NSString
		///     *)CFCreateString();
		/// </summary>
		ObjCBridgedCastExpr = 141,

		/// <summary>Represents a C++0x pack expansion that produces a sequence of expressions.</summary>
		PackExpansionExpr = 142,

		/// <summary>Represents an expression that computes the length of a parameter pack.</summary>
		SizeOfPackExpr = 143,

		/// <summary></summary>
		LambdaExpr = 144,

		/// <summary>Objective-c Boolean Literal.</summary>
		ObjCBoolLiteralExpr = 145,

		/// <summary>Represents the "self" expression in an Objective-C method.</summary>
		ObjCSelfExpr = 146,

		/// <summary>OpenMP 4.0 [2.4, Array Section].</summary>
		OMPArraySectionExpr = 147,

		/// <summary>Represents an (...) check.</summary>
		ObjCAvailabilityCheckExpr = 148,

		/// <summary>Represents an (...) check.</summary>
		LastExpr = 148,

		/// <summary>Represents an (...) check.</summary>
		FirstStmt = 200,

		/// <summary>A statement whose specific kind is not exposed via this interface.</summary>
		UnexposedStmt = 200,

		/// <summary>A labelled statement in a function.</summary>
		LabelStmt = 201,

		/// <summary>
		///     A group of statements like { stmt stmt }. This cursor kind is used to describe compound
		///     statements, e.g. function bodies.
		/// </summary>
		CompoundStmt = 202,

		/// <summary>A case statement.</summary>
		CaseStmt = 203,

		/// <summary>A default statement.</summary>
		DefaultStmt = 204,

		/// <summary>An if statement</summary>
		IfStmt = 205,

		/// <summary>A switch statement.</summary>
		SwitchStmt = 206,

		/// <summary>A while statement.</summary>
		WhileStmt = 207,

		/// <summary>A do statement.</summary>
		DoStmt = 208,

		/// <summary>A for statement.</summary>
		ForStmt = 209,

		/// <summary>A goto statement.</summary>
		GotoStmt = 210,

		/// <summary>An indirect goto statement.</summary>
		IndirectGotoStmt = 211,

		/// <summary>A continue statement.</summary>
		ContinueStmt = 212,

		/// <summary>A break statement.</summary>
		BreakStmt = 213,

		/// <summary>A return statement.</summary>
		ReturnStmt = 214,

		/// <summary>A GCC inline assembly statement extension.</summary>
		GCCAsmStmt = 215,

		/// <summary>A GCC inline assembly statement extension.</summary>
		AsmStmt = 215,

		/// <summary>Objective-C's overall</summary>
		ObjCAtTryStmt = 216,

		/// <summary>Objective-C's @ catch statement.</summary>
		ObjCAtCatchStmt = 217,

		/// <summary>Objective-C's @ finally statement.</summary>
		ObjCAtFinallyStmt = 218,

		/// <summary>Objective-C's @ throw statement.</summary>
		ObjCAtThrowStmt = 219,

		/// <summary>Objective-C's @ synchronized statement.</summary>
		ObjCAtSynchronizedStmt = 220,

		/// <summary>Objective-C's autorelease pool statement.</summary>
		ObjCAutoreleasePoolStmt = 221,

		/// <summary>Objective-C's collection statement.</summary>
		ObjCForCollectionStmt = 222,

		/// <summary>C++'s catch statement.</summary>
		CXXCatchStmt = 223,

		/// <summary>C++'s try statement.</summary>
		CXXTryStmt = 224,

		/// <summary>C++'s for (* : *) statement.</summary>
		CXXForRangeStmt = 225,

		/// <summary>Windows Structured Exception Handling's try statement.</summary>
		SEHTryStmt = 226,

		/// <summary>Windows Structured Exception Handling's except statement.</summary>
		SEHExceptStmt = 227,

		/// <summary>Windows Structured Exception Handling's finally statement.</summary>
		SEHFinallyStmt = 228,

		/// <summary>A MS inline assembly statement extension.</summary>
		MSAsmStmt = 229,

		/// <summary>
		///     The null statement ";": C99 6.8.3p3. This cursor kind is used to describe the null
		///     statement.
		/// </summary>
		NullStmt = 230,

		/// <summary>Adaptor class for mixing declarations with statements and expressions.</summary>
		DeclStmt = 231,

		/// <summary>OpenMP parallel directive.</summary>
		OMPParallelDirective = 232,

		/// <summary>OpenMP SIMD directive.</summary>
		OMPSimdDirective = 233,

		/// <summary>OpenMP for directive.</summary>
		OMPForDirective = 234,

		/// <summary>OpenMP sections directive.</summary>
		OMPSectionsDirective = 235,

		/// <summary>OpenMP section directive.</summary>
		OMPSectionDirective = 236,

		/// <summary>OpenMP single directive.</summary>
		OMPSingleDirective = 237,

		/// <summary>OpenMP parallel for directive.</summary>
		OMPParallelForDirective = 238,

		/// <summary>OpenMP parallel sections directive.</summary>
		OMPParallelSectionsDirective = 239,

		/// <summary>OpenMP task directive.</summary>
		OMPTaskDirective = 240,

		/// <summary>OpenMP master directive.</summary>
		OMPMasterDirective = 241,

		/// <summary>OpenMP critical directive.</summary>
		OMPCriticalDirective = 242,

		/// <summary>OpenMP taskyield directive.</summary>
		OMPTaskyieldDirective = 243,

		/// <summary>OpenMP barrier directive.</summary>
		OMPBarrierDirective = 244,

		/// <summary>OpenMP taskwait directive.</summary>
		OMPTaskwaitDirective = 245,

		/// <summary>OpenMP flush directive.</summary>
		OMPFlushDirective = 246,

		/// <summary>Windows Structured Exception Handling's leave statement.</summary>
		SEHLeaveStmt = 247,

		/// <summary>OpenMP ordered directive.</summary>
		OMPOrderedDirective = 248,

		/// <summary>OpenMP atomic directive.</summary>
		OMPAtomicDirective = 249,

		/// <summary>OpenMP for SIMD directive.</summary>
		OMPForSimdDirective = 250,

		/// <summary>OpenMP parallel for SIMD directive.</summary>
		OMPParallelForSimdDirective = 251,

		/// <summary>OpenMP target directive.</summary>
		OMPTargetDirective = 252,

		/// <summary>OpenMP teams directive.</summary>
		OMPTeamsDirective = 253,

		/// <summary>OpenMP taskgroup directive.</summary>
		OMPTaskgroupDirective = 254,

		/// <summary>OpenMP cancellation point directive.</summary>
		OMPCancellationPointDirective = 255,

		/// <summary>OpenMP cancel directive.</summary>
		OMPCancelDirective = 256,

		/// <summary>OpenMP target data directive.</summary>
		OMPTargetDataDirective = 257,

		/// <summary>OpenMP taskloop directive.</summary>
		OMPTaskLoopDirective = 258,

		/// <summary>OpenMP taskloop simd directive.</summary>
		OMPTaskLoopSimdDirective = 259,

		/// <summary>OpenMP distribute directive.</summary>
		OMPDistributeDirective = 260,

		/// <summary>OpenMP target enter data directive.</summary>
		OMPTargetEnterDataDirective = 261,

		/// <summary>OpenMP target exit data directive.</summary>
		OMPTargetExitDataDirective = 262,

		/// <summary>OpenMP target parallel directive.</summary>
		OMPTargetParallelDirective = 263,

		/// <summary>OpenMP target parallel for directive.</summary>
		OMPTargetParallelForDirective = 264,

		/// <summary>OpenMP target update directive.</summary>
		OMPTargetUpdateDirective = 265,

		/// <summary>OpenMP distribute parallel for directive.</summary>
		OMPDistributeParallelForDirective = 266,

		/// <summary>OpenMP distribute parallel for simd directive.</summary>
		OMPDistributeParallelForSimdDirective = 267,

		/// <summary>OpenMP distribute simd directive.</summary>
		OMPDistributeSimdDirective = 268,

		/// <summary>OpenMP target parallel for simd directive.</summary>
		OMPTargetParallelForSimdDirective = 269,

		/// <summary>OpenMP target simd directive.</summary>
		OMPTargetSimdDirective = 270,

		/// <summary>OpenMP teams distribute directive.</summary>
		OMPTeamsDistributeDirective = 271,

		/// <summary>OpenMP teams distribute simd directive.</summary>
		OMPTeamsDistributeSimdDirective = 272,

		/// <summary>OpenMP teams distribute parallel for simd directive.</summary>
		OMPTeamsDistributeParallelForSimdDirective = 273,

		/// <summary>OpenMP teams distribute parallel for directive.</summary>
		OMPTeamsDistributeParallelForDirective = 274,

		/// <summary>OpenMP target teams directive.</summary>
		OMPTargetTeamsDirective = 275,

		/// <summary>OpenMP target teams distribute directive.</summary>
		OMPTargetTeamsDistributeDirective = 276,

		/// <summary>OpenMP target teams distribute parallel for directive.</summary>
		OMPTargetTeamsDistributeParallelForDirective = 277,

		/// <summary>OpenMP target teams distribute parallel for simd directive.</summary>
		OMPTargetTeamsDistributeParallelForSimdDirective = 278,

		/// <summary>OpenMP target teams distribute simd directive.</summary>
		OMPTargetTeamsDistributeSimdDirective = 279,

		/// <summary>OpenMP target teams distribute simd directive.</summary>
		LastStmt = 279,

		/// <summary>
		///     Cursor that represents the translation unit itself. The translation unit cursor exists
		///     primarily to act as the root cursor for traversing the contents of a translation unit.
		/// </summary>
		TranslationUnit = 300,

		/// <summary>
		///     Cursor that represents the translation unit itself.
		///     <para>
		///         The translation unit cursor exists primarily to act as the root cursor for traversing the
		///         contents of a translation unit.
		///     </para>
		/// </summary>
		FirstAttr = 400,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		UnexposedAttr = 400,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		IBActionAttr = 401,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		IBOutletAttr = 402,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		IBOutletCollectionAttr = 403,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CXXFinalAttr = 404,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CXXOverrideAttr = 405,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		AnnotateAttr = 406,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		AsmLabelAttr = 407,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		PackedAttr = 408,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		PureAttr = 409,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		ConstAttr = 410,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		NoDuplicateAttr = 411,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CUDAConstantAttr = 412,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CUDADeviceAttr = 413,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CUDAGlobalAttr = 414,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CUDAHostAttr = 415,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		CUDASharedAttr = 416,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		VisibilityAttr = 417,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		DLLExport = 418,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		DLLImport = 419,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		LastAttr = 419,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		PreprocessingDirective = 500,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		MacroDefinition = 501,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		MacroExpansion = 502,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		MacroInstantiation = 502,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		InclusionDirective = 503,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		FirstPreprocessing = 500,

		/// <summary>An attribute whose specific kind is not exposed via this interface.</summary>
		LastPreprocessing = 503,

		/// <summary>A module import declaration.</summary>
		ModuleImportDecl = 600,

		/// <summary>A module import declaration.</summary>
		TypeAliasTemplateDecl = 601,

		/// <summary>A static_assert or _Static_assert node</summary>
		StaticAssert = 602,

		/// <summary>a friend declaration.</summary>
		FriendDecl = 603,

		/// <summary>a friend declaration.</summary>
		FirstExtraDecl = 600,

		/// <summary>a friend declaration.</summary>
		LastExtraDecl = 603,

		/// <summary>A code completion overload candidate.</summary>
		OverloadCandidate = 700
	}
}