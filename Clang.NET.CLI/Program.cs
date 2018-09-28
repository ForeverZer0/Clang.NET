using System;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

namespace LibClang.CLI
{
	using static Console;

	class Program
	{
		static void Main(string[] args)
		{

			// NativeLibraryLoader.Load(Clang.LIBRARY);
			var version = Clang.GetClangVersion();
			WriteLine(version);

			var parser = new Parser
			{
				SourceFiles = new[] { "D:/Program Files/LLVM/include/clang-c/Index.h" },
				CommandLineArgs = new [] { "-ID:/Program Files/LLVM/include" }
			};
			var data = parser.Parse();
		
		
			var generator = new CSharpGenerator
			{
				OutputName = "STB"
			};
			generator.Generate(data);

			Console.ReadLine();
		}
	}
}
