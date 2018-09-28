using System;
using System.Runtime.InteropServices;
using System.Security;

namespace LibClang
{
	[SuppressUnmanagedCodeSecurity]
	public static class Clang
	{
		/// <summary>
		/// The library name, without path or file extension.
		/// </summary>
		public const string LIBRARY = "libclang";

		/// <summary>
		/// Initializes the <see cref="Clang"/> class.
		/// </summary>
		static Clang()
		{
			Console.WriteLine(NativeLoader.Load(LIBRARY));
		}

		#region Methods

		/// <summary>
		///     Return a version string, suitable for showing to a user, but not intended to be parsed
		///     (the format is not guaranteed to be stable).
		/// </summary>
		public static string GetClangVersion() => GetClangVersionPrivate();

		/// <summary>Retrieve the last modification time of the given file.</summary>
		public static DateTime GetFileTime(File file)
		{
			var time = GetFileTimeLong(file);
			return new DateTime(1970, 1, 1).AddSeconds(time);
		}

		/// <summary>Create a ModuleMapDescriptor object.</summary>
		public static ModuleMapDescriptor ModuleMapDescriptorCreate() => ModuleMapDescriptorCreate(0);

		#endregion

		#region Native

		/// <summary>
		///     Annotate the given set of tokens by providing cursors for each token that can be mapped to
		///     a specific entity within the abstract syntax tree.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_annotateTokens", CallingConvention = CallingConvention.Cdecl)]
		public static extern void AnnotateTokens(TranslationUnit unit,
			[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
			Token[] tokens, uint numTokens, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
			Cursor[] cursors);

		/// <summary>
		///     a Comment_BlockCommand AST node. argument index (zero-based). text of the specified
		///     word-like argument.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_BlockCommandComment_getArgText",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String BlockCommandCommentGetArgText(Comment comment, uint argIdx);

		/// <summary>A Comment_BlockCommand AST node. name of the block command.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_BlockCommandComment_getCommandName",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String BlockCommandCommentGetCommandName(Comment comment);

		/// <summary>a Comment_BlockCommand AST node. number of word-like arguments.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_BlockCommandComment_getNumArgs",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint BlockCommandCommentGetNumArgs(Comment comment);

		/// <summary>
		///     a Comment_BlockCommand or Comment_VerbatimBlockCommand AST node. paragraph argument of the
		///     block command.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_BlockCommandComment_getParagraph",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Comment BlockCommandCommentGetParagraph(Comment comment);

		/// <summary>Perform code completion at a given location in a translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteAt", CallingConvention = CallingConvention.Cdecl)]
		public static extern ref CodeCompleteResults CodeCompleteAt(TranslationUnit unit,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string completeFilename, uint completeLine, uint completeColumn,
			[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
			UnsavedFile[] unsavedFiles, uint numUnsavedFiles, CodeCompleteFlags options);

		/// <summary>Returns the cursor kind for the container for the current code completion context.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteGetContainerKind",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CursorKind CodeCompleteGetContainerKind(ref CodeCompleteResults results,
			out bool isIncomplete);

		/// <summary>
		///     Returns the USR for the container for the current code completion context. If there is not
		///     a container for the current context, this function will return the empty string. the code
		///     completion results to query the USR for the container
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteGetContainerUSR",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CodeCompleteGetContainerUSR(ref CodeCompleteResults results);

		/// <summary>Determines what completions are appropriate for the context the given code completion.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteGetContexts", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong CodeCompleteGetContexts(ref CodeCompleteResults results);

		/// <summary>Retrieve a diagnostic associated with the given code completion.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteGetDiagnostic",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Diagnostic CodeCompleteGetDiagnostic(ref CodeCompleteResults results, uint index);

		/// <summary>
		///     Determine the number of diagnostics produced prior to the location where code completion
		///     was performed.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteGetNumDiagnostics",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint CodeCompleteGetNumDiagnostics(ref CodeCompleteResults results);

		/// <summary>
		///     Returns the currently-entered selector for an Objective-C message send, formatted like
		///     "initWithFoo:bar:".
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_codeCompleteGetObjCSelector",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CodeCompleteGetObjCSelector(ref CodeCompleteResults results);

		/// <summary>AST node of any kind.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Comment_getChild", CallingConvention = CallingConvention.Cdecl)]
		public static extern Comment CommentGetChild(Comment comment, uint childIdx);

		/// <summary>AST node of any kind.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Comment_getKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern CommentKind CommentGetKind(Comment comment);

		/// <summary>AST node of any kind.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Comment_getNumChildren", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint CommentGetNumChildren(Comment comment);

		/// <summary>
		///     A CommentParagraph node is considered whitespace if it contains only CommentText nodes
		///     that are empty or whitespace.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Comment_isWhitespace", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CommentIsWhitespace(Comment comment);

		/// <summary>Free the given compilation database</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompilationDatabase_dispose",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void CompilationDatabaseDispose(CompilationDatabase database);

		/// <summary>Creates a compilation database from the database found in directory buildDir.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompilationDatabase_fromDirectory",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CompilationDatabase CompilationDatabaseFromDirectory(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string buildDir, out CompilationDatabaseError errorCode);

		/// <summary>Get all the compile commands in the given compilation database.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompilationDatabase_getAllCompileCommands",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CompileCommands CompilationDatabaseGetAllCompileCommands(CompilationDatabase arg1);

		/// <summary>Find the compile commands used for a file.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompilationDatabase_getCompileCommands",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CompileCommands CompilationDatabaseGetCompileCommands(CompilationDatabase arg1,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string completeFileName);

		/// <summary>
		///     Get the I'th argument value in the compiler invocations Invariant : - argument 0 is the
		///     compiler executable
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getArg", CallingConvention = CallingConvention.Cdecl)]
		public static extern String CompileCommandGetArg(CompileCommand command, uint index);

		/// <summary>Get the working directory where the CompileCommand was executed from</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getDirectory",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CompileCommandGetDirectory(CompileCommand command);

		/// <summary>Get the filename associated with the CompileCommand.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getFilename",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CompileCommandGetFilename(CompileCommand command);

		/// <summary>Get the I'th mapped source content for the compiler invocation.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getMappedSourceContent",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CompileCommandGetMappedSourceContent(CompileCommand command, uint index);

		/// <summary>Get the I'th mapped source path for the compiler invocation.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getMappedSourcePath",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CompileCommandGetMappedSourcePath(CompileCommand command, uint index);

		/// <summary>Get the number of arguments in the compiler invocation.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getNumArgs",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint CompileCommandGetNumArgs(CompileCommand command);

		/// <summary>Get the number of source mappings for the compiler invocation.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommand_getNumMappedSources",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint CompileCommandGetNumMappedSources(CompileCommand command);

		/// <summary>Free the given CompileCommands</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommands_dispose", CallingConvention = CallingConvention.Cdecl)]
		public static extern void CompileCommandsDispose(CompileCommands commands);

		/// <summary>Get the I'th CompileCommand for a file</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommands_getCommand",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CompileCommand CompileCommandsGetCommand(CompileCommands commands, uint index);

		/// <summary>Get the number of CompileCommand we have for a file</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CompileCommands_getSize", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint CompileCommandsGetSize(CompileCommands commands);

		/// <summary>Determine if a C++ constructor is a converting constructor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXConstructor_isConvertingConstructor",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ConstructorIsConvertingConstructor(Cursor cursor);

		/// <summary>Determine if a C++ constructor is a copy constructor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXConstructor_isCopyConstructor",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ConstructorIsCopyConstructor(Cursor cursor);

		/// <summary>Determine if a C++ constructor is the default constructor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXConstructor_isDefaultConstructor",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ConstructorIsDefaultConstructor(Cursor cursor);

		/// <summary>Determine if a C++ constructor is a move constructor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXConstructor_isMoveConstructor",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ConstructorIsMoveConstructor(Cursor cursor);

		/// <summary>Construct a USR for a specified Objective-C category.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_constructUSR_ObjCCategory",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String ConstructUSRObjCCategory(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string className, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string categoryName);

		/// <summary>Construct a USR for a specified Objective-C class.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_constructUSR_ObjCClass", CallingConvention = CallingConvention.Cdecl)]
		public static extern String ConstructUSRObjCClass(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string className);

		/// <summary>
		///     Construct a USR for a specified Objective-C instance variable and the USR for its
		///     containing class.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_constructUSR_ObjCIvar", CallingConvention = CallingConvention.Cdecl)]
		public static extern String ConstructUSRObjCIvar(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string name, String classUSR);

		/// <summary>Construct a USR for a specified Objective-C method and the USR for its containing class.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_constructUSR_ObjCMethod", CallingConvention = CallingConvention.Cdecl)]
		public static extern String ConstructUSRObjCMethod(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string name, uint isInstanceMethod, String classUSR);

		/// <summary>Construct a USR for a specified Objective-C property and the USR for its containing class.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_constructUSR_ObjCProperty",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String ConstructUSRObjCProperty(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string property, String classUSR);

		/// <summary>Construct a USR for a specified Objective-C protocol.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_constructUSR_ObjCProtocol",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String ConstructUSRObjCProtocol(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string protocolName);

		/// <summary>Creates an empty CursorSet.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_createCXCursorSet", CallingConvention = CallingConvention.Cdecl)]
		public static extern CursorSet CreateCursorSet();

		/// <summary>Provides a shared context for creating translation units.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_createIndex", CallingConvention = CallingConvention.Cdecl)]
		public static extern Index CreateIndex(bool excludeDeclarations, bool displayDiagnostics);

		/// <summary>
		///     Same as clang_createTranslationUnit2, but returns the TranslationUnit instead of an error
		///     code. In case of an error this routine returns a NULL TranslationUnit, without further detailed
		///     error codes.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_createTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern TranslationUnit CreateTranslationUnit(Index index,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string astFilename);

		/// <summary>
		///     Create a translation unit from an AST file ( -emit-ast). A non-NULL pointer to store the
		///     created TranslationUnit. Zero on success, otherwise returns an error code.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_createTranslationUnit2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode CreateTranslationUnit2(Index index,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string astFilename, out TranslationUnit unit);

		/// <summary>
		///     Return the TranslationUnit for a given source file and the provided command line arguments
		///     one would pass to the compiler.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_createTranslationUnitFromSourceFile",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern TranslationUnit CreateTranslationUnitFromSourceFile(Index index,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string sourceFilename, int numClangCommandLineArgs, string[] clangCommandLineArgs, uint numUnsavedFiles,
			UnsavedFile[] unsavedFiles);

