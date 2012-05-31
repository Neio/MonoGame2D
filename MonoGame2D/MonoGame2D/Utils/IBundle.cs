using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame2D.Utils {

	/// <summary>
	/// Bundle is simple way to manage multiple resources in the dictionary-like map with string key.
	/// Each collection can load content from content description file (XML based). Also content loaded with specified file can be easily unloaded.
	/// </summary>
	/// <typeparam name="T">Defines type of primary collection element</typeparam>
	public interface IBundle<T> {

		/// <summary>
		/// Loads content into collection by description.
		/// </summary>
		/// <param name="descriptionFileName">Name of the descrition file.</param>
		void LoadContent(string descriptionFileName);

		/// <summary>
		/// Unloads the content loaded previously with that description file.
		/// </summary>
		/// <param name="descriptionFileName">Name of the description file.</param>
		void UnloadContent(string descriptionFileName);

		/// <summary>
		/// Gets the collection element stored with the specified id.
		/// </summary>
		T this[string id] { get; }

		/// <summary>
		/// Determines whether this collection contains element with the specified id.
		/// </summary>
		/// <param name="id">The id of element.</param>
		/// <returns>
		/// 	<c>true</c> if collection contains element with the specified id; otherwise, <c>false</c>.
		/// </returns>
		bool Contains(string id);

		/// <summary>
		/// Adds the specified element and binds it with specified id.
		/// </summary>
		/// <param name="id">The id of element.</param>
		/// <param name="element">The element.</param>
		void Add(string id, T element);

		/// <summary>
		/// Removes from collection the element with specified id.
		/// </summary>
		/// <param name="id">The id of element.</param>
		void Remove(string id);

		/// <summary>
		/// Clears this collection. All resources will be removed.
		/// </summary>
		void Clear();

		/// <summary>
		/// Gets the enumeration of collection elements ids.
		/// </summary>
		/// <value>Elements ids.</value>
		IEnumerable<string> Ids { get; }

	}


}
