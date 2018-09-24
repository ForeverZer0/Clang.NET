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
// SourceRange.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     Identifies a half-open character range in the source code.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SourceRange : IEquatable<SourceRange>
	{
		private readonly IntPtr PtrData1;
		private readonly IntPtr PtrData2;
		private readonly uint BeginIntData;
		private readonly uint EndIntData;

		#region Properties & Indexers

		/// <summary>Retrieve a null (invalid) source range.</summary>
		/// <value>The null range.</value>
		public static SourceRange Null => Clang.GetNullRange();

		/// <summary>Retrieve a source location representing the last character within a source range.</summary>
		/// <value>The end.</value>
		public SourceLocation End => Clang.GetRangeEnd(this);

		/// <summary>Gets a value indicating whether this instance is null.</summary>
		/// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
		public bool IsNull => Clang.RangeIsNull(this);

		/// <summary>Retrieve a source location representing the first character within a source range.</summary>
		/// <value>The start.</value>
		public SourceLocation Start => Clang.GetRangeStart(this);

		#endregion

		#region IEquatable<SourceRange> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(SourceRange other) => Clang.EqualRanges(this, other);

		#endregion

		#region Methods

		/// <summary>Creates the specified <see cref="SourceRange" /> given the beginning and ending locations.</summary>
		/// <param name="start">The start location.</param>
		/// <param name="end">The end location.</param>
		/// <returns>A newly created <see cref="SourceRange" />.</returns>
		public static SourceRange Create(SourceLocation start, SourceLocation end) => Clang.GetRange(start, end);

		/// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
		///     otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is SourceRange other && Equals(other);
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
				var hashCode = PtrData1.GetHashCode();
				hashCode = (hashCode * 271) ^ PtrData2.GetHashCode();
				hashCode = (hashCode * 311) ^ (int) BeginIntData;
				hashCode = (hashCode * 313) ^ (int) EndIntData;
				return hashCode;
			}
		}

		#endregion

		#region Operators

		public static bool operator ==(SourceRange left, SourceRange right) => left.Equals(right);

		public static bool operator !=(SourceRange left, SourceRange right) => !left.Equals(right);

		#endregion
	}
}