		/// <summary>
		///     If cursor is a statement declaration tries to evaluate the statement and if its variable,
		///     tries to evaluate its initializer, into its corresponding type.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_Evaluate", CallingConvention = CallingConvention.Cdecl)]
		public static extern EvalResult CursorEvaluate(Cursor cursor);

		/// <summary>
		///     Retrieve the argument cursor of a function or method. The argument cursor can be
		///     determined for calls as well as for declarations of functions or methods. For other cursors and
		///     for invalid indices, an invalid cursor is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getArgument", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor CursorGetArgument(Cursor cursor, uint index);

		/// <summary>
		///     Given a cursor that represents a documentable entity (e.g., declaration), return the
		///     associated \ brief paragraph; otherwise return the first paragraph.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getBriefCommentText",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String CursorGetBriefCommentText(Cursor cursor);

		/// <summary>
		///     Given a cursor that represents a declaration, return the associated comment's source
		///     range. The range may include multiple consecutive comments with whitespace in between.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getCommentRange", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange CursorGetCommentRange(Cursor cursor);

		/// <summary>
		///     Retrieve the Strings representing the mangled symbols of the C++ constructor or destructor
		///     at the cursor.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getCXXManglings", CallingConvention = CallingConvention.Cdecl)]
		public static extern StringSet CursorGetCXXManglings(Cursor cursor);

		/// <summary>Retrieve the String representing the mangled name of the cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getMangling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String CursorGetMangling(Cursor cursor);

		/// <summary>Given a Cursor_ModuleImportDecl cursor, return the associated module.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getModule", CallingConvention = CallingConvention.Cdecl)]
		public static extern Module CursorGetModule(Cursor cursor);

		/// <summary>
		///     Retrieve the number of non-variadic arguments associated with a given cursor. The number
		///     of arguments can be determined for calls as well as for declarations of functions or methods.
		///     For other cursors -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getNumArguments", CallingConvention = CallingConvention.Cdecl)]
		public static extern int CursorGetNumArguments(Cursor cursor);

		/// <summary>
		///     Returns the number of template args of a function decl representing a template
		///     specialization. If the argument cursor cannot be converted into a template function
		///     declaration, -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getNumTemplateArguments",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern int CursorGetNumTemplateArguments(Cursor cursor);

		/// <summary>
		///     Given a cursor that represents an Objective-C method or parameter declaration, return the
		///     associated Objective-C qualifiers for the return type or the parameter respectively.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getObjCDeclQualifiers",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ObjCDeclQualifierKind CursorGetObjCDeclQualifiers(Cursor cursor);

		/// <summary>
		///     Retrieve the Strings representing the mangled symbols of the ObjC class interface or
		///     implementation at the cursor.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getObjCManglings", CallingConvention = CallingConvention.Cdecl)]
		public static extern StringSet CursorGetObjCManglings(Cursor cursor);

		/// <summary>
		///     Given a cursor that represents a property declaration, return the associated property
		///     attributes.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getObjCPropertyAttributes",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ObjCPropertyAttrKind CursorGetObjCPropertyAttributes(Cursor cursor, uint reserved);

		/// <summary>
		///     If the cursor points to a selector identifier in an Objective-C method or message
		///     expression, this returns the selector index. After getting a cursor with #clang_getCursor, this
		///     can be called to determine if the location points to a selector identifier. The selector index
		///     if the cursor is an Objective-C method or message expression and the cursor is pointing to a
		///     selector identifier, or -1 otherwise.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getObjCSelectorIndex",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern int CursorGetObjCSelectorIndex(Cursor cursor);

		/// <summary>
		///     Return the offset of the field represented by the Cursor. If the cursor is not a field
		///     declaration, -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getOffsetOfField", CallingConvention = CallingConvention.Cdecl)]
		public static extern long CursorGetOffsetOfField(Cursor cursor);

		/// <summary>
		///     Given a cursor that represents a documentable entity (e.g., declaration), return the
		///     associated parsed comment as a Comment_FullComment AST node.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getParsedComment", CallingConvention = CallingConvention.Cdecl)]
		public static extern Comment CursorGetParsedComment(Cursor cursor);

		/// <summary>
		///     Given a cursor that represents a declaration, return the associated comment text,
		///     including comment markers.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getRawCommentText", CallingConvention = CallingConvention.Cdecl)]
		public static extern String CursorGetRawCommentText(Cursor cursor);

		/// <summary>
		///     Given a cursor pointing to an Objective-C message or property reference, or C++ method
		///     call, returns the Type of the receiver.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getReceiverType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type CursorGetReceiverType(Cursor cursor);

		/// <summary>
		///     Retrieve a range for a piece that forms the cursors spelling name. Most of the times there
		///     is only one range for the complete spelling but for Objective-C methods and Objective-C message
		///     expressions, there are multiple pieces for each selector identifier. the index of the spelling
		///     name piece. If this is greater than the actual number of pieces, it will return a NULL
		///     (invalid) range. Reserved.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getSpellingNameRange",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange CursorGetSpellingNameRange(Cursor cursor, uint pieceIndex, uint options);

		/// <summary>
		///     Returns the storage class for a function or variable declaration. If the passed in Cursor
		///     is not a function or variable declaration, _SC_Invalid is returned else the storage class.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getStorageClass", CallingConvention = CallingConvention.Cdecl)]
		public static extern StorageClass CursorGetStorageClass(Cursor cursor);

		/// <summary>
		///     Retrieve the kind of the I'th template argument of the Cursor C. If the argument Cursor
		///     does not represent a FunctionDecl, an invalid template argument kind is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getTemplateArgumentKind",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern TemplateArgumentKind CursorGetTemplateArgumentKind(Cursor cursor, uint index);

		/// <summary>
		///     Retrieve a Type representing the type of a TemplateArgument of a function decl
		///     representing a template specialization. If the argument Cursor does not represent a
		///     FunctionDecl whose I'th template argument has a kind of TemplateArgKind_Integral, an invalid
		///     type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getTemplateArgumentType",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Type CursorGetTemplateArgumentType(Cursor cursor, uint index);

		/// <summary>
		///     Retrieve the value of an Integral TemplateArgument (of a function decl representing a
		///     template specialization) as an unsigned long long. It is undefined to call this function on a
		///     Cursor that does not represent a FunctionDecl or whose I'th template argument is not an
		///     integral value.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getTemplateArgumentUnsignedValue",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong CursorGetTemplateArgumentUnsignedValue(Cursor cursor, uint index);

		/// <summary>
		///     Retrieve the value of an Integral TemplateArgument (of a function decl representing a
		///     template specialization) as a signed long long. It is undefined to call this function on a
		///     Cursor that does not represent a FunctionDecl or whose I'th template argument is not an
		///     integral value.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getTemplateArgumentValue",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern long CursorGetTemplateArgumentValue(Cursor cursor, uint index);

		/// <summary>Returns the translation unit that a cursor originated from.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_getTranslationUnit",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern TranslationUnit CursorGetTranslationUnit(Cursor cursor);

		/// <summary>Determine whether the given cursor has any attributes.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_hasAttrs", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorHasAttrs(Cursor cursor);

		/// <summary>Determine whether the given cursor represents an anonymous record declaration.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isAnonymous", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsAnonymous(Cursor cursor);

		/// <summary>Returns non-zero if the cursor specifies a Record member that is a bitfield.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isBitField", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsBitField(Cursor cursor);

		/// <summary>
		///     Given a cursor pointing to a C++ method call or an Objective-C message, returns non-zero
		///     if the method/message is "dynamic", meaning: For a C++ method: the call is virtual. For an
		///     Objective-C message: the receiver is an object instance, not 'super' or a specific class. If
		///     the method/message is "static" or the cursor does not point to a method/message, it will return
		///     zero.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isDynamicCall", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsDynamicCall(Cursor cursor);

		/// <summary>
		///     Returns <c>true</c> if the given cursor points to a symbol marked with
		///     external_source_symbol attribute.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isExternalSymbol", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CursorIsExternalSymbol(Cursor cursor, out String language, out String definedIn,
			out bool isGenerated);

		/// <summary>Determine whether a  Cursor that is a function declaration, is an inline declaration.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isFunctionInlined", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsFunctionInlined(Cursor cursor);

		/// <summary>Determine whether a  Cursor that is a macro, is a builtin one.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isMacroBuiltin", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsMacroBuiltin(Cursor cursor);

		/// <summary>Determine whether a  Cursor that is a macro, is function like.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isMacroFunctionLike",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsMacroFunctionLike(Cursor cursor);

		/// <summary>Returns non-zero if cursor is null.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isNull", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsNull(Cursor cursor);

		/// <summary>
		///     Given a cursor that represents an Objective-C method or property declaration, return
		///     non-zero if the declaration was affected by " @ optional". Returns zero if the cursor is not
		///     such a declaration or it is " @ required".
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isObjCOptional", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsObjCOptional(Cursor cursor);

		/// <summary>Returns non-zero if the given cursor is a variadic function or method.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Cursor_isVariadic", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorIsVariadic(Cursor cursor);

		/// <summary>
		///     Queries a CursorSet to see if it contains a specific Cursor. Returns <c>true</c> if the
		///     set contains the specified cursor.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXCursorSet_contains", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorSetContains(CursorSet set, Cursor cursor);

		/// <summary>
		///     Inserts a Cursor into a CursorSet. <c>false</c> if the Cursor was already in the set, and
		///     <c>true</c> otherwise.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXCursorSet_insert", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CursorSetInsert(CursorSet set, Cursor cursor);

		/// <summary>
		///     Returns a default set of code-completion options that can be passed to
		///     clang_codeCompleteAt().
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_defaultCodeCompleteOptions",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint DefaultCodeCompleteOptions();

		/// <summary>
		///     Retrieve the set of display options most similar to the default behavior of the clang
		///     compiler.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_defaultDiagnosticDisplayOptions",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint DefaultDiagnosticDisplayOptions();

		/// <summary>
		///     Returns the set of flags that is suitable for parsing a translation unit that is being
		///     edited.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_defaultEditingTranslationUnitOptions",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint DefaultEditingTranslationUnitOptions();

		/// <summary>Returns the set of flags that is suitable for reparsing a translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_defaultReparseOptions", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint DefaultReparseOptions(TranslationUnit unit);

