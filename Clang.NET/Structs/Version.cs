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
// Version.cs created on 2018-09-21

#endregion

namespace LibClang
{
	/// <summary>Describes a version number of the form major.minor.subminor.</summary>
	public struct Version
	{
		/// <summary>
		///     The major version number, e.g., the '10' in '10.7.3'. A negative value indicates that
		///     there is no version number at all.
		/// </summary>
		public readonly int Major;

		/// <summary>
		///     The minor version number, e.g., the '7' in '10.7.3'. This value will be negative if no
		///     minor version number was provided, e.g., for version '10'.
		/// </summary>
		public readonly int Minor;

		/// <summary>
		///     The subminor version number, e.g., the '3' in '10.7.3'. This value will be negative if no
		///     minor or subminor version number was provided, e.g., in version '10' or '10.7'.
		/// </summary>
		public readonly int SubMinor;

		/// <summary>Initializes a new instance of the <see cref="Version" /> struct.</summary>
		/// <param name="version">A <see cref="System.Version" /> to create from.</param>
		public Version(System.Version version)
		{
			Major = version.Major;
			Minor = version.Minor;
			SubMinor = version.Build;
		}

		/// <summary>Initializes a new instance of the <see cref="Version" /> struct.</summary>
		/// <param name="major">The major part of the version.</param>
		public Version(int major) : this(major, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="Version" /> struct.</summary>
		/// <param name="major">The major part of the version.</param>
		/// <param name="minor">The minor part of the version.</param>
		public Version(int major, int minor) : this(major, minor, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="Version" /> struct.</summary>
		/// <param name="major">The major part of the version.</param>
		/// <param name="minor">The minor part of the version.</param>
		/// <param name="subMinor">The sub-minor of the version.</param>
		public Version(int major, int minor, int subMinor)
		{
			Major = major;
			Minor = minor;
			SubMinor = subMinor;
		}

		#region Methods

		/// <summary>Returns this <see cref="Version" /> represented as a <see cref="System.Version" />.</summary>
		/// <returns>The <see cref="System.Version" /> representation of this version.</returns>
		public System.Version ToSystemVersion() => new System.Version(Major, Minor, SubMinor);

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="Version" /> to
		///     <see cref="System.Version" />.
		/// </summary>
		/// <param name="version">The version.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator System.Version(Version version) => version.ToSystemVersion();

		/// <summary>
		///     Performs an explicit conversion from <see cref="System.Version" /> to
		///     <see cref="Version" />.
		/// </summary>
		/// <param name="version">The version.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator Version(System.Version version) => new Version(version);

		#endregion
	}
}