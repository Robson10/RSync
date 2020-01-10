using System;

namespace RSync.Core.Extends
{
    /// <summary>
    /// Attribute class containing name of resource.
    /// </summary>
    public class Resource : Attribute
    {
        /// <summary>
        /// Resource name
        /// </summary>
        private readonly string resourceName;

        /// <summary>
        /// Resource name property
        /// </summary>
        public string ResourceName => resourceName;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="resourceName"></param>
        public Resource(string resourceName)
        {
            this.resourceName = resourceName;
        }
    }
}