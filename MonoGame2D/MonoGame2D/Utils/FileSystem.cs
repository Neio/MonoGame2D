using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MonoGame2D
{
    /// <summary>
    /// Contains global configuration of Vortex IO file system access.
    /// </summary>
    public static class FileSystem
    {

        private static string _rootDirectory = string.Empty;
        public static string RootDirectory
        {
            get
            {
                return _rootDirectory;
            }
            set
            {
                _rootDirectory = value;
            }
        }



        public static string GetFilename(string assetName)
        {
            // Replace non-Windows path separators with local path separators
            return Path.Combine(_rootDirectory, assetName.Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
