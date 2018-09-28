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
// Serializer.cs created on 2018-09-21

#endregion

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace LibClang
{
	/// <summary>Provides methods for serializing/deserializing data in JSON, XML, and binary formats.</summary>
	public static class Serializer
	{
		/// <summary>Initializes the <see cref="Serializer" /> class.</summary>
		static Serializer()
		{
			JsonSettings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
			XmlSettings = new DataContractSerializerSettings();
		}

		#region Properties & Indexers

		/// <summary>Gets the settings used when serializing in JavaScript Object Notation format.</summary>
		/// <value>The settings.</value>
		public static DataContractJsonSerializerSettings JsonSettings { get; }

		/// <summary>Gets the settings used when serializing in Extensible Markup Language format.</summary>
		/// <value>The settings.</value>
		public static DataContractSerializerSettings XmlSettings { get; }

		#endregion

		#region Methods

		/// <summary>
		///     Deserializes a binary formatted stream into an object of the specified
		///     <see cref="System.Type" />.
		/// </summary>
		/// <typeparam name="T">The type to deserialize the object to.</typeparam>
		/// <param name="stream">The stream to deserialize from.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static T LoadBinary<T>(Stream stream)
		{
			try
			{
				var serializer = new BinaryFormatter();
				return (T) serializer.Deserialize(stream);
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		/// <summary>Deserializes a binary formatted file into an object.</summary>
		/// <param name="filename">The filename to deserialize from.</param>
		/// <param name="type">The <see cref="System.Type" /> of the object to deserialize.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static object LoadBinary(string filename)
		{
			try
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					return LoadBinary(stream);
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		///     Deserializes a binary formatted file into an object of the specified
		///     <see cref="System.Type" />.
		/// </summary>
		/// <typeparam name="T">The type to deserialize the object to.</typeparam>
		/// <param name="filename">The filename to deserialize from.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static T LoadBinary<T>(string filename)
		{
			try
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					return LoadBinary<T>(stream);
				}
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		/// <summary>Deserializes a binary formatted stream into an object.</summary>
		/// <param name="stream">The <see cref="Stream" /> to deserialize from.</param>
		/// <param name="type">The <see cref="System.Type" /> of the object to deserialize.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static object LoadBinary(Stream stream)
		{
			try
			{
				var serializer = new BinaryFormatter();
				return serializer.Deserialize(stream);
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>Deserializes a JSON formatted file into an object.</summary>
		/// <param name="filename">The filename to deserialize from.</param>
		/// <param name="type">The <see cref="System.Type" /> of the object to deserialize.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static object LoadJson(string filename, System.Type type)
		{
			try
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					return LoadJson(stream, type);
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		///     Deserializes a JSON formatted file into an object of the specified
		///     <see cref="System.Type" />.
		/// </summary>
		/// <typeparam name="T">The type to deserialize the object to.</typeparam>
		/// <param name="filename">The filename to deserialize from.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static T LoadJson<T>(string filename)
		{
			try
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					return (T) LoadJson(stream, typeof(T));
				}
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		/// <summary>
		///     Deserializes a JSON formatted stream into an object of the specified
		///     <see cref="System.Type" />.
		/// </summary>
		/// <typeparam name="T">The type to deserialize the object to.</typeparam>
		/// <param name="stream">The stream to deserialize from.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static T LoadJson<T>(Stream stream) => (T) LoadJson(stream, typeof(T));

		/// <summary>Deserializes a JSON formatted stream into an object.</summary>
		/// <param name="stream">The <see cref="Stream" /> to deserialize from.</param>
		/// <param name="type">The <see cref="System.Type" /> of the object to deserialize.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static object LoadJson(Stream stream, System.Type type)
		{
			try
			{
				var serializer = new DataContractJsonSerializer(type);
				return serializer.ReadObject(stream);
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>Deserializes an XML formatted file into an object.</summary>
		/// <param name="filename">The filename to deserialize from.</param>
		/// <param name="type">The <see cref="System.Type" /> of the object to deserialize.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static object LoadXml(string filename, System.Type type)
		{
			try
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					return LoadXml(stream, type);
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		///     Deserializes an XML formatted file into an object of the specified
		///     <see cref="System.Type" />.
		/// </summary>
		/// <typeparam name="T">The type to deserialize the object to.</typeparam>
		/// <param name="filename">The filename to deserialize from.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static T LoadXml<T>(string filename)
		{
			try
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					return (T) LoadXml(stream, typeof(T));
				}
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		/// <summary>
		///     Deserializes an XML formatted stream into an object of the specified
		///     <see cref="System.Type" />.
		/// </summary>
		/// <typeparam name="T">The type to deserialize the object to.</typeparam>
		/// <param name="stream">The stream to deserialize from.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static T LoadXml<T>(Stream stream) => (T) LoadXml(stream, typeof(T));

		/// <summary>Deserializes an XML formatted stream into an object.</summary>
		/// <param name="stream">The <see cref="Stream" /> to deserialize from.</param>
		/// <param name="type">The <see cref="System.Type" /> of the object to deserialize.</param>
		/// <returns>The deserialized object, or <c>default</c> for the given type if an error occured.</returns>
		public static object LoadXml(Stream stream, System.Type type)
		{
			try
			{
				var serializer = new DataContractSerializer(type, XmlSettings);
				return serializer.ReadObject(stream);
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>Serializes an object in binary format.</summary>
		/// <typeparam name="T">Any object type that can be serialized.</typeparam>
		/// <param name="filename">The filename to serialize to..</param>
		/// <param name="obj">The object to serialize.</param>
		/// <returns><c>true</c> if successful, otherwise <c>false</c> if an error occured.</returns>
		public static bool SaveBinary<T>(string filename, T obj)
		{
			try
			{
				using (var stream = System.IO.File.Open(filename, FileMode.Create, FileAccess.Write))
				{
					return SaveBinary(stream, obj);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>Serializes an object in binary format.</summary>
		/// <typeparam name="T">Any object type that can be serialized.</typeparam>
		/// <param name="stream">The stream to serialize to..</param>
		/// <param name="obj">The object to serialize.</param>
		/// <returns><c>true</c> if successful, otherwise <c>false</c> if an error occured.</returns>
		public static bool SaveBinary<T>(Stream stream, T obj)
		{
			try
			{
				var serializer = new BinaryFormatter();
				serializer.Serialize(stream, obj);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>Serializes an object in JSON format.</summary>
		/// <typeparam name="T">Any object type that can be serialized.</typeparam>
		/// <param name="filename">The filename to serialize to..</param>
		/// <param name="obj">The object to serialize.</param>
		/// <returns><c>true</c> if successful, otherwise <c>false</c> if an error occured.</returns>
		public static bool SaveJson<T>(string filename, T obj)
		{
			try
			{
				using (var stream = System.IO.File.Open(filename, FileMode.Create, FileAccess.Write))
				{
					return SaveJson(stream, obj);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>Serializes an object in JSON format.</summary>
		/// <typeparam name="T">Any object type that can be serialized.</typeparam>
		/// <param name="stream">The stream to serialize to..</param>
		/// <param name="obj">The object to serialize.</param>
		/// <returns><c>true</c> if successful, otherwise <c>false</c> if an error occured.</returns>
		public static bool SaveJson<T>(Stream stream, T obj)
		{
			try
			{
				using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true))
				{
					var serializer = new DataContractJsonSerializer(typeof(T), JsonSettings);
					serializer.WriteObject(writer, obj);
					writer.Flush();
				}

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}
		}

		/// <summary>Serializes an object in XML format.</summary>
		/// <typeparam name="T">Any object type that can be serialized.</typeparam>
		/// <param name="filename">The filename to serialize to..</param>
		/// <param name="obj">The object to serialize.</param>
		/// <returns><c>true</c> if successful, otherwise <c>false</c> if an error occured.</returns>
		public static bool SaveXml<T>(string filename, T obj)
		{
			try
			{
				using (var stream = System.IO.File.Open(filename, FileMode.Create, FileAccess.Write))
				{
					return SaveXml(stream, obj);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>Serializes an object in XML format.</summary>
		/// <typeparam name="T">Any object type that can be serialized.</typeparam>
		/// <param name="stream">The stream to serialize to..</param>
		/// <param name="obj">The object to serialize.</param>
		/// <returns><c>true</c> if successful, otherwise <c>false</c> if an error occured.</returns>
		public static bool SaveXml<T>(Stream stream, T obj)
		{
			try
			{
				var serializer = new DataContractSerializer(typeof(T), XmlSettings);
				using (var xml = new XmlTextWriter(stream, Encoding.UTF8) { Formatting = Formatting.Indented })
				{
					serializer.WriteObject(xml, obj);
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		#endregion
	}
}