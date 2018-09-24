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
// CompletionString.cs created on 2018-09-21

#endregion

using System;

namespace LibClang
{
	public struct CompletionString
	{
		private readonly IntPtr _pointer;

		/// <summary>Initializes a new instance of the <see cref="CompletionString" /> struct.</summary>
		/// <param name="address">The address in memory.</param>
		public CompletionString(IntPtr address)
		{
			_pointer = address;
		}

		#region Properties & Indexers

		/// <summary>Retrieve the number of annotations associated with the given completion string.</summary>
		/// <value>The number of annotations associated with the given completion string.</value>
		public int AnnotationCount => Convert.ToInt32(Clang.GetCompletionNumAnnotations(this));

		/// <summary>Determine the availability of the entity that this code-completion string refers to.</summary>
		/// <value>The availability.</value>
		public AvailabilityKind Availability => Clang.GetCompletionAvailability(this);

		/// <summary>
		///     Retrieve the brief documentation comment attached to the declaration that corresponds to
		///     the completion string.
		/// </summary>
		/// <value>The brief comment.</value>
		public string BriefComment => Clang.GetCompletionBriefComment(this);

		/// <summary>Retrieve the number of chunks in the given code-completion string.</summary>
		/// <value>The chunk count.</value>
		public int ChunkCount => Convert.ToInt32(Clang.GetNumCompletionChunks(this));

		/// <summary>Retrieve the parent context of the completion string.</summary>
		/// <value>The parent.</value>
		public string Parent => Clang.GetCompletionParent(this, out var dummy);

		/// <summary>
		///     Determine the priority of this code completion. query.
		///     <para>
		///         The priority of this completion string. Smaller values indicate higher-priority (more
		///         likely) completions.
		///     </para>
		/// </summary>
		/// <value>The priority.</value>
		public uint Priority => Clang.GetCompletionPriority(this);

		#endregion

		#region Methods

		/// <summary>Retrieve the annotation associated with the given completion string.</summary>
		/// <param name="index">The 0-based index of the annotation of the completion string.</param>
		/// <returns>The annotation string associated with the completion at the specified index.</returns>
		public string GetAnnotation(uint index) => Clang.GetCompletionAnnotation(this, index);

		/// <summary>Retrieve the annotation associated with the given completion string.</summary>
		/// <param name="index">The 0-based index of the annotation of the completion string.</param>
		/// <returns>The annotation string associated with the completion at the specified index.</returns>
		public string GetAnnotation(int index) => Clang.GetCompletionAnnotation(this, Convert.ToUInt32(index));

		/// <summary>
		///     Retrieve the completion string associated with a particular chunk within a completion
		///     string.
		/// </summary>
		/// <param name="index">The 0-based index of the chunk in the completion string.</param>
		/// <returns>The completion string associated with the chunk at index.</returns>
		public CompletionString GetChunkCompletion(uint index) => Clang.GetCompletionChunkCompletionString(this, index);

		/// <summary>
		///     Retrieve the completion string associated with a particular chunk within a completion
		///     string.
		/// </summary>
		/// <param name="index">The 0-based index of the chunk in the completion string.</param>
		/// <returns>The completion string associated with the chunk at index.</returns>
		public CompletionString GetChunkCompletion(int index) => GetChunkCompletion(Convert.ToUInt32(index));

		/// <summary>Determine the kind of a particular chunk within a completion string.</summary>
		/// <param name="index">The 0-based index of the chunk in the completion string.</param>
		/// <returns>The kind of the specified index.</returns>
		public CompletionChunkKind GetChunkKind(uint index) => Clang.GetCompletionChunkKind(this, index);

		/// <summary>Determine the kind of a particular chunk within a completion string.</summary>
		/// <param name="index">The 0-based index of the chunk in the completion string.</param>
		/// <returns>The kind of the specified index.</returns>
		public CompletionChunkKind GetChunkKind(int index) =>
			Clang.GetCompletionChunkKind(this, Convert.ToUInt32(index));

		/// <summary>Retrieve the text associated with a particular chunk within the completion string.</summary>
		/// <param name="index">The 0-based index of the chunk in the completion string.</param>
		/// <returns>The text associated with the chunk at index.</returns>
		public string GetChunkText(uint index) => Clang.GetCompletionChunkText(this, index);

		/// <summary>Retrieve the text associated with a particular chunk within the completion string.</summary>
		/// <param name="index">The 0-based index of the chunk in the completion string.</param>
		/// <returns>The text associated with the chunk at index.</returns>
		public string GetChunkText(int index) => Clang.GetCompletionChunkText(this, Convert.ToUInt32(index));

		#endregion

		#region Operators

		/// <summary>
		///     Performs an implicit conversion from <see cref="CompletionString" /> to
		///     <see cref="IntPtr" />.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator IntPtr(CompletionString instance) => instance._pointer;

		#endregion
	}
}