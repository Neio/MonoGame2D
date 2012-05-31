using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonoGame2D.Utils
{
    /// <summary>
    /// Contains global configuration of Vortex IO file system access.
    /// </summary>
    public static class FileSystem
    {
        private static readonly Regex ALIAS_PATTERN = new Regex("{(.*?)}", RegexOptions.Compiled);
        private static readonly Dictionary<string, string> _aliases = new Dictionary<string, string>();

        /// <summary>
        /// Sets the global path alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <param name="path">The path.</param>
        public static void SetPathAlias(string alias, string path)
        {
            if (null == alias) throw new ArgumentNullException("alias");
            if (null == path) throw new ArgumentNullException("path");
            if (alias == "") throw new ArgumentException("Alias can't be empty", "alias");

            alias = alias.ToLower();
            _aliases.Remove(alias);
            _aliases.Add(alias, path);
        }

        /// <summary>
        /// Resolves the user's path into real path (replacing aliases).
        /// </summary>
        /// <param name="userPath">The user path to resolve.</param>
        /// <returns>Resolved user alias into real path</returns>
        public static string ResolvePath(string userPath)
        {
            if (null == userPath) throw new ArgumentNullException("userPath");

            //Step 3. Process all of matches as style classses
            foreach (Match match in ALIAS_PATTERN.Matches(userPath))
            {
                string aliasName = match.Groups[1].Value.ToLower();
                string replacement;
                if (_aliases.TryGetValue(aliasName, out replacement))
                {
                    userPath = userPath.Replace(match.Groups[0].Value, replacement);
                }
                else
                {
                    throw new System.IO.IOException(string.Format("Unknown path alias '{0}'", aliasName));
                }
            }

            return userPath;
        }
    }
}
