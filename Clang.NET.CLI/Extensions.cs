using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LibClang
{
	public static class Extensions
	{
		// public static string ToPascalCase(this string input)
		// {
		// 	
		//
		//
		// }





		public static string Capitalize(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;
			if (input.Length == 1)
				return input.ToUpperInvariant();
			return char.ToUpperInvariant(input[0]) + input.Substring(1);
		}

		public static string ToCamelCase(this string input)
		{
			var output = ToPascalCase(input);
			return char.ToLowerInvariant(output[0]) + output.Substring(1);
		}

		public static string ToPascalCase(this string input)
		{
			var split = Regex.Split(input, @"(_)|(\s+)|([A-Z][a-z]+)")
				.Where(s => !s.Contains('_') && !string.IsNullOrWhiteSpace(s))
				.Select(s => s.ToLower().Capitalize());
			return string.Join(string.Empty, split);
		}

		public static string ToSnakeCase(this string input, bool upcase = false)
		{
			var output = Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1_$2");
			output = Regex.Replace(output, @"([a-z\d])([A-Z])", "$1_$2");
			return upcase ? output.ToUpper() : output.ToLower();
		}
	}
}