		/// <summary>Returns the set of flags that is suitable for saving a translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_defaultSaveOptions", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint DefaultSaveOptions(TranslationUnit unit);

		/// <summary>Free the given set of code-completion results.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeCodeCompleteResults",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeCodeCompleteResults(ref CodeCompleteResults results);

		/// <summary>Disposes a CursorSet and releases its associated memory.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeCXCursorSet", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeCursorSet(CursorSet set);

		/// <summary>Free the memory associated with a PlatformAvailability structure.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeCXPlatformAvailability",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeCXPlatformAvailability(ref PlatformAvailability availability);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeCXTUResourceUsage", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeCXTUResourceUsage(TUResourceUsage usage);

		/// <summary>Destroy a diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeDiagnostic", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeDiagnostic(Diagnostic diagnostic);

		/// <summary>Release a DiagnosticSet and all of its contained diagnostics.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeDiagnosticSet", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeDiagnosticSet(DiagnosticSet set);

		/// <summary>
		///     Destroy the given index. The index must not be destroyed until all of the translation
		///     units created within that index have been destroyed.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeIndex", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeIndex(Index index);

		/// <summary>Free the set of overridden cursors returned by clang_getOverriddenCursors().</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeOverriddenCursors", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeOverriddenCursors(IntPtr overridden);

		/// <summary>Destroy the given SourceRangeList.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeSourceRangeList", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeSourceRangeList(ref SourceRangeList ranges);

		/// <summary>Free the given string.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeString", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeString(String str);

		/// <summary>Free the given string set.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeStringSet", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeStringSet(ref StringSet set);

		/// <summary>Free the given set of tokens.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeTokens", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeTokens(TranslationUnit unit,
			[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
			Token[] tokens, uint numTokens);

		/// <summary>Destroy the specified TranslationUnit object.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_disposeTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DisposeTranslationUnit(TranslationUnit unit);

		[DllImport(LIBRARY, EntryPoint = "clang_enableStackTraces", CallingConvention = CallingConvention.Cdecl)]
		public static extern void EnableStackTraces();

		/// <summary>Determine if an enum declaration refers to a scoped enum.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EnumDecl_isScoped", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool EnumDeclIsScoped(Cursor cursor);

		/// <summary>Determine whether two cursors are equivalent.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_equalCursors", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool EqualCursors(Cursor cursor, Cursor arg2);

		/// <summary>
		///     Determine whether two source locations, which must refer into the same translation unit,
		///     refer to exactly the same point in the source code. non-zero if the source locations refer to
		///     the same location, zero if they refer to different locations.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_equalLocations", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool EqualLocations(SourceLocation loc1, SourceLocation loc2);

		/// <summary>
		///     Determine whether two ranges are equivalent. non-zero if the ranges are the same, zero if
		///     they differ.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_equalRanges", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool EqualRanges(SourceRange range1, SourceRange range2);

		/// <summary>
		///     Determine whether two Types represent the same type. non-zero if the Types represent the
		///     same type and zero otherwise.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_equalTypes", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool EqualTypes(Type a, Type b);

		/// <summary>Disposes the created Eval memory.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_dispose", CallingConvention = CallingConvention.Cdecl)]
		public static extern void EvalResultDispose(EvalResult result);

		/// <summary>Returns the evaluation result as double if the kind is double.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_getAsDouble", CallingConvention = CallingConvention.Cdecl)]
		public static extern double EvalResultGetAsDouble(EvalResult result);

		/// <summary>Returns the evaluation result as integer if the kind is Int.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_getAsInt", CallingConvention = CallingConvention.Cdecl)]
		public static extern int EvalResultGetAsInt(EvalResult result);

		/// <summary>
		///     Returns the evaluation result as a long long integer if the kind is Int. This prevents
		///     overflows that may happen if the result is returned with clang_EvalResult_getAsInt.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_getAsLongLong", CallingConvention = CallingConvention.Cdecl)]
		public static extern long EvalResultGetAsLongLong(EvalResult result);

		/// <summary>Returns the evaluation result as a constant string if the kind is other than Int or float.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_getAsStr", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public static extern string EvalResultGetAsStr(EvalResult result);

		/// <summary>
		///     Returns the evaluation result as an unsigned integer if the kind is Int and
		///     clang_EvalResult_isUnsignedInt is non-zero.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_getAsUnsigned", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong EvalResultGetAsUnsigned(EvalResult result);

		/// <summary>Returns the kind of the evaluated result.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_getKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern EvalResultKind EvalResultGetKind(EvalResult result);

		/// <summary>
		///     Returns a non-zero value if the kind is Int and the evaluation result resulted in an
		///     unsigned integer.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_EvalResult_isUnsignedInt", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool EvalResultIsUnsignedInt(EvalResult result);

		/// <summary>Executes the given action on the same thread.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_executeOnThread", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ExecuteOnThread(Action fn, IntPtr userData, uint stackSize);

		/// <summary>Determine if a C++ field is declared 'mutable'.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXField_isMutable", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool FieldIsMutable(Cursor cursor);

		/// <summary>Returns non-zero if the file1 and file2 point to the same file, or they are both NULL.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_File_isEqual", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool FileIsEqual(File file1, File file2);

		/// <summary>
		///     Find #import/#include directives in a specific file. translation unit containing the file
		///     to query. to search for #import/#include directives. callback that will receive pairs of
		///     Cursor/CXSourceRange for each directive found. one of the Result enumerators.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_findIncludesInFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result FindIncludesInFile(TranslationUnit unit, File file, CursorAndRangeVisitor visitor);

		/// <summary>
		///     Find references of a declaration in a specific file. pointing to a declaration or a
		///     reference of one. to search for references.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_findReferencesInFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result FindReferencesInFile(Cursor cursor, File file, CursorAndRangeVisitor visitor);

		/// <summary>
		///     Format the given diagnostic in a manner that is suitable for display. This routine will
		///     format the given diagnostic to a string, rendering the diagnostic according to the various
		///     options given.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_formatDiagnostic", CallingConvention = CallingConvention.Cdecl)]
		public static extern String FormatDiagnostic(Diagnostic diagnostic, DiagnosticDisplayOptions options);

		/// <summary>Free memory allocated by LibClang.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_free", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Free(IntPtr buffer);

		/// <summary>Convert a given full parsed comment to an HTML fragment.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_FullComment_getAsHTML", CallingConvention = CallingConvention.Cdecl)]
		public static extern String FullCommentGetAsHTML(Comment comment);

		/// <summary>Convert a given full parsed comment to an XML document.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_FullComment_getAsXML", CallingConvention = CallingConvention.Cdecl)]
		public static extern String FullCommentGetAsXML(Comment comment);


		/// <summary>Returns the address space of the given type.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getAddressSpace", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetAddressSpace(Type type);


		/// <summary>
		///     Retrieve all ranges from all files that were skipped by the preprocessor. The preprocessor
		///     will skip lines when they are surrounded by an if/ifdef/ifndef directive whose condition does
		///     not evaluate to true.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getAllSkippedRanges", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRangeList GetAllSkippedRanges(TranslationUnit tu);

		/// <summary>
		///     Retrieve the type of a parameter of a function type. If a non-function type is passed in
		///     or the function does not have enough parameters, an invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getArgType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetArgType(Type type, uint index);

		/// <summary>
		///     Return the element type of an array type. If a non-array type is passed in, an invalid
		///     type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getArrayElementType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetArrayElementType(Type type);

		/// <summary>
		///     Return the array size of a constant array. If a non-array type is passed in, -1 is
		///     returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getArraySize", CallingConvention = CallingConvention.Cdecl)]
		public static extern long GetArraySize(Type type);

		/// <summary>Return the timestamp for use with Clang's -fbuild-session-timestamp= option.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getBuildSessionTimestamp", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong GetBuildSessionTimestamp();

		/// <summary>
		///     Retrieve the canonical cursor corresponding to the given cursor.
		///     <para>
		///         In the C family of languages, many kinds of entities can be declared several times within
		///         a single translation unit.
		///     </para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCanonicalCursor", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetCanonicalCursor(Cursor cursor);

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
		[DllImport(LIBRARY, EntryPoint = "clang_getCanonicalType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetCanonicalType(Type type);

		/// <summary>Retrieve the child diagnostics of a Diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getChildDiagnostics", CallingConvention = CallingConvention.Cdecl)]
		public static extern DiagnosticSet GetChildDiagnostics(Diagnostic diagnostic);

		[DllImport(LIBRARY, EntryPoint = "clang_getClangVersion", CallingConvention = CallingConvention.Cdecl)]
		private static extern String GetClangVersionPrivate();

		/// <summary>
		///     Retrieve the annotation associated with the given completion string. the completion string
		///     to query. the 0-based index of the annotation of the completion string. annotation string
		///     associated with the completion at index annotation_number, or a NULL string if that annotation
		///     is not available.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionAnnotation", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCompletionAnnotation(CompletionString completionString, uint annotationNumber);

		/// <summary>
		///     Determine the availability of the entity that this code-completion string refers to. The
		///     completion string to query. The availability of the completion string.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionAvailability",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern AvailabilityKind GetCompletionAvailability(CompletionString completionString);

		/// <summary>
		///     Retrieve the brief documentation comment attached to the declaration that corresponds to
		///     the given completion string.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionBriefComment",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCompletionBriefComment(CompletionString completionString);

		/// <summary>
		///     Retrieve the completion string associated with a particular chunk within a completion
		///     string. the completion string to query. the 0-based index of the chunk in the completion
		///     string. the completion string associated with the chunk at index chunk_number.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionChunkCompletionString",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CompletionString GetCompletionChunkCompletionString(CompletionString completionString,
			uint chunkNumber);

		/// <summary>
		///     Determine the kind of a particular chunk within a completion string. the completion string
		///     to query. the 0-based index of the chunk in the completion string. the kind of the chunk at the
		///     index chunk_number.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionChunkKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern CompletionChunkKind GetCompletionChunkKind(CompletionString completionString,
			uint chunkNumber);

		/// <summary>
		///     Retrieve the text associated with a particular chunk within a completion string. the
		///     completion string to query. the 0-based index of the chunk in the completion string. the text
		///     associated with the chunk at index chunk_number.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionChunkText", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCompletionChunkText(CompletionString completionString, uint chunkNumber);

		/// <summary>
		///     Retrieve the number of annotations associated with the given completion string. the
		///     completion string to query. the number of annotations associated with the given completion
		///     string.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionNumAnnotations",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetCompletionNumAnnotations(CompletionString completionString);

