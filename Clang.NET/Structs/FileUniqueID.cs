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
// FileUniqueID.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>
	///     Uniquely identifies a <see cref="File" />, that refers to the same underlying file, across
	///     an indexing session.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct FileUniqueID : IEquatable<FileUniqueID>
	{
		private readonly ulong Data1;
		private readonly ulong Data2;
		private readonly ulong Data3;

		#region IEquatable<FileUniqueID> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(FileUniqueID other) => Data1 == other.Data1 && Data2 == other.Data2 && Data3 == other.Data3;

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
			return obj is FileUniqueID other && Equals(other);
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
				var hashCode = Data1.GetHashCode();
				hashCode = (hashCode * 89) ^ Data2.GetHashCode();
				hashCode = (hashCode * 331) ^ Data3.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region Operators

		public static bool operator ==(FileUniqueID left, FileUniqueID right) => left.Equals(right);

		public static bool operator !=(FileUniqueID left, FileUniqueID right) => !left.Equals(right);

		#endregion
	}
}