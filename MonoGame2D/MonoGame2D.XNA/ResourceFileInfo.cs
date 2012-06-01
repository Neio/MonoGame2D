using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Utils
{
    /// <summary>
    /// ResourceFileInfo is universal wrapper for any kind of data reading provider: filesystem, embedded resources and packages.
    /// Resource location priorities: first File System, then Package and embedded resource at last.
    /// </summary>
    public class ResourceFileInfo
    {
        private string _filename;
        private IResourceProvider _provider;
        private byte[] _content;

        /// <summary>
        /// Initializes instance of <see cref="ResourceFileInfo"/> as reference to virtual resource file.
        /// </summary>
        /// <param name="filename">The virtual filename.</param>
        public ResourceFileInfo(string filename)
        {
            if (null == filename) throw new ArgumentNullException("filename");
            if (filename == "") throw new ArgumentException("File name can't be empty.", "filename");

            //resolve path
            filename = FileSystem.GetFilename(filename);
            _filename = filename;

            //check file system first, it has biggest priority
            if (FileSystemResourceProvider.Exists(filename))
            {
                _provider = new FileSystemResourceProvider(filename);
            }
            else
            {
                //try to find file in assemblies
                //TODO: add assembly scaning				
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    //string[] names = assembly.GetManifestResourceNames();
                    ManifestResourceInfo resInfo = assembly.GetManifestResourceInfo(filename);

                    if (null != resInfo)
                    {
                        _provider = new ManifestResourceProvider(assembly, filename);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether physical resource file exists.
        /// </summary>
        /// <value><c>true</c> if file exists; otherwise, <c>false</c>.</value>
        public bool Exists
        {
            get { return null != _provider; }
        }

        /// <summary>
        /// Gets the name of the file used for creation of <see cref="ResourceFileInfo"/>.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return _filename; }
        }

        /// <summary>
        /// Opens the reading stream for resource.
        /// </summary>
        /// <returns></returns>
        public Stream OpenStream()
        {
            if (null == _provider)
            {
                //Watcher.Crush("Resource file '{0}' not found (checked locations: OS File System, Data Package, Embedded Resources)", FileName);
            }
            return _provider.OpenStream();
        }

        /// <summary>
        /// Gets the content of file as byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] GetContent()
        {
            if (null == _content)
            {
                using (Stream stream = OpenStream())
                {
                    _content = new byte[(int)stream.Length];
                    stream.Read(_content, 0, _content.Length);
                }
            }
            return _content;
        }

        /// <summary>
        /// Gets the content of file as text using automatic encoding detection.
        /// </summary>
        /// <returns>String from file</returns>
        public string GetContentAsText()
        {
            byte[] content = GetContent();
            Encoding encoding = DetectEncoding(content);
            int charCount = content.Length - encoding.GetPreamble().Length;
            return encoding.GetString(content, encoding.GetPreamble().Length, charCount);
        }

        /// <summary>
        /// Detects the encoding of string data bytes.
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <returns>Encoding detected for specified data</returns>
        private Encoding DetectEncoding(byte[] data)
        {
            foreach (EncodingInfo encoding in Encoding.GetEncodings())
            {
                byte[] preamble = encoding.GetEncoding().GetPreamble();
                if (preamble.Length > 0 && preamble.Length <= data.Length)
                {
                    int matchCounter = 0;
                    for (int n = 0; n < preamble.Length; ++n)
                    {
                        if (preamble[n] == data[n]) matchCounter++;
                    }
                    if (matchCounter == preamble.Length)
                    {
                        return encoding.GetEncoding();
                    }
                }
            }
            return Encoding.Default;
        }

        /// <summary>
        /// Gets the content of file as text using specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns>String from file</returns>
        public string GetContentAsText(System.Text.Encoding encoding)
        {
            byte[] content = GetContent();
            return encoding.GetString(content);
        }
    }
}
