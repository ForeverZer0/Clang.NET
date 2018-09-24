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
// SourceLocation.cs created on 2018-09-21

#endregion

using System;
using System.Runtime.InteropServices;

namespace LibClang
{
	/// <summary>Identifies a specific source location within a translation unit.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SourceLocation : IEquatable<SourceLocation>
	{
		private readonly IntPtr PtrData1;
		private readonly IntPtr PtrData2;
		private readonly uint IntData;

		#region Properties & Indexers

		/// <summary>Retrieve a null (invalid) source location.</summary>
		/// <value>A null location.</value>
		public static SourceLocation Null => Clang.GetNullLocation();

		/// <summary>
		///     Returns <c>true</c> if the given source location is in the main file of the corresponding
		///     translation unit.
		/// </summary>
		/// <value><c>true</c> if this instance is from main file; otherwise, <c>false</c>.</value>
		public bool IsFromMainFile => Clang.LocationIsFromMainFile(this);

		/// <summary>Returns <c>true</c> if the given source location is in a system header.</summary>
		/// <value><c>true</c> if this instance is in a system header; otherwise, <c>false</c>.</value>
		public bool IsInSystemHeader => Clang.LocationIsInSystemHeader(this);

		#endregion

		#region IEquatable<SourceLocation> Implementation

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     <c>true</c> if the current object is equal to the <paramref name="other">other</paramref>
		///     parameter; otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(SourceLocation other) => Clang.EqualLocations(this, other);

		#endregion

		#region Methods

		/// <summary>Determines whether the specified <see cref="System.Object" />, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
		///     otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) => obj is SourceLocation other && Equals(other);

		/// <summary>Retrieve the file, line, column, and offset represented by the given source location.</summary>
		/// <param name="file">The file.</param>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <param name="offset">The offset.</param>
		public void GetExpansionLocation(out File file, out uint line, out uint column, out uint offset)
		{
			Clang.GetExpansionLocation(this, out file, out line, out column, out offset);
		}

		/// <summary>Retrieve the file, line, column, and offset represented by the given source location.</summary>
		/// <param name="file">The file.</param>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <param name="offset">The offset.</param>
		public void GetFileLocation(out File file, out uint line, out uint column, out uint offset) =>
			Clang.GetFileLocation(this, out file, out line, out column, out offset);

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
				hashCode = (hashCode * 103) ^ PtrData2.GetHashCode();
				hashCode = (hashCode * 149) ^ (int) IntData;
				return hashCode;
			}
		}


		/// <summary>
		///     Legacy API to retrieve the file, line, column, and offset represented by the given source
		///     location.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <param name="offset">The offset.</param>
		[Obsolete]
		public void GetInstantiationLocation(out File file, out uint line, out uint column, out uint offset)
		{
			Clang.GetInstantiationLocation(this, out file, out line, out column, out offset);
		}

		/// <summary>
		///     Retrieve the file, line and column represented by the given source location, as specified
		///     in a # line directive.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		public void GetPresumedLocation(out String filename, out uint line, out uint column)
		{
			Clang.GetPresumedLocation(this, out filename, out line, out column);
		}

		/// <summary>Retrieve the file, line, column, and offset represented by the given source location.</summary>
		/// <param name="file">The file.</param>
		/// <param name="line">The line.</param>
		/// <param name="column">The column.</param>
		/// <param name="offset">The offset.</param>
		public void GetSpellingLocation(out File file, out uint line, out uint column, out uint offset)
		{
			Clang.GetSpellingLocation(this, out file, out line, out column, out offset);
		}

		#endregion

		#region Operators

		public static bool operator ==(SourceLocation left, SourceLocation right) => left.Equals(right);

		public static bool operator !=(SourceLocation left, SourceLocation right) => !left.Equals(right);

		#endregion
	}
}