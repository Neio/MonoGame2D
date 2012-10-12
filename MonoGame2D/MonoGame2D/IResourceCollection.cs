using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D
{
    /// <summary>
    /// Interface for Vortex resource collections. It provides methods to get resources by their ids.
    /// </summary>
    public interface IResourceCollection
    {

        /// <summary>
        /// Gets the resource by id. Resource should exists and be capable with type of generic argument. 
        /// </summary>
        /// <typeparam name="T">Type of resource to get.</typeparam>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>Resource object</returns>
        T Get<T>(string resourceId, params string[] extraResourceIds);

        /// <summary>
        /// Gets the resource by id. Resource may exists and be capable with type of generic argument or not.
        /// </summary>
        /// <typeparam name="T">Type of resource to get.</typeparam>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="resource">The resource or default value.</param>
        /// <returns>
        ///		<c>true</c> if resource with specified type and id exists; otherwise <c>false</c>
        /// </returns>
        bool TryGet<T>(string resourceId, out T resource);
    }
}
