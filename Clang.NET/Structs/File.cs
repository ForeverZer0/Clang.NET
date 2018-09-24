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
// File.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct File : IEquatable<File>
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="File" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public File(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Retrieve the last modification time of the given file.</summary>
		/// <value>The file time.</value>
		public DateTime FileTime => Clang.GetFileTime(this);

		/// <summary>Retrieve the last modification time of the given file.</summary>
		/// <value>The file time.</value>
		public long FileTimeLong => Clang.GetFileTimeLong(this);

		/// <summary>Retrieve the unique ID for the given file.</summary>
		/// <value>The identifier.</value>
		public FileUniqueID Id
		{
			get
			{
				Clang.GetFileUniqueID(this, out var id);
				return id;
			}
		}

		/// <summary>Retrieve the complete file and path name of the given file.</summary>
		/// <value>The name.</value>
		public string Name => Clang.GetFileName(this);

		#endregion

		#region IEquatable<File> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     true if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, false.
		/// </returns>
		public bool Equals(File other) => Clang.FileIsEqual(this, other);

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
			return obj is File other && Equals(other);
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>
		///     A hash code for this instance, suitable for use in hashing algorithms and data structures
		///     like a hash table.
		/// </returns>
		public override int GetHashCode() => _pointer.GetHashCode();

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="File" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(File instance) => instance._pointer;

		public static bool operator ==(File left, File right) => left.Equals(right);

		public static bool operator !=(File left, File right) => !left.Equals(right);

		#endregion
	}
}