using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibClang
{
	public class CodeWriter : StreamWriter
	{
		private int _indent;
		protected string OriginalNewline { get; }

		public bool UseTabs { get; set; } = false;

		public int TabSize { get; set; } = 4;

		public int Indent
		{
			get => _indent;
			set
			{
				_indent = Math.Max(0, value);
				NewLine = OriginalNewline + (UseTabs ? new string('\t', _indent) : new string(' ', _indent * TabSize));
			}
		}

		public CodeWriter(string filename, Encoding encoding) : 
			base(System.IO.File.Open(filename, FileMode.Create, FileAccess.Write), encoding)
		{
			OriginalNewline = NewLine;
		}

		public CodeWriter(Stream stream, Encoding encoding) : base(stream, encoding)
		{
		}
	}
}
