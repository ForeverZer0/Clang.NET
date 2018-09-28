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
// CFunction.cs created on 2018-09-21

#endregion

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LibClang
{
	/// <summary>Describes a function.</summary>
	/// <seealso cref="LibClang.Export.CEntity" />
	/// <seealso cref="System.Collections.Generic.IEnumerable{LibClang.Export.CArgument}" />
	[DataContract(Namespace = "clang", Name = "function"), KnownType(typeof(CFunction))]
	public class CFunction : CEntity, IEnumerable<CArgument>
	{
		public CFunction(string name, Type returnType) : base(name, PrimitiveType.Function, "function")
		{
			Arguments = new List<CArgument>();
			ReturnType = new CType(returnType);
		}

		private CFunction(IEnumerable<CArgument> arguments) : base(null, null)
		{
			Arguments = new List<CArgument>(arguments);
			;
		}

		#region Properties & Indexers

		[DataMember(Name = "arguments")]
		public List<CArgument> Arguments { get; protected set; }

		public int Count => Arguments.Count;

		[DataMember(Name = "return")]
		public CType ReturnType { get; protected set; }

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Arguments).GetEnumerator();

		#endregion

		#region IEnumerable<CArgument> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<CArgument> GetEnumerator() => Arguments.GetEnumerator();

		#endregion

		#region Methods

		public void Add(string name, Type type) => Arguments.Add(new CArgument(name, type));

		#endregion
	}
}