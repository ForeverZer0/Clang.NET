using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LibClang
{
	internal static class Util
	{
		public static string PointerToString(IntPtr pointer)
		{
			using (var buffer = new MemoryStream(256))
			{
				var offset = 0;
				while (true)
				{
					var b = Marshal.ReadByte(pointer, offset);
					if (b != 0)
					{
						buffer.WriteByte(b);
						offset += 1;
						continue;
					}

					break;
				}

				return Encoding.UTF8.GetString(buffer.GetBuffer(), 0, (int) buffer.Length);
			}
		}
	}

}