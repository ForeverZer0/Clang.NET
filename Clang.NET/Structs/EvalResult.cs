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
// EvalResult.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct EvalResult : IDisposable
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="EvalResult" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public EvalResult(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>
		///     Returns <c>true</c> if the kind is <see cref="EvalResultKind.Int" /> and the evaluation
		///     result resulted in an unsigned integer.
		/// </summary>
		/// <value><c>true</c> if this instance is unsigned; otherwise, <c>false</c>.</value>
		public bool IsUnsigned => Clang.EvalResultIsUnsignedInt(this);

		/// <summary>Returns the kind of the evaluated result.</summary>
		/// <value>The kind.</value>
		public EvalResultKind Kind => Clang.EvalResultGetKind(this);

		#endregion

		#region IDisposable Implementation

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting
		///     unmanaged resources.
		/// </summary>
		public void Dispose() => Clang.EvalResultDispose(this);

		#endregion

		#region Methods

		/// <summary>
		///     Returns the evaluation result as double if the kind is <see cref="EvalResultKind.Float" />
		///     .
		/// </summary>
		/// <returns>The value as a <see cref="double" />.</returns>
		public double ToDouble() => Clang.EvalResultGetAsDouble(this);

		/// <summary>Returns the evaluation result as integer if the kind is <see cref="int" />.</summary>
		/// <returns>The value as an <see cref="int" />.</returns>
		public int ToInt32() => Clang.EvalResultGetAsInt(this);

		/// <summary>
		///     Returns the evaluation result as a <see cref="long" /> if the kind is
		///     <see cref="EvalResultKind.Int" />. This prevents overflows that may happen if the result is
		///     returned with <see cref="ToInt32" />.
		/// </summary>
		/// <returns>The value as an <see cref="long" />.</returns>
		public long ToInt64() => Clang.EvalResultGetAsLongLong(this);

		/// <summary>
		///     Returns the evaluation result as an unsigned integer if the kind is
		///     <see cref="EvalResultKind.Int" /> and <see cref="IsUnsigned" /> is <c>true</c>.
		/// </summary>
		/// <returns>The value as an <see cref="ulong" />.</returns>
		public ulong ToUint64() => Clang.EvalResultGetAsUnsigned(this);

		#endregion

		#region Operators

		/// <summary>Performs an implicit conversion from <see cref="EvalResult" /> to <see cref="IntPtr" />.</summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(EvalResult instance) => instance._pointer;

		#endregion
	}
}