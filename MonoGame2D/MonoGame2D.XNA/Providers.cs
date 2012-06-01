using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Utils
{
    /// <summary>
    /// Defines interface for different resource file providers
    /// </summary>
    interface IResourceProvider
    {
        /// <summary>
        /// Opens the reading stream for resource.
        /// </summary>
        /// <returns></returns>
        Stream OpenStream();
    }

    /// <summary>
    /// Resource file provider for manifest resources
    /// </summary>
    class ManifestResourceProvider : IResourceProvider
    {
        private Assembly _assembly;
        private string _fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestResourceProvider"/> class.
        /// </summary>
        /// <param name="resourceInfo">The manifest resource info.</param>
        public ManifestResourceProvider(Assembly assembly, string fileName)
        {
            _assembly = assembly;
            _fileName = fileName;
        }

        #region IResourceProvider Members

        /// <summary>
        /// Opens the reading stream for manifest resource.
        /// </summary>
        /// <returns></returns>
        public System.IO.Stream OpenStream()
        {
            return _assembly.GetManifestResourceStream(_fileName);
        }

        #endregion
    }

    /// <summary>
    /// Implements IResourceProvider interface for File System
    /// </summary>
    class FileSystemResourceProvider : IResourceProvider
    {
        private FileInfo _fileInfo;

        /// <summary>
        /// Check is there are file with specified file name on file system
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public static bool Exists(string filename)
        {
            return new FileInfo(filename).Exists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemResourceProvider"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public FileSystemResourceProvider(string filename)
        {
            _fileInfo = new FileInfo(filename);

            if (!_fileInfo.Exists)
            {
                throw new FileNotFoundException("No resource file found on file system", filename);
            }
        }

        #region IResourceProvider Members

        /// <summary>
        /// Opens the reading stream for resource.
        /// </summary>
        /// <returns></returns>
        public Stream OpenStream()
        {
            return _fileInfo.OpenRead();
        }

        #endregion
    }
}
