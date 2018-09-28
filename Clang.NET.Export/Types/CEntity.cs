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
// CEntity.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Base class for all basic C-types that get parsed.</summary>
	/// <seealso cref="System.IEquatable{LibClang.Export.CEntity}" />
	/// <seealso cref="System.IComparable{LibClang.Export.CEntity}" />
	/// <seealso cref="System.IComparable" />
	[DataContract(Namespace = "clang", Name = "entity"), KnownType(typeof(CEntity)), KnownType(typeof(PrimitiveType))]
	public abstract class CEntity : IEquatable<CEntity>, IComparable<CEntity>, IComparable
	{
		/// <summary>Initializes a new instance of the <see cref="CEntity" /> class.</summary>
		/// <param name="name">The name of the entity.</param>
		/// <param name="type">The type of the entity.</param>
		protected CEntity(string name, Type type)
		{
			Name = name ?? string.Empty;
			Type = new CType(type);
		}

		/// <summary>Initializes a new instance of the <see cref="CEntity" /> class.</summary>
		/// <param name="name">The name of the entity.</param>
		/// <param name="primitive">The primitive type of the entity.</param>
		/// <param name="canonical">The canonical type of the entity.</param>
		protected CEntity(string name, PrimitiveType primitive, string canonical)
		{
			Name = name;
			Type = new CType(primitive, canonical);
		}

		/// <summary>Initializes a new instance of the <see cref="CEntity" /> class.</summary>
		/// <param name="name">The name of the entity.</param>
		/// <param name="type">The type of the entity.</param>
		protected CEntity(string name, CType type)
		{
			Name = name;
			Type = type;
		}

		#region Properties & Indexers

		/// <summary>Gets or sets a comment associated with this entity.</summary>
		/// <value>The comment.</value>
		[DataMember(Name = "comment", EmitDefaultValue = false)]
		public string Comment { get; set; }

		/// <summary>Gets or sets the name of this entity.</summary>
		/// <value>The name.</value>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>Gets the type associated with this entity.</summary>
		/// <value>The type.</value>
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public CType Type { get; protected set; }

		#endregion

		#region IComparable Implementation

		/// <summary>
		///     Compares the current instance with another object of the same type and returns an integer
		///     that indicates whether the current instance precedes, follows, or occurs in the same position
		///     in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///     A value that indicates the relative order of the objects being compared. The return value
		///     has these meanings: Value Meaning Less than zero This instance precedes
		///     <paramref name="obj">obj</paramref> in the sort order. Zero This instance occurs in the same
		///     position in the sort order as <paramref name="obj">obj</paramref>. Greater than zero This
		///     instance follows <paramref name="obj">obj</paramref> in the sort order.
		/// </returns>
		/// <exception cref="ArgumentException">CEntity</exception>
		public int CompareTo(object obj)
		{
			if (ReferenceEquals(null, obj)) return 1;
			if (ReferenceEquals(this, obj)) return 0;
			return obj is CEntity other
				? CompareTo(other)
				: throw new ArgumentException($"Object must be of type {nameof(CEntity)}");
		}

		#endregion

		#region IComparable<CEntity> Implementation

		/// <summary>
		///     Compares the current instance with another object of the same type and returns an integer
		///     that indicates whether the current instance precedes, follows, or occurs in the same position
		///     in the sort order as the other object.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>
		///     A value that indicates the relative order of the objects being compared. The return value
		///     has these meanings: Value Meaning Less than zero This instance precedes
		///     <paramref name="other">other</paramref> in the sort order. Zero This instance occurs in the
		///     same position in the sort order as <paramref name="other">other</paramref>. Greater than zero
		///     This instance follows <paramref name="other">other</paramref> in the sort order.
		/// </returns>
		public int CompareTo(CEntity other)
		{
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			var result = string.Compare(Name, other.Name, StringComparison.Ordinal);
			if (result != 0)
				return result;
			return string.CompareOrdinal(Type.Canonical, other.Type.Canonical);
		}

		#endregion

		#region IEquatable<CEntity> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(CEntity other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Equals(Type, other.Type);
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
			if (obj.GetType() != GetType()) return false;
			return Equals((CEntity) obj);
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
				// ReSharper disable NonReadonlyMemberInGetHashCode
				var hash = (Name != null ? StringComparer.InvariantCulture.GetHashCode(Name) : 0) * 809;
				// ReSharper restore NonReadonlyMemberInGetHashCode
				return hash ^ (Type != null ? Type.GetHashCode() : 0);
			}
		}

		/// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() => Name;

		#endregion

		#region Operators

		public static bool operator ==(CEntity left, CEntity right) => Equals(left, right);

		public static bool operator !=(CEntity left, CEntity right) => !Equals(left, right);

		#endregion
	}
}