		/// <summary>Retrieve the parent context of the given completion string.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionParent", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCompletionParent(CompletionString completionString, out CursorKind kind);

		/// <summary>
		///     Determine the priority of this code completion. query. The priority of this completion
		///     string. Smaller values indicate higher-priority (more likely) completions.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCompletionPriority", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetCompletionPriority(CompletionString completionString);

		/// <summary>Retrieve the character data associated with the given string.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCString", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public static extern string GetCString(String str);

		/// <summary>
		///     Map a source location to the cursor that describes the entity at that location in the
		///     source code.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursor", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetCursor(TranslationUnit unit, SourceLocation location);

		/// <summary>
		///     Determine the availability of the entity that this cursor refers to, taking the current
		///     target platform into account. The cursor to query. The availability of the cursor.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorAvailability", CallingConvention = CallingConvention.Cdecl)]
		public static extern AvailabilityKind GetCursorAvailability(Cursor cursor);

		/// <summary>
		///     Retrieve a completion string for an arbitrary declaration or macro definition cursor. The
		///     cursor to query. A non-context-sensitive completion string for declaration and macro definition
		///     cursors, or NULL for other kinds of cursors.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorCompletionString",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CompletionString GetCursorCompletionString(Cursor cursor);

		/// <summary>
		///     For a cursor that is either a reference to or a declaration of some entity, retrieve a
		///     cursor that describes the definition of that entity. Some entities can be declared multiple
		///     times within a translation unit, but only one of those declarations can also be a definition.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorDefinition", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetCursorDefinition(Cursor cursor);

		/// <summary>
		///     Retrieve the display name for the entity referenced by this cursor. The display name
		///     contains extra information that helps identify the cursor, such as the parameters of a function
		///     or template or the arguments of a class template specialization.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorDisplayName", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCursorDisplayName(Cursor cursor);

		/// <summary>
		///     Retrieve the exception specification type associated with a given cursor. This only
		///     returns a valid result if the cursor refers to a function or method.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorExceptionSpecificationType",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CursorExceptionKind GetCursorExceptionSpecificationType(Cursor cursor);

		/// <summary>
		///     Retrieve the physical extent of the source construct referenced by the given cursor. The
		///     extent of a cursor starts with the file/line/column pointing at the first character within the
		///     source construct that the cursor refers to and ends with the last character within that source
		///     construct. For a declaration, the extent covers the declaration itself. For a reference, the
		///     extent covers the location of the reference (e.g., where the referenced entity was actually
		///     used).
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorExtent", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange GetCursorExtent(Cursor cursor);

		/// <summary>Retrieve the kind of the given cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern CursorKind GetCursorKind(Cursor cursor);

		/// <summary>
		///     CINDEX_DEBUG Debugging facilities These routines are used for testing and debugging, only,
		///     and should not be relied upon. @ {
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorKindSpelling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCursorKindSpelling(CursorKind kind);

		/// <summary>Determine the "language" of the entity referred to by a given cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorLanguage", CallingConvention = CallingConvention.Cdecl)]
		public static extern LanguageKind GetCursorLanguage(Cursor cursor);

		/// <summary>Determine the lexical parent of the given cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorLexicalParent", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetCursorLexicalParent(Cursor cursor);

		/// <summary>Determine the linkage of the entity referred to by a given cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorLinkage", CallingConvention = CallingConvention.Cdecl)]
		public static extern LinkageKind GetCursorLinkage(Cursor cursor);

		/// <summary>
		///     Retrieve the physical location of the source constructor referenced by the given cursor.
		///     The location of a declaration is typically the location of the name of that declaration, where
		///     the name of that declaration would occur if it is unnamed, or some keyword that introduces that
		///     particular declaration. The location of a reference is where that reference occurs within the
		///     source code.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetCursorLocation(Cursor cursor);

