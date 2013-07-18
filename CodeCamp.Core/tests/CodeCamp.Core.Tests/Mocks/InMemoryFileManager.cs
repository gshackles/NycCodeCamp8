using System.Collections.Generic;
using CodeCamp.Core.Data;

namespace CodeCamp.Core.Tests.Mocks
{
    public class InMemoryFileManager : IFileManager
    {
        private IDictionary<string, string> _contentCache = new Dictionary<string,string>();

        public bool FileExists(string file)
        {
            return _contentCache.ContainsKey(file);
        }

        public string ReadFile(string file)
        {
            if (!FileExists(file))
                return null;

            return _contentCache[file];
        }

        public void WriteFile(string file, string content)
        {
            _contentCache[file] = content;
        }

        public void DeleteFile(string file)
        {
            if (FileExists(file))
                _contentCache.Remove(file);
        }
    }
}