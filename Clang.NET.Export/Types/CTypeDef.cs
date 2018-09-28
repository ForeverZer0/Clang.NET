﻿#region MIT License

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
// CTypeDef.cs created on 2018-09-21

#endregion

using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Describes a C-style typedef.</summary>
	/// <seealso cref="CEntity" />
	[DataContract(Namespace = "clang", Name = "typedef"), KnownType(typeof(CTypeDef))]
	public class CTypeDef : CEntity
	{
		/// <summary>Initializes a new instance of the <see cref="CTypeDef" /> class.</summary>
		/// <param name="name">The name assigned to the typedef.</param>
		/// <param name="underlyingType">The underlying type of the type definition.</param>
		public CTypeDef(string name, Type underlyingType) : base(name, PrimitiveType.TypeDef, "typedef")
		{
			UnderlyingType = new CType(underlyingType);
		}

		#region Properties & Indexers

		/// <summary>Gets the underlying type of the type definition.</summary>
		/// <value>The underlying type.</value>
		[DataMember(Name = "underlying")]
		public CType UnderlyingType { get; private set; }

		#endregion
	}
}