		/// <summary>
		///     Determine the availability of the entity that this cursor refers to on any platforms for
		///     which availability information is known. The cursor to query.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorPlatformAvailability",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern int GetCursorPlatformAvailability(Cursor cursor, out int alwaysDeprecated,
			out String deprecatedMessage, out bool alwaysUnavailable, out String unavailableMessage,
			out IntPtr availability, int availabilitySize);

		/// <summary>
		///     For a cursor that is a reference, retrieve a cursor representing the entity that it
		///     references.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorReferenced", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetCursorReferenced(Cursor cursor);

		/// <summary>
		///     Given a cursor that references something else, return the source range covering that
		///     reference.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorReferenceNameRange",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange GetCursorReferenceNameRange(Cursor cursor, NameRefFlags nameFlags,
			uint pieceIndex);

		/// <summary>
		///     Retrieve the return type associated with a given cursor. This only returns a valid type if
		///     the cursor refers to a function or method.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorResultType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetCursorResultType(Cursor cursor);

		/// <summary>Determine the semantic parent of the given cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorSemanticParent", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetCursorSemanticParent(Cursor cursor);

		/// <summary>Retrieve a name for the entity referenced by this cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorSpelling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCursorSpelling(Cursor cursor);

		/// <summary>
		///     Determine the "thread-local storage (TLS) kind" of the declaration referred to by a
		///     cursor.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorTLSKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern TLSKind GetCursorTLSKind(Cursor cursor);

		/// <summary>Retrieve the type of a Cursor (if any).</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetCursorType(Cursor cursor);

		/// <summary>
		///     Retrieve a Unified Symbol Resolution (USR) for the entity referenced by the given cursor.
		///     A Unified Symbol Resolution (USR) is a string that identifies a particular entity (function,
		///     class, variable, etc.) within a program. USRs can be compared across translation units to
		///     determine, e.g., when references in one translation refer to an entity defined in another
		///     translation unit.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorUSR", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetCursorUSR(Cursor cursor);

		/// <summary>
		///     Describe the visibility of the entity referred to by a cursor. This returns the default
		///     visibility if not explicitly specified by a visibility attribute. The default visibility may be
		///     changed by commandline arguments. The cursor to query.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCursorVisibility", CallingConvention = CallingConvention.Cdecl)]
		public static extern VisibilityKind GetCursorVisibility(Cursor cursor);

		/// <summary>Return the memory usage of a translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCXTUResourceUsage", CallingConvention = CallingConvention.Cdecl)]
		public static extern TUResourceUsage GetCXTUResourceUsage(TranslationUnit unit);

		/// <summary>
		///     Returns the access control level for the referenced object. If the cursor refers to a C++
		///     declaration, its access control level within its parent scope is returned. Otherwise, if the
		///     cursor refers to a base specifier or access specifier, the specifier itself is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getCXXAccessSpecifier", CallingConvention = CallingConvention.Cdecl)]
		public static extern CXXAccessSpecifier GetCXXAccessSpecifier(Cursor cursor);

		/// <summary>Returns the Objective-C type encoding for the specified declaration.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDeclObjCTypeEncoding", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDeclObjCTypeEncoding(Cursor cursor);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDefinitionSpellingAndExtent",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetDefinitionSpellingAndExtent(Cursor cursor, out IntPtr /* const char ** */ startBuf,
			out IntPtr /* const char ** */ endBuf, out uint startLine, out uint startColumn, out uint endLine,
			out uint endColumn);

		/// <summary>
		///     Retrieve a diagnostic associated with the given translation unit. the translation unit to
		///     query.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnostic", CallingConvention = CallingConvention.Cdecl)]
		public static extern Diagnostic GetDiagnostic(TranslationUnit unit, uint index);

		/// <summary>Retrieve the category number for this diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticCategory", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetDiagnosticCategory(Diagnostic diagnostic);

		/// <summary>Retrieve the name of a particular diagnostic category.</summary>
		[Obsolete("Use Clang.GetDiagnosticCategoryText instead."),
		 DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticCategoryName",
			 CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDiagnosticCategoryName(uint category);

		/// <summary>
		///     Retrieve the diagnostic category text for a given diagnostic. The text of the given
		///     diagnostic category.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticCategoryText",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDiagnosticCategoryText(Diagnostic diagnostic);

		/// <summary>
		///     Retrieve the replacement information for a given fix-it. Fix-its are described in terms of
		///     a source range whose contents should be replaced by a string.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticFixIt", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDiagnosticFixIt(Diagnostic diagnostic, uint fixIt,
			out SourceRange replacementRange);

		/// <summary>
		///     Retrieve a diagnostic associated with the given DiagnosticSet. the DiagnosticSet to query.
		///     the zero-based diagnostic number to retrieve. the requested diagnostic.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticInSet", CallingConvention = CallingConvention.Cdecl)]
		public static extern Diagnostic GetDiagnosticInSet(DiagnosticSet set, uint index);

		/// <summary>
		///     Retrieve the source location of the given diagnostic. This location is where Clang would
		///     print the caret ('^') when displaying the diagnostic on the command line.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetDiagnosticLocation(Diagnostic diagnostic);

		/// <summary>Determine the number of fix-it hints associated with the given diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticNumFixIts", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetDiagnosticNumFixIts(Diagnostic diagnostic);

		/// <summary>Determine the number of source ranges associated with the given diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticNumRanges", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetDiagnosticNumRanges(Diagnostic diagnostic);

		/// <summary>Retrieve the name of the command-line option that enabled this diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticOption", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDiagnosticOption(Diagnostic diagnostic, ref String disable);

		/// <summary>Retrieve the name of the command-line option that enabled this diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticOption", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDiagnosticOption(Diagnostic diagnostic, IntPtr disable);

		/// <summary>
		///     Retrieve a source range associated with the diagnostic. A diagnostic's source ranges
		///     highlight important elements in the source code. On the command line, Clang displays source
		///     ranges by underlining them with '~' characters. the diagnostic whose range is being extracted.
		///     the zero-based index specifying which range to the requested source range.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticRange", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange GetDiagnosticRange(Diagnostic diagnostic, uint range);

		/// <summary>Retrieve the complete set of diagnostics associated with a translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticSetFromTU", CallingConvention = CallingConvention.Cdecl)]
		public static extern DiagnosticSet GetDiagnosticSetFromTU(TranslationUnit unit);

		/// <summary>Determine the severity of the given diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticSeverity", CallingConvention = CallingConvention.Cdecl)]
		public static extern DiagnosticSeverity GetDiagnosticSeverity(Diagnostic diagnostic);

		/// <summary>Retrieve the text of the given diagnostic.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getDiagnosticSpelling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetDiagnosticSpelling(Diagnostic diagnostic);

		/// <summary>
		///     Return the element type of an array, complex, or vector type. If a type is passed in that
		///     is not an array, complex, or vector type, an invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getElementType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetElementType(Type type);

		/// <summary>
		///     Retrieve the integer value of an enum constant declaration as a <see cref="ulong" />.
		///     <para>
		///         If the cursor does not reference an enum constant declaration,
		///         <see cref="ulong.MaxValue" /> is returned. Since this is also potentially a valid constant
		///         value, the kind of the cursor must be verified before calling this function.
		///     </para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getEnumConstantDeclUnsignedValue",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong GetEnumConstantDeclUnsignedValue(Cursor cursor);

		/// <summary>
		///     Retrieve the integer value of an enum constant declaration as a <c>long</c>.
		///     <para>
		///         If the cursor does not reference an enum constant declaration,
		///         <see cref="long.MinValue" /> is returned. Since this is also potentially a valid constant
		///         value, the kind of the cursor must be verified before calling this function.
		///     </para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getEnumConstantDeclValue", CallingConvention = CallingConvention.Cdecl)]
		public static extern long GetEnumConstantDeclValue(Cursor cursor);

		/// <summary>
		///     Retrieve the integer type of an enum declaration. If the cursor does not reference an enum
		///     declaration, an invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getEnumDeclIntegerType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetEnumDeclIntegerType(Cursor cursor);

		/// <summary>
		///     Retrieve the exception specification type associated with a function type. If a
		///     non-function type is passed in, an error code of -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getExceptionSpecificationType",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CursorExceptionKind GetExceptionSpecificationType(Type type);

		/// <summary>Retrieve the file, line, column, and offset represented by the given source location.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getExpansionLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetExpansionLocation(SourceLocation location, out File file, out uint line,
			out uint column, out uint offset);

		/// <summary>
		///     Retrieve the bit width of a bit field declaration as an integer. If a cursor that is not a
		///     bit field declaration is passed in, -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFieldDeclBitWidth", CallingConvention = CallingConvention.Cdecl)]
		public static extern int GetFieldDeclBitWidth(Cursor cursor);

		/// <summary>
		///     Retrieve a file handle within the given translation unit. the translation unit the name of
		///     the file. the file handle for the named file in the translation unit tu, or a NULL file handle
		///     if the file was not a part of this translation unit.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern File GetFile(TranslationUnit tu,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string fileName);

		/// <summary>
		///     Retrieve the buffer associated with the given file. the translation unit the file for
		///     which to retrieve the buffer. [out] if non-NULL, will be set to the size of the buffer. a
		///     pointer to the buffer in memory that holds the contents of file, or a NULL pointer when the
		///     file is not loaded.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFileContents", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public static extern IntPtr GetFileContents(TranslationUnit tu, File file, out UIntPtr /* size_t * */ size);

		/// <summary>Retrieve the file, line, column, and offset represented by the given source location.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFileLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetFileLocation(SourceLocation location, out File file, out uint line,
			out uint column, out uint offset);

		/// <summary>Retrieve the complete file and path name of the given file.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFileName", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetFileName(File sFile);

		/// <summary>Retrieve the last modification time of the given file.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFileTime", CallingConvention = CallingConvention.Cdecl)]
		public static extern long GetFileTimeLong(File file);

		/// <summary>
		///     Retrieve the unique ID for the given file. the file to get the ID for. stores the returned
		///     FileUniqueID. If there was a failure getting the unique ID, returns non-zero, otherwise returns
		///     0.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFileUniqueID", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool GetFileUniqueID(File file, out FileUniqueID id);

		/// <summary>
		///     Retrieve the calling convention associated with a function type. If a non-function type is
		///     passed in, <see cref="CallingConv.Invalid" /> is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getFunctionTypeCallingConv",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CallingConv GetFunctionTypeCallingConv(Type type);

		/// <summary>
		///     For cursors representing an IB outlet collection attribute, this function returns the
		///     collection element type.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getIBOutletCollectionType",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetIBOutletCollectionType(Cursor cursor);

		/// <summary>Retrieve the file that is included by the given inclusion directive cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getIncludedFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern File GetIncludedFile(Cursor cursor);


		/// <summary>
		///     Visit the set of preprocessor inclusions in a translation unit. The visitor function is
		///     called with the provided data for every included file.  This does not include headers included
		///     by the PCH file (unless one is inspecting the inclusions in the PCH file itself).
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getInclusions", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetInclusions(TranslationUnit tu, InclusionVisitor visitor, ClientData clientData);

		/// <summary>
		///     Legacy API to retrieve the file, line, column, and offset represented by the given source
		///     location.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getInstantiationLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetInstantiationLocation(SourceLocation location, out File file, out uint line,
			out uint column, out uint offset);

		/// <summary>
		///     Retrieves the source location associated with a given file/line/column in a particular
		///     translation unit.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetLocation(TranslationUnit tu, File file, uint line, uint column);

		/// <summary>
		///     Retrieves the source location associated with a given character offset in a particular
		///     translation unit.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getLocationForOffset", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetLocationForOffset(TranslationUnit unit, File file, uint offset);

		/// <summary>Given a File header file, return the module that contains it, if one exists.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getModuleForFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern Module GetModuleForFile(TranslationUnit unit, File file);

		/// <summary>Retrieve the NULL cursor, which represents no entity.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNullCursor", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetNullCursor();

		/// <summary>Retrieve a NULL (invalid) source location.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNullLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetNullLocation();

		/// <summary>Retrieve a NULL (invalid) source range.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNullRange", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange GetNullRange();

		/// <summary>
		///     Retrieve the number of non-variadic parameters associated with a function type. If a
		///     non-function type is passed in, -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNumArgTypes", CallingConvention = CallingConvention.Cdecl)]
		public static extern int GetNumArgTypes(Type type);

		/// <summary>Retrieve the number of chunks in the given code-completion string.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNumCompletionChunks", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetNumCompletionChunks(CompletionString completionString);

		/// <summary>Determine the number of diagnostics produced for the given translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNumDiagnostics", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetNumDiagnostics(TranslationUnit unit);

		/// <summary>Determine the number of diagnostics in a DiagnosticSet.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNumDiagnosticsInSet", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetNumDiagnosticsInSet(DiagnosticSet set);

		/// <summary>
		///     Return the number of elements of an array or vector type. If a type is passed in that is
		///     not an array or vector type, -1 is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNumElements", CallingConvention = CallingConvention.Cdecl)]
		public static extern long GetNumElements(Type type);

		/// <summary>
		///     Determine the number of overloaded declarations referenced by a Cursor_OverloadedDeclRef
		///     cursor. The cursor whose overloaded declarations are being queried. The number of overloaded
		///     declarations referenced by cursor. If it is not a Cursor_OverloadedDeclRef cursor, returns 0.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getNumOverloadedDecls", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint GetNumOverloadedDecls(Cursor cursor);

		/// <summary>
		///     Retrieve a cursor for one of the overloaded declarations referenced by a
		///     Cursor_OverloadedDeclRef cursor.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getOverloadedDecl", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetOverloadedDecl(Cursor cursor, uint index);

		/// <summary>Determine the set of methods that are overridden by the given method.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getOverriddenCursors", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetOverriddenCursors(Cursor cursor, out IntPtr /* Cursor ** */ overridden,
			out uint numOverridden);

		/// <summary>For pointer types, returns the type of the pointee.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getPointeeType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetPointeeType(Type type);

		/// <summary>
		///     Retrieve the file, line and column represented by the given source location, as specified
		///     in a # line directive.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getPresumedLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetPresumedLocation(SourceLocation location, out String filename, out uint line,
			out uint column);

		/// <summary>Retrieve a source range given the beginning and ending source locations.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getRange", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange GetRange(SourceLocation begin, SourceLocation end);

		/// <summary>Retrieve a source location representing the last character within a source range.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getRangeEnd", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetRangeEnd(SourceRange range);

		/// <summary>Retrieve a source location representing the first character within a source range.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getRangeStart", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetRangeStart(SourceRange range);

		/// <summary>
		///     Retrieve a remapping. the path that contains metadata about remappings. the requested
		///     remapping.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getRemappings", CallingConvention = CallingConvention.Cdecl)]
		public static extern Remapping GetRemappings(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string path);

		/// <summary>
		///     Retrieve a remapping. pointer to an array of file paths containing remapping info. number
		///     of file paths. the requested remapping.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getRemappingsFromFileList",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Remapping GetRemappingsFromFileList(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]
			string[] filePaths, uint numFiles);

		/// <summary>
		///     Retrieve the return type associated with a function type. If a non-function type is passed
		///     in, an invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getResultType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetResultType(Type type);

		/// <summary>
		///     Retrieve all ranges that were skipped by the preprocessor. The preprocessor will skip
		///     lines when they are surrounded by an if/ifdef/ifndef directive whose condition does not
		///     evaluate to true.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getSkippedRanges", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRangeList GetSkippedRanges(TranslationUnit tu, File file);

		/// <summary>
		///     Given a cursor that may represent a specialization or instantiation of a template,
		///     retrieve the cursor that represents the template that it specializes or from which it was
		///     instantiated.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getSpecializedCursorTemplate",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetSpecializedCursorTemplate(Cursor cursor);


		/// <summary>Retrieve the file, line, column, and offset represented by the given source location.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getSpellingLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetSpellingLocation(SourceLocation location, out File file, out uint line,
			out uint column, out uint offset);

		/// <summary>
		///     Given a cursor that represents a template, determine the cursor kind of the
		///     specializations would be generated by instantiating the template.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTemplateCursorKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern CursorKind GetTemplateCursorKind(Cursor cursor);

		/// <summary>Retrieve a source range that covers the given token.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTokenExtent", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceRange GetTokenExtent(TranslationUnit unit, Token token);

		/// <summary>Determine the kind of the given token.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTokenKind", CallingConvention = CallingConvention.Cdecl)]
		public static extern TokenKind GetTokenKind(Token token);

		/// <summary>Retrieve the source location of the given token.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTokenLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation GetTokenLocation(TranslationUnit unit, Token token);

		/// <summary>
		///     Determine the spelling of the given token. The spelling of a token is the textual
		///     representation of that token, e.g., the text of an identifier or keyword.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTokenSpelling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetTokenSpelling(TranslationUnit unit, Token token);


		/// <summary>
		///     Retrieve the cursor that represents the given translation unit. The translation unit
		///     cursor can be used to start traversing the various declarations within the given translation
		///     unit.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTranslationUnitCursor", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetTranslationUnitCursor(TranslationUnit unit);

		/// <summary>Get the original translation unit source file name.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTranslationUnitSpelling",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetTranslationUnitSpelling(TranslationUnit unit);

		/// <summary>
		///     Get target information for this translation unit.
		///     <para>
		///         The <see cref="TargetInfo" /> object cannot outlive the <see cref="TranslationUnit" />.
		///         object.
		///     </para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTranslationUnitTargetInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern TargetInfo GetTranslationUnitTargetInfo(TranslationUnit unit);

		/// <summary>
		///     Returns the human-readable null-terminated C string that represents the name of the memory
		///     category.  This string should never be freed.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTUResourceUsageName", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
		public static extern string GetTUResourceUsageName(TUResourceUsageKind kind);

		/// <summary>Return the cursor for the declaration of the given type.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTypeDeclaration", CallingConvention = CallingConvention.Cdecl)]
		public static extern Cursor GetTypeDeclaration(Type type);

		/// <summary>
		///     Retrieve the underlying type of a typedef declaration. If the cursor does not reference a
		///     typedef declaration, an invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTypedefDeclUnderlyingType",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Type GetTypedefDeclUnderlyingType(Cursor cursor);

		/// <summary>Returns the typedef name of the given type.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTypedefName", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetTypedefName(Type type);

		/// <summary>Retrieve the spelling of a given TypeKind.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTypeKindSpelling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetTypeKindSpelling(TypeKind kind);

		/// <summary>
		///     Pretty-print the underlying type using the rules of the language of the translation unit
		///     from which it came. If the type is invalid, an empty string is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_getTypeSpelling", CallingConvention = CallingConvention.Cdecl)]
		public static extern String GetTypeSpelling(Type type);

		/// <summary>Compute a hash value for the given cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_hashCursor", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint HashCursor(Cursor cursor);

		/// <summary>Determines if a comment HTML start tag that is self-closing.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_HTMLStartTagComment_isSelfClosing",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool HTMLStartTagCommentIsSelfClosing(Comment comment);

		/// <summary>
		///     a Comment_HTMLStartTag AST node. attribute index (zero-based). name of the specified
		///     attribute.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_HTMLStartTag_getAttrName", CallingConvention = CallingConvention.Cdecl)]
		public static extern String HTMLStartTagGetAttrName(Comment comment, uint attrIdx);

		/// <summary>
		///     a Comment_HTMLStartTag AST node. attribute index (zero-based). value of the specified
		///     attribute.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_HTMLStartTag_getAttrValue",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String HTMLStartTagGetAttrValue(Comment comment, uint attrIdx);

		/// <summary>
		///     a Comment_HTMLStartTag AST node. number of attributes (name-value pairs) attached to the
		///     start tag.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_HTMLStartTag_getNumAttrs", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint HTMLStartTagGetNumAttrs(Comment comment);

		/// <summary>Convert an HTML tag AST node to string.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_HTMLTagComment_getAsString",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String HTMLTagCommentGetAsString(Comment comment);

		/// <summary>a Comment_HTMLStartTag or Comment_HTMLEndTag AST node. HTML tag name.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_HTMLTagComment_getTagName",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String HTMLTagCommentGetTagName(Comment comment);

		/// <summary>
		///     An indexing action/session, to be applied to one or multiple translation units. The index
		///     object with which the index action will be associated.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_IndexAction_create", CallingConvention = CallingConvention.Cdecl)]
		public static extern IndexAction IndexActionCreate(Index cIdx);

		/// <summary>
		///     Destroy the given index action. The index action must not be destroyed until all of the
		///     translation units created within that index action have been destroyed.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_IndexAction_dispose", CallingConvention = CallingConvention.Cdecl)]
		public static extern void IndexActionDispose(IndexAction indexAction);

		/// <summary>For retrieving a custom IdxClientContainer attached to a container.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getClientContainer", CallingConvention = CallingConvention.Cdecl)]
		public static extern IdxClientContainer IndexGetClientContainer([MarshalAs(UnmanagedType.LPArray)]
			IdxContainerInfo[] info);

		/// <summary>For retrieving a custom IdxClientEntity attached to an entity.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getClientEntity", CallingConvention = CallingConvention.Cdecl)]
		public static extern IdxClientEntity IndexGetClientEntity([MarshalAs(UnmanagedType.LPArray)]
			IdxEntityInfo[] info);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getCXXClassDeclInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxCXXClassDeclInfo * */
			IndexGetCXXClassDeclInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxDeclInfo[] info);

		/// <summary>Gets the general options associated with a index.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXIndex_getGlobalOptions", CallingConvention = CallingConvention.Cdecl)]
		public static extern GlobalOptFlags IndexGetGlobalOptions(Index index);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getIBOutletCollectionAttrInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxIBOutletCollectionAttrInfo * */
			IndexGetIBOutletCollectionAttrInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxAttrInfo[] info);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getObjCCategoryDeclInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxObjCCategoryDeclInfo * */
			IndexGetObjCCategoryDeclInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxDeclInfo[] info);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getObjCContainerDeclInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxObjCContainerDeclInfo * */
			IndexGetObjCContainerDeclInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxDeclInfo[] info);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getObjCInterfaceDeclInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxObjCInterfaceDeclInfo * */
			IndexGetObjCInterfaceDeclInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxDeclInfo[] info);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getObjCPropertyDeclInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxObjCPropertyDeclInfo * */
			IndexGetObjCPropertyDeclInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxDeclInfo[] info);

		/// <summary></summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_getObjCProtocolRefListInfo",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr /* const IdxObjCProtocolRefListInfo * */
			IndexGetObjCProtocolRefListInfo([MarshalAs(UnmanagedType.LPArray)]
				IdxDeclInfo[] info);

		[DllImport(LIBRARY, EntryPoint = "clang_index_isEntityObjCContainerKind",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IndexIsEntityObjCContainerKind(IdxEntityKind entityKind);

		/// <summary>Retrieve the SourceLocation represented by the given IdxLoc.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_indexLoc_getCXSourceLocation",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern SourceLocation IndexLocGetCXSourceLocation(IdxLoc loc);

		/// <summary>
		///     Retrieve the IdxFile, file, line, column, and offset represented by the given IdxLoc. If
		///     the location refers into a macro expansion, retrieves the location of the macro expansion and
		///     if it refers into a macro argument retrieves the location of the argument.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_indexLoc_getFileLocation", CallingConvention = CallingConvention.Cdecl)]
		public static extern void IndexLocGetFileLocation(IdxLoc loc, out IdxClientFile indexFile, out File file,
			out uint line, out uint column, out uint offset);

		/// <summary>For setting a custom IdxClientContainer attached to a container.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_setClientContainer", CallingConvention = CallingConvention.Cdecl)]
		public static extern void IndexSetClientContainer([MarshalAs(UnmanagedType.LPArray)]
			IdxContainerInfo[] info, IdxClientContainer container);

		/// <summary>For setting a custom IdxClientEntity attached to an entity.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_index_setClientEntity", CallingConvention = CallingConvention.Cdecl)]
		public static extern void IndexSetClientEntity([MarshalAs(UnmanagedType.LPArray)]
			IdxEntityInfo[] info, IdxClientEntity entity);

		/// <summary>Sets general options associated with a Index.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXIndex_setGlobalOptions", CallingConvention = CallingConvention.Cdecl)]
		public static extern void IndexSetGlobalOptions(Index index, GlobalOptFlags options);

		/// <summary>
		///     Sets the invocation emission path option. The invocation emission path specifies a path
		///     which will contain log files for certain LibClang invocations.
		///     <para>A <c>null</c> value (default) implies that LibClang invocations are not logged.</para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXIndex_setInvocationEmissionPathOption",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void IndexSetInvocationEmissionPathOption(Index index,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string path);

		/// <summary>
		///     Index the given source file and the translation unit corresponding to that file via
		///     callbacks implemented through #IndexerCallbacks.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_indexSourceFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern int IndexSourceFile(IndexAction index, ClientData clientData,
			IndexerCallbacks indexCallbacks, uint indexCallbacksSize, uint indexOptions,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string sourceFilename, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]
			string[] commandLineArgs, int numCommandLineArgs, [MarshalAs(UnmanagedType.LPArray)]
			UnsavedFile[] unsavedFiles, uint numUnsavedFiles, out TranslationUnit translationUnit, uint tUOptions);

		/// <summary>
		///     Same as clang_indexSourceFile but requires a full command line for command_line_args
		///     including argv[0]. This is useful if the standard library paths are relative to the binary.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_indexSourceFileFullArgv", CallingConvention = CallingConvention.Cdecl)]
		public static extern int IndexSourceFileFullArgv(IndexAction indexAction, ClientData clientData,
			IndexerCallbacks indexCallbacks, uint indexCallbacksSize, uint indexOptions,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string sourceFilename, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]
			string[] commandLineArgs, int numCommandLineArgs, [MarshalAs(UnmanagedType.LPArray)]
			UnsavedFile[] unsavedFiles, uint numUnsavedFiles, out TranslationUnit translationUnit, uint options);

		/// <summary>Index the given translation unit via callbacks implemented through #IndexerCallbacks.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_indexTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern int IndexTranslationUnit(IndexAction indexAction, ClientData clientData,
			IndexerCallbacks indexCallbacks, uint indexCallbacksSize, uint indexOptions,
			TranslationUnit translationUnit);

		/// <summary>
		///     a Comment_InlineCommand AST node. argument index (zero-based). text of the specified
		///     argument.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_InlineCommandComment_getArgText",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String InlineCommandCommentGetArgText(Comment comment, uint argIdx);

		/// <summary>a Comment_InlineCommand AST node. name of the inline command.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_InlineCommandComment_getCommandName",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String InlineCommandCommentGetCommandName(Comment comment);

		/// <summary>a Comment_InlineCommand AST node. number of command arguments.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_InlineCommandComment_getNumArgs",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint InlineCommandCommentGetNumArgs(Comment comment);

		/// <summary>
		///     a Comment_InlineCommand AST node. the most appropriate rendering mode, chosen on command
		///     semantics in Doxygen.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_InlineCommandComment_getRenderKind",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CommentInlineCommandRenderKind InlineCommandCommentGetRenderKind(Comment comment);

		/// <summary>
		///     <c>true</c> if <see cref="Comment" /> is inline content and has a newline immediately
		///     following it in the comment text.
		///     <para>Newlines between paragraphs do not count.</para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_InlineContentComment_hasTrailingNewline",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool InlineContentCommentHasTrailingNewline(Comment comment);

		/// <summary>Determine whether the given cursor kind represents an attribute.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isAttribute", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsAttribute(CursorKind kind);

		/// <summary>
		///     Determine whether a Type has the "const" qualifier set, without looking through typedefs
		///     that may have added "const" at a different level.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isConstQualifiedType", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsConstQualifiedType(Type type);

		/// <summary>
		///     Determine whether the declaration pointed to by this cursor is also a definition of that
		///     entity.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isCursorDefinition", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsCursorDefinition(Cursor cursor);

		/// <summary>Determine whether the given cursor kind represents a declaration.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isDeclaration", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsDeclaration(CursorKind kind);

		/// <summary>Determine whether the given cursor kind represents an expression.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isExpression", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsExpression(CursorKind kind);

		/// <summary>
		///     Determine whether the given header is guarded against multiple inclusions, either with the
		///     conventional # ifndef/ # define/ # endif macro guards or with # pragma once.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isFileMultipleIncludeGuarded",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsFileMultipleIncludeGuarded(TranslationUnit unit, File file);

		/// <summary>
		///     Return <c>true</c> if the <see cref="Type" /> is a variadic function type, and
		///     <c>false</c> otherwise.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isFunctionTypeVariadic", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsFunctionTypeVariadic(Type type);

		/// <summary>Determine whether the given cursor kind represents an invalid cursor.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isInvalid", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsInvalid(CursorKind kind);

		/// <summary>
		///     Return <c>true</c> if the <see cref="Type" /> is a POD (plain old data) type, and
		///     <c>false</c> otherwise.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isPODType", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsPODType(Type type);

		/// <summary>
		///     Determine whether the given cursor represents a preprocessing element, such as a
		///     preprocessor directive or macro instantiation.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isPreprocessing", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsPreprocessing(CursorKind kind);

		/// <summary>Determine whether the given cursor kind represents a simple reference.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isReference", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsReference(CursorKind kind);

		/// <summary>
		///     Determine whether a Type has the "restrict" qualifier set, without looking through
		///     typedefs that may have added "restrict" at a different level.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isRestrictQualifiedType", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsRestrictQualifiedType(Type type);

		/// <summary>Determine whether the given cursor kind represents a statement.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isStatement", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsStatement(CursorKind kind);

		/// <summary>Determine whether the given cursor kind represents a translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsTranslationUnit(CursorKind kind);

		/// <summary>Determine whether the given cursor represents a currently unexposed piece of the AST.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isUnexposed", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsUnexposed(CursorKind kind);

		/// <summary>Returns <c>true</c> if the base class specified by the cursor is virtual.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isVirtualBase", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsVirtualBase(Cursor cursor);

		/// <summary>
		///     Determine whether a Type has the "volatile" qualifier set, without looking through
		///     typedefs that may have added "volatile" at a different level.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_isVolatileQualifiedType", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool IsVolatileQualifiedType(Type type);

		/// <summary>Deserialize a set of diagnostics from a Clang diagnostics bitcode file.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_loadDiagnostics", CallingConvention = CallingConvention.Cdecl)]
		public static extern DiagnosticSet LoadDiagnostics(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string file, out LoadDiagError error, out String errorString);

		/// <summary>
		///     Returns non-zero if the given source location is in the main file of the corresponding
		///     translation unit.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Location_isFromMainFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool LocationIsFromMainFile(SourceLocation location);

		/// <summary>Returns non-zero if the given source location is in a system header.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Location_isInSystemHeader",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool LocationIsInSystemHeader(SourceLocation location);

		/// <summary>Determine if a C++ member function or member function template is declared 'const'.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXMethod_isConst", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool MethodIsConst(Cursor cursor);

		/// <summary>Determine if a C++ method is declared '= default'.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXMethod_isDefaulted", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool MethodIsDefaulted(Cursor cursor);

		/// <summary>Determine if a C++ member function or member function template is pure virtual.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXMethod_isPureVirtual", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool MethodIsPureVirtual(Cursor cursor);

		/// <summary>Determine if a C++ member function or member function template is declared 'static'.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXMethod_isStatic", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool MethodIsStatic(Cursor cursor);

		/// <summary>
		///     Determine if a C++ member function or member function template is explicitly declared
		///     'virtual' or if it overrides a virtual method from one of the base classes.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXMethod_isVirtual", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool MethodIsVirtual(Cursor cursor);

		/// <summary>a module object. the module file where the provided module object came from.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_getASTFile", CallingConvention = CallingConvention.Cdecl)]
		public static extern File ModuleGetASTFile(Module module);

		/// <summary>a module object. the full name of the module, e.g. "std.vector".</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_getFullName", CallingConvention = CallingConvention.Cdecl)]
		public static extern String ModuleGetFullName(Module module);

		/// <summary>
		///     a module object. the name of the module, e.g. for the 'std.vector' sub-module it will
		///     return "vector".
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_getName", CallingConvention = CallingConvention.Cdecl)]
		public static extern String ModuleGetName(Module module);

		/// <summary>a module object. the number of top level headers associated with this module.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_getNumTopLevelHeaders",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ModuleGetNumTopLevelHeaders(TranslationUnit unit, Module module);

		/// <summary>
		///     a module object. the parent of a sub-module or NULL if the given module is top-level, e.g.
		///     for 'std.vector' it will return the 'std' module.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_getParent", CallingConvention = CallingConvention.Cdecl)]
		public static extern Module ModuleGetParent(Module module);

		/// <summary>
		///     a module object. top level header index (zero-based). the specified top level header
		///     associated with the module.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_getTopLevelHeader", CallingConvention = CallingConvention.Cdecl)]
		public static extern File ModuleGetTopLevelHeader(TranslationUnit unit, Module module, uint index);

		/// <summary>a module object. non-zero if the module is a system one.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Module_isSystem", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ModuleIsSystem(Module module);

		/// <summary>Create a ModuleMapDescriptor object.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ModuleMapDescriptor_create",
			CallingConvention = CallingConvention.Cdecl)]
		private static extern ModuleMapDescriptor ModuleMapDescriptorCreate(uint options);

		/// <summary>Dispose a ModuleMapDescriptor object.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ModuleMapDescriptor_dispose",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void ModuleMapDescriptorDispose(ModuleMapDescriptor descriptor);

		/// <summary>
		///     Sets the framework module name that the module.map describes. 0 for success, non-zero to
		///     indicate an error.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ModuleMapDescriptor_setFrameworkModuleName",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode ModuleMapDescriptorSetFrameworkModuleName(ModuleMapDescriptor descriptor,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string name);

		/// <summary>
		///     Sets the umbrealla header name that the module.map describes. 0 for success, non-zero to
		///     indicate an error.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ModuleMapDescriptor_setUmbrellaHeader",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode ModuleMapDescriptorSetUmbrellaHeader(ModuleMapDescriptor descriptor,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string name);

		/// <summary>
		///     Write out the ModuleMapDescriptor object to a char buffer. is reserved, always pass 0.
		///     pointer to receive the buffer pointer, which should be disposed using clang_free(). pointer to
		///     receive the buffer size. 0 for success, non-zero to indicate an error.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ModuleMapDescriptor_writeToBuffer",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode ModuleMapDescriptorWriteToBuffer(ModuleMapDescriptor descriptor, uint options,
			out IntPtr outBufferPtr, out uint outBufferSize);

		/// <summary>a Comment_ParamCommand AST node. parameter passing direction.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ParamCommandComment_getDirection",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern CommentParamPassDirection ParamCommandCommentGetDirection(Comment comment);

		/// <summary>a Comment_ParamCommand AST node. zero-based parameter index in function prototype.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ParamCommandComment_getParamIndex",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ParamCommandCommentGetParamIndex(Comment comment);

		/// <summary>a Comment_ParamCommand AST node. parameter name.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ParamCommandComment_getParamName",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String ParamCommandCommentGetParamName(Comment comment);

		/// <summary>
		///     a Comment_ParamCommand AST node. non-zero if parameter passing direction was specified
		///     explicitly in the comment.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ParamCommandComment_isDirectionExplicit",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ParamCommandCommentIsDirectionExplicit(Comment comment);

		/// <summary>
		///     a Comment_ParamCommand AST node. non-zero if the parameter that this AST node represents
		///     was found in the function prototype and clang_ParamCommandComment_getParamIndex function will
		///     return a meaningful value.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_ParamCommandComment_isParamIndexValid",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool ParamCommandCommentIsParamIndexValid(Comment comment);

		/// <summary>
		///     Same as clang_parseTranslationUnit2, but returns the TranslationUnit instead of an error
		///     code. In case of an error this routine returns a NULL TranslationUnit, without further detailed
		///     error codes.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_parseTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern TranslationUnit ParseTranslationUnit(Index cIdx,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string sourceFilename, string[] commandLineArgs, int numCommandLineArgs, [MarshalAs(UnmanagedType.LPArray)]
			UnsavedFile[] unsavedFiles, uint numUnsavedFiles, uint options);

		/// <summary>
		///     Parse the given source file and the translation unit corresponding to that file.
		///     <para>
		///         This routine is the main entry point for the Clang C API, providing the ability to parse
		///         a source file into a translation unit that can then be queried by other functions in the
		///         API. This routine accepts a set of command-line arguments so that the compilation can be
		///         configured in the same way that the compiler is configured on the command line.
		///     </para>
		///     <para>
		///         The index object with which the translation unit will be associated. The name of the
		///         source file to load, or NULL if the source file is included in command_line_args. The
		///         command-line arguments that would be passed to the clang executable if it were being
		///         invoked out-of-process. These command-line options will be parsed and will affect how the
		///         translation unit is parsed.
		///     </para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_parseTranslationUnit2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode ParseTranslationUnit2(Index cIdx,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string sourceFilename, string[] commandLineArgs, int numCommandLineArgs, [MarshalAs(UnmanagedType.LPArray)]
			UnsavedFile[] unsavedFiles, uint numUnsavedFiles, uint options, out TranslationUnit translationUnit);

		/// <summary>
		///     Same as clang_parseTranslationUnit2 but requires a full command line for command_line_args
		///     including argv[0]. This is useful if the standard library paths are relative to the binary.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_parseTranslationUnit2FullArgv",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode ParseTranslationUnit2FullArgv(Index cIdx,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string sourceFilename, string[] commandLineArgs, int numCommandLineArgs, [MarshalAs(UnmanagedType.LPArray)]
			UnsavedFile[] unsavedFiles, uint numUnsavedFiles, uint options, out TranslationUnit translationUnit);

		/// <summary>Returns non-zero if range is null.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Range_isNull", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool RangeIsNull(SourceRange range);

		/// <summary>
		///     Determine if a C++ record is abstract, i.e. whether a class or struct has a pure virtual
		///     member function.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_CXXRecord_isAbstract", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool RecordIsAbstract(Cursor cursor);

		/// <summary>Dispose the remapping.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_remap_dispose", CallingConvention = CallingConvention.Cdecl)]
		public static extern void RemapDispose(Remapping remapping);

		/// <summary>
		///     Get the original and the associated filename from the remapping. If non-NULL, will be set
		///     to the original filename. If non-NULL, will be set to the filename that the original is
		///     associated with.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_remap_getFilenames", CallingConvention = CallingConvention.Cdecl)]
		public static extern void RemapGetFilenames(Remapping mapping, uint index, out String original,
			out String transformed);

		/// <summary>Determine the number of remappings.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_remap_getNumFiles", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint RemapGetNumFiles(Remapping mapping);

		/// <summary>Reparse the source files that produced this translation unit.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_reparseTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode ReparseTranslationUnit(TranslationUnit unit, uint numUnsavedFiles,
			UnsavedFile[] unsavedFiles, ReparseFlags options);

		/// <summary>
		///     Saves a translation unit into a serialized representation of that translation unit on
		///     disk. Any translation unit that was parsed without error can be saved into a file.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_saveTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern SaveError SaveTranslationUnit(TranslationUnit unit,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string fileName, SaveTranslationUnitFlags options);

		/// <summary>
		///     Sort the code-completion results in case-insensitive alphabetical order. The set of
		///     results to sort. The number of results in Results.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_sortCodeCompletionResults",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void SortCodeCompletionResults(CompletionResult[] results, uint numResults);

		/// <summary>Suspend a translation unit in order to free memory associated with it.
		///     <para>A suspended translation unit uses significantly less memory.</para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_suspendTranslationUnit", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SuspendTranslationUnit(TranslationUnit unit);

		/// <summary>Destroy the TargetInfo object.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TargetInfo_dispose", CallingConvention = CallingConvention.Cdecl)]
		public static extern void TargetInfoDispose(TargetInfo info);

		/// <summary>Get the pointer width of the target in bits. Returns -1 in case of error.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TargetInfo_getPointerWidth",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern int TargetInfoGetPointerWidth(TargetInfo info);

		/// <summary>
		///     Get the normalized target triple as a string. Returns the empty string in case of any
		///     error.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TargetInfo_getTriple", CallingConvention = CallingConvention.Cdecl)]
		public static extern String TargetInfoGetTriple(TargetInfo info);

		/// <summary>a Comment_Text AST node. text contained in the AST node.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TextComment_getText", CallingConvention = CallingConvention.Cdecl)]
		public static extern String TextCommentGetText(Comment comment);

		/// <summary>Enable/disable crash recovery.
		///     <param name="enabled">Flag to indicate if crash recovery is enabled.</param>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_toggleCrashRecovery", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ToggleCrashRecovery(bool enabled);

		/// <summary>Tokenize the source code described by the given range into raw lexical tokens.</summary>
		/// <param name="unit">The translation unit whose text is being tokenized.</param>
		/// <param name="range">
		///     The source range in which text should be tokenized. All of the tokens produced
		///     by tokenization will fall within this source range, this pointer will be set to point to the
		///     array of tokens that occur within the given source range. The returned pointer must be freed
		///     with clang_disposeTokens() before the translation unit is destroyed. will be set to the number
		///     of tokens in the *Tokens array.
		/// </param>
		/// <param name="tokens">The tokens.</param>
		/// <param name="numTokens">The number tokens.</param>
		[DllImport(LIBRARY, EntryPoint = "clang_tokenize", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Tokenize(TranslationUnit unit, SourceRange range, out IntPtr tokens,
			out uint numTokens);


		/// <summary>
		///     a Comment_TParamCommand AST node. zero-based nesting depth of this parameter in the
		///     template parameter list
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TParamCommandComment_getDepth",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint TParamCommandCommentGetDepth(Comment comment);

		/// <summary>
		///     a Comment_TParamCommand AST node. zero-based parameter index in the template parameter
		///     list at a given nesting depth.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TParamCommandComment_getIndex",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern uint TParamCommandCommentGetIndex(Comment comment, uint depth);

		/// <summary>a Comment_TParamCommand AST node. template parameter name.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TParamCommandComment_getParamName",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String TParamCommandCommentGetParamName(Comment comment);

		/// <summary>
		///     a Comment_TParamCommand AST node. non-zero if the parameter that this AST node represents
		///     was found in the template parameter list and clang_TParamCommandComment_getDepth and
		///     clang_TParamCommandComment_getIndex functions will return a meaningful value.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_TParamCommandComment_isParamPositionValid",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool TParamCommandCommentIsParamPositionValid(Comment comment);

		/// <summary>Return the alignment of a type in bytes as per C++[expr.alignof] standard.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getAlignOf", CallingConvention = CallingConvention.Cdecl)]
		public static extern long TypeGetAlignOf(Type type);

		/// <summary>
		///     Return the class type of an member pointer type. If a non-member-pointer type is passed
		///     in, an invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getClassType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type TypeGetClassType(Type type);

		/// <summary>
		///     Retrieve the ref-qualifier kind of a function or method. The ref-qualifier is returned for
		///     C++ functions or methods. For other types or non-C++ declarations, RefQualifier_None is
		///     returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getCXXRefQualifier", CallingConvention = CallingConvention.Cdecl)]
		public static extern RefQualifierKind TypeGetCXXRefQualifier(Type type);

		/// <summary>
		///     Retrieve the type named by the qualified-id. If a non-elaborated type is passed in, an
		///     invalid type is returned.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getNamedType", CallingConvention = CallingConvention.Cdecl)]
		public static extern Type TypeGetNamedType(Type type);

		/// <summary>
		///     Returns the number of template arguments for given template specialization, or -1 if type
		///     T is not a template specialization.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getNumTemplateArguments",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern int TypeGetNumTemplateArguments(Type type);

		/// <summary>Returns the Objective-C type encoding for the specified Type.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getObjCEncoding", CallingConvention = CallingConvention.Cdecl)]
		public static extern String TypeGetObjCEncoding(Type type);

		/// <summary>Return the offset of the field with the specified name.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getOffsetOf", CallingConvention = CallingConvention.Cdecl)]
		public static extern long TypeGetOffsetOf(Type type,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string fieldName);

		/// <summary>Return the size of a type in bytes as per C++[expr.sizeof] standard.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getSizeOf", CallingConvention = CallingConvention.Cdecl)]
		public static extern long TypeGetSizeOf(Type type);

		/// <summary>
		///     Returns the type template argument of a template class specialization at given index. This
		///     function only returns template type arguments and does not handle template template arguments
		///     or variadic packs.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_getTemplateArgumentAsType",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern Type TypeGetTemplateArgumentAsType(Type type, uint index);

		/// <summary>
		///     Determine if a typedef is 'transparent' tag. A typedef is considered 'transparent' if it
		///     shares a name and spelling location with its underlying tag type, as is the case with the
		///     NS_ENUM macro. non-zero if transparent and zero otherwise.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_isTransparentTagTypedef",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern bool TypeIsTransparentTagTypedef(Type type);

		/// <summary>
		///     Visit the fields of a particular type. This function visits all the direct fields of the
		///     given cursor, invoking the given visitor function with the cursors of each visited field.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_Type_visitFields", CallingConvention = CallingConvention.Cdecl)]
		public static extern VisitorResult TypeVisitFields(Type type, FieldVisitor visitor, ClientData clientData);

		/// <summary>a Comment_VerbatimBlockLine AST node. text contained in the AST node.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VerbatimBlockLineComment_getText",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String VerbatimBlockLineCommentGetText(Comment comment);

		/// <summary>a Comment_VerbatimLine AST node. text contained in the AST node.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VerbatimLineComment_getText",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern String VerbatimLineCommentGetText(Comment comment);

		/// <summary>
		///     Map an absolute virtual file path to an absolute real one. The virtual path must be
		///     canonicalized (not contain "."/"..").
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VirtualFileOverlay_addFileMapping",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode VirtualFileOverlayAddFileMapping(VirtualFileOverlay overlay,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string virtualPath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
			string realPath);


		/// <summary>Create a VirtualFileOverlay object.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VirtualFileOverlay_create",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern VirtualFileOverlay VirtualFileOverlayCreate(uint options);

		/// <summary>Dispose a VirtualFileOverlay object.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VirtualFileOverlay_dispose",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern void VirtualFileOverlayDispose(VirtualFileOverlay overlay);

		/// <summary>
		///     Set the case sensitivity for the <see cref="VirtualFileOverlay" /> object.
		///     <para>
		///         The <see cref="VirtualFileOverlay" /> object is case-sensitive by default, this option
		///         can be used to override the default.
		///     </para>
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VirtualFileOverlay_setCaseSensitivity",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode VirtualFileOverlaySetCaseSensitivity(VirtualFileOverlay overlay,
			bool caseSensitive);

		/// <summary>Write out the VirtualFileOverlay object to a char buffer.</summary>
		[DllImport(LIBRARY, EntryPoint = "clang_VirtualFileOverlay_writeToBuffer",
			CallingConvention = CallingConvention.Cdecl)]
		public static extern ErrorCode VirtualFileOverlayWriteToBuffer(VirtualFileOverlay arg1, uint options,
			out IntPtr outBufferPtr, out uint outBufferSize);

		/// <summary>
		///     Visit the children of a particular cursor. This function visits all the direct children of
		///     the given cursor, invoking the given visitor function with the cursors of each visited child.
		/// </summary>
		[DllImport(LIBRARY, EntryPoint = "clang_visitChildren", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint VisitChildren(Cursor parent, CursorVisitor visitor, ClientData clientData);

		#endregion
	}
}