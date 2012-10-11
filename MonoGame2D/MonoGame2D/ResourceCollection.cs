using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame2D.Utils;
using Microsoft.Xna.Framework.Content;

namespace MonoGame2D
{

	public class ResourceCollection: IResourceCollection
	{
		ContentManager _content;

		public ResourceCollection(ContentManager content)
		{

			_content = content;
		}

		public T Get<T>(string resourceId){

			if(typeof(T) == typeof(Particle.BaseParticle)){

				//process particle
				throw new NotImplementedException();
				//return (T) new Particle.GenericParticle(1) ;

			}
			else{

				return _content.Load<T>(resourceId);
			}
		} 


		public bool TryGet<T>(string resourceId, out T resource){
			try{

				var r =  Get<T>(resourceId);
				resource = r;
				return true;
			}
			catch{

				resource = default(T);
				return false;
			}

		}
	}
//    /// <summary>
//    /// Resource collections. Collection is a map of resources. Key is ID of resource, value is instance.
//    /// </summary>
//    public class ResourceCollection : IDisposable
//    {
//        ///<summary>Global map of loaded resource collections</summary>
//        private static Dictionary<string, ResourceCollection> _loadedCollections = new Dictionary<string, ResourceCollection>();
//
//        private Dictionary<string, object> _resourceMap = new Dictionary<string, object>();
//        private List<IDisposable> _disposableList = new List<IDisposable>();
//
//        private string _baseDirectory;
//        private string _collectionId;
//        private int _refCount = 1;	//initially reference count is 1
//
//        /// <summary>
//        /// Create a new instance of the <see cref="ResourceCollection"/>. All of data is loaded using description from "content.xml"
//        /// </summary>
//        /// <param name="xmlDescriptionFileName">Name of the XML description file.</param>
//        private ResourceCollection(string xmlDescriptionFileName)
//        {
//            _collectionId = xmlDescriptionFileName;
//            _baseDirectory = System.IO.Path.GetDirectoryName(xmlDescriptionFileName);
//            if (String.IsNullOrEmpty(_baseDirectory))
//            {
//                _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
//               // Log.Info("set base directory to {0}", _baseDirectory);
//            }
//            //lets parse the file
//           // ResourceFileParser parser = new ResourceFileParser(this, Progress);
//           // parser.Parse(_baseDirectory, xmlDescriptionFileName);
//        }
//
//        #region Internal Stuff
//
//        /// <summary>
//        /// Increase reference count to this domain.
//        /// </summary>
//        private void AddRef()
//        {
//            _refCount++;
//        }
//
//        /// <summary>
//        /// Decreases reference count to this resource collection.
//        /// </summary>
//        /// <returns><c>true</c> if there are still some references to this resource collection; otherwise <c>false</c></returns>
//        public bool ReleaseRef()
//        {
//            _refCount--;
//            return _refCount != 0;
//        }
//
//        /// <summary>
//        /// Adds the disposable object into resource collection
//        /// </summary>
//        /// <param name="disposable">The disposable object.</param>
//        internal void AddDisposable(IDisposable disposable)
//        {
//            _disposableList.Add(disposable);
//        }
//
//        /// <summary>
//        /// Adds the specified sprite info.
//        /// </summary>
//        /// <param name="spriteInfo">The sprite info.</param>
//        internal void Add(string resourceId, object resourceObject)
//        {
//            _resourceMap.Add(resourceId, resourceObject);
//        }
//
//        #endregion
//
//        #region IDisposable Members
//
//        /// <summary>
//        /// Frees all of content of collection physically
//        /// </summary>
//        public void Dispose()
//        {
//            if (ReleaseRef())
//            {
//                //remove resource collection from cache map
//                _loadedCollections.Remove(_collectionId);
//                //release freeing all of resources
//                foreach (IDisposable disposable in _disposableList)
//                {
//                    disposable.Dispose();
//                }
//            }
//        }
//
//        #endregion
//
//        #region Public Interface
//
//        /// <summary>
//        /// Loads the resource collection using specified XML resource description file.
//        /// </summary>
//        /// <param name="xmlDescriptionFileName">Name of the XML description file.</param>
//        /// <returns>Loaded resource collection</returns>
//        public static ResourceCollection Load(string xmlDescriptionFileName)
//        {
//            ResourceCollection collection;
//            if (TryLoad(xmlDescriptionFileName, out collection))
//            {
//                return collection;
//            }
//            else
//            {
//                throw new ArgumentException("Unable to load resource collection '{0}'. Resource description file missing.", xmlDescriptionFileName);
//            }
//        }
//
//        /// <summary>
//        /// Tries to load the resource collection using specified XML resource description file.
//        /// </summary>
//        /// <param name="xmlDescriptionFileName">Name of the XML description file.</param>
//        /// <param name="collection">Loaded collection.</param>
//        /// <returns>
//        /// 	<c>true</c> if collection successfully loaded or retreived from cache; otherwise <c>false</c>
//        /// </returns>
//        public static bool TryLoad(string xmlDescriptionFileName, out ResourceCollection collection)
//        {
//            if (null == xmlDescriptionFileName) throw new ArgumentNullException("resourceDescriptionFileName");
//
//            if (_loadedCollections.TryGetValue(xmlDescriptionFileName, out collection))
//            {
//                collection.AddRef();
//            }
//            else
//            {
//                ResourceFileInfo resourceFile = new ResourceFileInfo(xmlDescriptionFileName);
//                if (resourceFile.Exists)
//                {
//
//                    collection = new ResourceCollection(xmlDescriptionFileName);
//                    _loadedCollections.Add(xmlDescriptionFileName, collection);
//
//                }
//                else
//                {
//                    collection = null;
//                    return false;
//                }
//            }
//            return true;
//        }
//
//        /// <summary>
//        /// Gets the resource by id. Resource should exists and be capable with type of generic argument. 
//        /// </summary>
//        /// <typeparam name="T">Type of resource to get.</typeparam>
//        /// <param name="resourceId">The resource id.</param>
//        /// <returns>Resource object</returns>
//        public T Get<T>(string resourceId)
//        {
//            T resource;
//            if (!TryGet<T>(resourceId, out resource))
//            {
//                throw new ArgumentException(String.Format("Resource collection '{0}' hasn't resource of type '{1}' and id = '{2}'", _collectionId, typeof(T), resourceId));
//            }
//            return resource;
//        }
//
//        /// <summary>
//        /// Gets the resource by id. Resource may exists and be capable with type of generic argument.
//        /// </summary>
//        /// <typeparam name="T">Type of resource to get.</typeparam>
//        /// <param name="resourceId">The resource id.</param>
//        /// <param name="resource">The resource or default value.</param>
//        /// <returns>
//        ///		<c>true</c> if resource with specified type and id exists; otherwise <c>false</c>
//        /// </returns>
//        public bool TryGet<T>(string resourceId, out T resource)
//        {
//            if (null == resourceId) throw new ArgumentNullException("resourceId");
//
//            object resourceObject;
//            if (_resourceMap.TryGetValue(resourceId, out resourceObject))
//            {
//                if (typeof(T).IsAssignableFrom(resourceObject.GetType()))
//                {
//                    resource = (T)resourceObject;
//                    return true;
//                }
//            }
//            resource = default(T);
//            return false;
//        }
//
//        #endregion
//    }
}
