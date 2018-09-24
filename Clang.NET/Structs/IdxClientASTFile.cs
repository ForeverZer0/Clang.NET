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
// IdxClientASTFile.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct IdxClientASTFile
	{
		private readonly IntPtr _pointer;

		public IdxClientASTFile(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		public static IdxClientASTFile Null => new IdxClientASTFile(IntPtr.Zero);

		#endregion

		#region Operators

		public static implicit operator IntPtr(IdxClientASTFile instance) => instance._pointer;

		#endregion
	}
}