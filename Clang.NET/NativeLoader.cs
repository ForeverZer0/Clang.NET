using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LibClang
{
	public static class NativeLoader
	{
		public static IntPtr Load(string name)
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				return LoadUnix(name, 0x002);
			string path = null;
			if (Directory.Exists("x86") && RuntimeInformation.ProcessArchitecture == Architecture.X86)
				path = Path.GetFullPath(Path.Combine("x86", name + ".dll"));
			if (Directory.Exists("x64") && RuntimeInformation.ProcessArchitecture == Architecture.X64)
				path = Path.GetFullPath(Path.Combine("x64", name + ".dll"));
			if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
				return LoadWindows(path);
			return LoadWindows(name);

		}

		[DllImport("kernel32", EntryPoint = "LoadLibrary")]
		private static extern IntPtr LoadWindows(string fileName);

		[DllImport("libdl", EntryPoint = "dlopen")]
		private static extern IntPtr LoadUnix(string fileName, int flags);
	}
}
