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
// CEntitySet.cs created on 2018-09-21

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Represents a collection of unique <see cref="CEntity" /> objects.</summary>
	/// <typeparam name="T">A type that is derived from <see cref="CEntity" />.</typeparam>
	/// <seealso cref="System.Collections.Generic.HashSet{T}" />
	[CollectionDataContract(Namespace = "clang", Name = "entities", ItemName = "entity"),
	 KnownType(typeof(CEntitySet<CEntity>))]
	public class CEntitySet<T> : HashSet<T> where T : CEntity
	{
		/// <summary>Initializes a new instance of the <see cref="CEntitySet{T}" /> class.</summary>
		public CEntitySet()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="CEntitySet{T}" /> class.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		public CEntitySet(IEnumerable<T> collection) : base(collection)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="CEntitySet{T}" /> class.</summary>
		/// <param name="info">
		///     A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that
		///     contains the information required to serialize the
		///     <see cref="T:System.Collections.Generic.HashSet`1" /> object.
		/// </param>
		/// <param name="context">
		///     A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure
		///     that contains the source and destination of the serialized stream associated with the
		///     <see cref="T:System.Collections.Generic.HashSet`1" /> object.
		/// </param>
		public CEntitySet(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>
		/// Determines whether any entity with the specified name is the set.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		///   <c>true</c> if the specified name is found; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsName(string name) => this.Any(e => e.Name == name);
	}
}