using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LibClang
{
	public class ClangException : Exception
	{
		public ClangException(string message) : base(message)
		{
		}
	}

	public class ParseError : ClangException
	{
		public ParseError(string message) : base(message)
		{
		}
	}
}
