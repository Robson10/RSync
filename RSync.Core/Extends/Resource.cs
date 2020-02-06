using System;

namespace RSync.Core.Extends
{
    /// <summary>
    /// Attribute class containing name of resource.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ResourceAttribute : Attribute
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
        public ResourceAttribute(string resourceName)
        {
            this.resourceName = resourceName;
        }
    }
}