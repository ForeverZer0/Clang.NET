using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LibClang
{
	public class CSharpGenerator : CodeGenerator
	{
		/// <inheritdoc />
		public override string Name => "csharp";
	
		/// <inheritdoc />
		public override void Generate(ParserData data)
		{
			var name = Path.GetFileNameWithoutExtension(OutputName);
			var baseDir = CreateProject(name);
			GenerateEnums(baseDir, name, data.Enums);
			GenerateStructs(baseDir, name, data.Structs);
		}

		private static void GenerateStructs(string baseDir, string name, CEntitySet<CStruct> set)
		{
			var path = Path.Combine(baseDir, "Structs.cs");
			using (var writer = new CodeWriter(path, Encoding.UTF8))
			{
				writer.WriteLine("using System;");
				writer.WriteLine("using System.InteropServices;\n");
				writer.WriteLine($"namespace {name}");
				writer.WriteLine("{");
				foreach (var entity in set)
					GenerateStruct(writer, entity);
				writer.WriteLine("}");
			}
		}

		private static void GenerateStruct(CodeWriter writer, CStruct entity)
		{
			var name = Regex.Replace(entity.Name, "^struct ", string.Empty).ToPascalCase();

			if (entity.Count == 0)
				GeneratePointerType(writer, entity, name);
			else
			{
				writer.WriteLine("\t[StructLayout(LayoutKind.Sequential)]");
				writer.WriteLine($"\tpublic struct {name}");
				writer.WriteLine("\t{");
				foreach (var field in entity)
					GenerateField(writer, field);
			}
			writer.WriteLine("\t}\n");
		}

		private static void GeneratePointerType(CodeWriter writer, CStruct entity, string name)
		{
			writer.WriteLine($"\tpublic struct {name}");
			writer.WriteLine("\t{");
			writer.WriteLine("\t\tprivate readonly IntPtr _pointer;\n");
			writer.WriteLine($"\t\tpublic {name}(IntPtr address)");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\t_pointer = address;");
			writer.WriteLine("\t\t}\n");
			writer.WriteLine($"\t\tpublic static implicit operator IntPtr({name} instance)");
			writer.WriteLine("\t\t{");
			writer.WriteLine("\t\t\treturn instance._pointer;");
			writer.WriteLine("\t\t}");
		}

		private static void GenerateField(CodeWriter writer, CField field)
		{
			var type = ConvertType(field.Type);
			if (field.Type.Primitive == PrimitiveType.ConstantArray)
			{
				var size = Regex.Match(field.Type.Canonical, @"\[(\d+)\]").Groups[1].Value;
				writer.WriteLine($"\t\t[MarshalAs(UnmanagedType.ByValArray, SizeConst = {size})]");
			}
			writer.WriteLine($"\t\tpublic {type} {field.Name.ToPascalCase()};");
		}

		private static void GenerateEnums(string baseDir, string name, CEntitySet<CEnum> set)
		{
			var path = Path.Combine(baseDir, "Enums.cs");
			using (var writer = new CodeWriter(path, Encoding.UTF8))
			{
				writer.WriteLine("using System;\n");
				writer.WriteLine($"namespace {name}");
				writer.WriteLine("{");
				foreach (var entity in set)
					GenerateEnum(writer, entity);
				writer.WriteLine("}");
			}
		}

		private static void GenerateEnum(CodeWriter writer, CEnum entity)
		{
			var name = Regex.Replace(entity.Name, @"^enum ", string.Empty);
			var type = entity.IntegerType.Canonical;
			// TODO: Validate "name"
			writer.WriteLine($"\tpublic enum {name.ToPascalCase()} : {type}");
			writer.WriteLine("\t{");
			var unsigned = Regex.IsMatch(entity.IntegerType.Canonical, "^unsigned ");
			foreach (var member in entity)
			{
				var value = unsigned ? member.UnsignedValue.ToString() : member.Value.ToString();
				writer.WriteLine($"\t\t{member.Name.ToPascalCase()} = {value},");
			}
			writer.WriteLine("\t}\n");
		}

		private static string CreateProject(string name)
		{
			
			if (!Directory.Exists(name))
				Directory.CreateDirectory(name);
			var projectPath = Path.Combine(name, name + ".csproj");
			using (var writer = new CodeWriter(projectPath, Encoding.UTF8))
			{
				writer.UseTabs = false;
				writer.TabSize = 2;
				writer.WriteLine("<Project Sdk=\"Microsoft.NET.Sdk\">\n");
				writer.Indent++;
				writer.WriteLine("<PropertyGroup>");
				writer.Indent++;
				writer.WriteLine("<TargetFramework>netstandard2.0</TargetFramework>");
				writer.WriteLine($"<RootNamespace>{name}</RootNamespace>");
				writer.Indent--;
				writer.WriteLine("</PropertyGroup>\n");
				writer.WriteLine("<PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == 'Release|AnyCPU'\">");
				writer.Indent--;
				writer.WriteLine("</PropertyGroup>");
				writer.Indent--;
				writer.WriteLine("</Project>");
			}

			return name;
		}


		private static string ConvertType(string type)
		{
			switch (type)
			{
				case "char":
				case "signed char":
					return "sbyte";
				case "unsigned char":
					return "byte";
				case "short":
				case "short int":
				case "signed short int":
				case "signed short":
					return "short";
				case "unsigned short":
				case "unsigned short int":
					return "ushort";
				case "int":
				case "signed":
				case "signed int":
				case "long":
				case "long int":
				case "signed long":
				case "signed long int":
					return "int";
				case "unsigned":
				case "unsigned int":
				case "unsigned long":
				case "unsigned long int":
					return "uint";
				case "long long":
				case "long long int":
				case "signed long long":
				case "signed long long int":
					return "long";
				case "unsigned long long":
				case "unsigned long long int":
					return "ulong";
				case "long double":
					return "decimal";
				default:
					return type;
			}
		}

		private static string ConvertType(CType type)
		{
			switch (type.Primitive)
			{
				case PrimitiveType.Boolean: return "bool";
				case PrimitiveType.Int8: return "sbyte";
				case PrimitiveType.Int16: return "short";
				case PrimitiveType.Int32: return "int";
				case PrimitiveType.Int64: return "long";
				case PrimitiveType.Int128: return "IntPtr"; // BigInteger
				case PrimitiveType.UInt8: return "byte";
				case PrimitiveType.UInt16: return "ushort";
				case PrimitiveType.UInt32: return "uint";
				case PrimitiveType.UInt64: return "ulong";
				case PrimitiveType.UInt128: return "IntPtr"; // BigInteger
				case PrimitiveType.Float16: return "ushort"; // User conversion of bits required
				case PrimitiveType.Float32: return "float";
				case PrimitiveType.Float64: return "double";
				case PrimitiveType.Float128: return "decimal";
				case PrimitiveType.Pointer:

					// TODO
					return "IntPtr";
				case PrimitiveType.ConstantArray:
					if (Regex.IsMatch(type.Canonical, @".*\*.*\[\d+\]"))
						return "IntPtr";
					var sub = Regex.Replace(type.Canonical, @"\[\d+\]", string.Empty);
					return string.Concat(ConvertType(sub.Trim()), "[]");
				case PrimitiveType.Macro:
				case PrimitiveType.Enum:
				case PrimitiveType.Struct:
				case PrimitiveType.Function:
				case PrimitiveType.TypeDef:
				case PrimitiveType.Invalid:
				case PrimitiveType.Void:
				case PrimitiveType.All:
				default:
					return type.Canonical;
			}
		}
	}
}
