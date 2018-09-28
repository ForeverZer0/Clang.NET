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
// CStruct.cs created on 2018-09-21

#endregion

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Describes a C-style struct.</summary>
	/// <seealso cref="LibClang.Export.CEntity" />
	/// <seealso cref="System.Collections.Generic.IEnumerable{LibClang.Export.CField}" />
	[DataContract(Namespace = "clang", Name = "struct"), KnownType(typeof(CStruct))]
	public class CStruct : CEntity, IEnumerable<CField>
	{
		/// <summary>Initializes a new instance of the <see cref="CStruct" /> class.</summary>
		/// <param name="name">The name of the struct.</param>
		public CStruct(string name) : base(name, PrimitiveType.Struct, "struct")
		{
			Fields = new List<CField>();
		}

		private CStruct(IEnumerable<CField> fields) : base(null, null)
		{
			Fields = new List<CField>(fields);
			;
		}

		#region Properties & Indexers

		/// <summary>Gets the number of fields within the struct.</summary>
		/// <value>The count.</value>
		public int Count => Fields.Count;

		/// <summary>Gets a list containing the fields of the struct.</summary>
		/// <value>The fields.</value>
		[DataMember(Name = "fields")]
		public List<CField> Fields { get; protected set; }

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Fields).GetEnumerator();

		#endregion

		#region IEnumerable<CField> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<CField> GetEnumerator() => Fields.GetEnumerator();

		#endregion

		#region Methods

		/// <summary>Adds a field to the struct.</summary>
		/// <param name="name">The name of the field.</param>
		/// <param name="type">The type associated with the field.</param>
		public void Add(string name, Type type) => Fields.Add(new CField(name, type));

		#endregion
	}
}