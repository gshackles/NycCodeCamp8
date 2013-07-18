using System.Linq;

namespace CodeCamp.Core.Data
{
    public interface IFileManager
    {
        bool FileExists(string file);
        string ReadFile(string file);
        void WriteFile(string file, string content);
        void DeleteFile(string file);
    }
}