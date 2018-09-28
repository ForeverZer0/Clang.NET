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
// CEnum.cs created on 2018-09-21

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace LibClang
{
	/// <summary>Describes a C-style enum.</summary>
	/// <seealso cref="LibClang.Export.CEntity" />
	/// <seealso cref="System.Collections.Generic.IEnumerable{LibClang.Export.CMember}" />
	[DataContract(Namespace = "clang", Name = "enum"), KnownType(typeof(CEnum))]
	public class CEnum : CEntity, IEnumerable<CMember>
	{
		public CEnum(string name, Type type) : base(name, PrimitiveType.Enum, "enum")
		{
			Members = new List<CMember>();
			IntegerType = new CType(type);
		}

		private CEnum(IEnumerable<CMember> members) : base(null, null)
		{
			Members = new List<CMember>(members);
		}

		#region Properties & Indexers

		public int Count => Members.Count;

		[DataMember(Name = "integer_type")]
		public CType IntegerType { get; protected set; }

		[DataMember(Name = "members")]
		public List<CMember> Members { get; protected set; }

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Members).GetEnumerator();

		#endregion

		#region IEnumerable<CMember> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<CMember> GetEnumerator() => Members.GetEnumerator();

		#endregion

		#region Methods

		public void Add(string name, long value) => Members.Add(new CMember(name, value));

		public void Add(string name, ulong value) => Members.Add(new CMember(name, value));

		#endregion
	}
}