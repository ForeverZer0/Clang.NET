using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate ChildVisitResult CursorVisitor(Cursor cursor, Cursor parent, ClientData clientData);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void InclusionVisitor(IntPtr includedFile, out SourceLocation inclusionStack, uint valueIncludeLen,
		ClientData clientData);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate VisitorResult FieldVisitor(Cursor cursor, ClientData clientData);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate VisitorResult CursorRangeVisitorHandler(IntPtr context, Cursor cursor, SourceRange range);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public delegate bool AbortQueryCallback(ClientData clientData, IntPtr reserved);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void DiagnosticCallback(ClientData clientData, DiagnosticSet set, IntPtr reserved);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IdxClientFile EnterMainFileCallback(ClientData clientData, File mainFile, IntPtr reserved);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IdxClientFile IncludedFileCallback(ClientData clientData,
		[MarshalAs(UnmanagedType.LPArray)] IdxIncludedFileInfo[] info);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IdxClientASTFile ImportedASTFileCallback(ClientData clientData,
		[MarshalAs(UnmanagedType.LPArray)] IdxImportedASTFileInfo[] info);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IdxClientContainer StartedTranslationUnitCallback(ClientData clientData, IntPtr reserved);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void IndexDeclarationCallback(ClientData clientData,
		[MarshalAs(UnmanagedType.LPArray)] IdxDeclInfo[] info);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void IndexEntityReferenceCallback(ClientData clientData,
		[MarshalAs(UnmanagedType.LPArray)] IdxEntityRefInfo[] info);

}