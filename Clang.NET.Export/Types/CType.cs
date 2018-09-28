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
// CType.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.Serialization;

namespace LibClang
{
	[DataContract(Namespace = "clang", Name = "type"), KnownType(typeof(CType))]
	public sealed class CType : IEquatable<CType>
	{
		public CType(Type type)
		{
			Primitive = Util.TypeToPrimitive(type);
			Canonical = Clang.GetTypeSpelling(type);
		}

		public CType(PrimitiveType primitive, string canonical)
		{
			Primitive = primitive;
			Canonical = canonical;
		}

		#region Properties & Indexers

		[DataMember(Name = "canonical")]
		public string Canonical { get; private set; }

		[DataMember(Name = "primitive")]
		public PrimitiveType Primitive { get; private set; }

		#endregion

		#region IEquatable<CType> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(CType other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Canonical, other.Canonical, StringComparison.InvariantCulture) &&
			       Primitive == other.Primitive;
		}

		#endregion

		#region Methods

		/// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
		///     otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((CType) obj);
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>
		///     A hash code for this instance, suitable for use in hashing algorithms and data structures
		///     like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return ((Canonical != null ? StringComparer.InvariantCulture.GetHashCode(Canonical) : 0) * 397) ^
				       (int) Primitive;
			}
		}

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Canonical;

		#endregion

		#region Operators

		public static bool operator ==(CType left, CType right) => Equals(left, right);

		public static bool operator !=(CType left, CType right) => !Equals(left, right);

		#endregion
	}
}