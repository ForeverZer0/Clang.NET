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
// ParserData.cs created on 2018-09-23

#endregion

using System.Collections;
using System.Collections.Generic;
using System.IO;
using LibClang;

namespace LibClang
{
	public class ParserData : IEnumerable<CEntity>
	{
		/// <summary>Initializes a new instance of the <see cref="ParserData" /> class.</summary>
		public ParserData()
		{
			Delegates = new CEntitySet<CFunction>();
			Enums = new CEntitySet<CEnum>();
			Functions = new CEntitySet<CFunction>();
			Structs = new CEntitySet<CStruct>();
			TypeDefs = new CEntitySet<CTypeDef>();
			Macros = new CEntitySet<CMacro>();
		}

		#region Properties & Indexers

		/// <summary>Gets the number of parsed top-level entities .</summary>
		/// <value>The count.</value>
		public int Count
		{
			get
			{
				var count = 0;
				count += Delegates.Count;
				count += Enums.Count;
				count += Functions.Count;
				count += Structs.Count;
				count += TypeDefs.Count;
				return count;
			}
		}

		/// <summary>Gets or sets the delegate types.</summary>
		/// <value>The delegates.</value>
		public CEntitySet<CFunction> Delegates { get; protected set; }

		/// <summary>Gets or sets the parsed enums.</summary>
		/// <value>The enums.</value>
		public CEntitySet<CEnum> Enums { get; protected set; }

		/// <summary>Gets or sets the parsed functions.</summary>
		/// <value>The functions.</value>
		public CEntitySet<CFunction> Functions { get; protected set; }

		/// <summary>Gets or sets the parsed macros.</summary>
		/// <value>The macros.</value>
		public CEntitySet<CMacro> Macros { get; protected set; }

		/// <summary>Gets or sets the parsed structs.</summary>
		/// <value>The structs.</value>
		public CEntitySet<CStruct> Structs { get; protected set; }

		/// <summary>Gets or sets the parsed typedefs.</summary>
		/// <value>The type defs.</value>
		public CEntitySet<CTypeDef> TypeDefs { get; protected set; }

		#endregion

		#region IEnumerable Implementation

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>
		///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate
		///     through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region IEnumerable<CEntity> Implementation

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<CEntity> GetEnumerator()
		{
			foreach (var entity in Enums)
				yield return entity;
			foreach (var entity in Structs)
				yield return entity;
			foreach (var entity in Functions)
				yield return entity;
			foreach (var entity in TypeDefs)
				yield return entity;
			foreach (var entity in Delegates)
				yield return entity;
			foreach (var entity in Macros)
				yield return entity;
		}

		#endregion

		#region Methods

		private static void ExportJson<T>(string filename, CEntitySet<T> set) where T : CEntity
		{
			if (set.Count > 0)
				Serializer.SaveJson(filename, set);
		}

		private static void ExportXml<T>(string filename, CEntitySet<T> set) where T : CEntity
		{
			if (set.Count > 0)
				Serializer.SaveXml(filename, set);
		}

		/// <summary>Clears this collection.</summary>
		public void Clear()
		{
			Delegates.Clear();
			Enums.Clear();
			Functions.Clear();
			Structs.Clear();
			TypeDefs.Clear();
		}

		/// <summary>Exports the data as JSON to the specified file.</summary>
		/// <param name="filename">The filename where the data will be written.</param>
		public void ExportJson(string filename) => Serializer.SaveJson(filename, this);

		/// <summary>Exports each data type individually as JSON to the specified directory.</summary>
		/// <param name="directory">The directory to export to.</param>
		public virtual void ExportJsonToDirectory(string directory)
		{
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			ExportJson(Path.Combine(directory, "functions.json"), Functions);
			ExportJson(Path.Combine(directory, "typedefs.json"), TypeDefs);
			ExportJson(Path.Combine(directory, "structs.json"), Structs);
			ExportJson(Path.Combine(directory, "enums.json"), Enums);
			ExportJson(Path.Combine(directory, "delegates.json"), Delegates);
			ExportJson(Path.Combine(directory, "macros.json"), Macros);
		}

		/// <summary>Exports the data as XML to the specified file.</summary>
		/// <param name="filename">The filename where the data will be written.</param>
		public void ExportXml(string filename) => Serializer.SaveXml(filename, this);

		/// <summary>Exports each data type individually as XML to the specified directory.</summary>
		/// <param name="directory">The directory to export to.</param>
		public virtual void ExportXmlToDirectory(string directory)
		{
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			ExportXml(Path.Combine(directory, "functions.xml"), Functions);
			ExportXml(Path.Combine(directory, "typedefs.xml"), TypeDefs);
			ExportXml(Path.Combine(directory, "structs.xml"), Structs);
			ExportXml(Path.Combine(directory, "enums.xml"), Enums);
			ExportXml(Path.Combine(directory, "delegates.xml"), Delegates);
			ExportXml(Path.Combine(directory, "macros.xml"), Macros);
		}

		#endregion
	}
}