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
// CMember.cs created on 2018-09-21

#endregion

using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Describes a member within a C-style enum.</summary>
	/// <seealso cref="CEntity" />
	[DataContract(Namespace = "clang", Name = "member"), KnownType(typeof(CMember))]
	public class CMember : CEntity
	{
		public CMember(string name, long value) : base(name, null)
		{
			Value = value;
		}

		public CMember(string name, ulong value) : base(name, null)
		{
			Value = unchecked((long) value);
		}

		#region Properties & Indexers

		public ulong UnsignedValue => unchecked((ulong) Value);

		[DataMember(Name = "value")]
		public long Value { get; protected set; }

		#endregion
	}
}