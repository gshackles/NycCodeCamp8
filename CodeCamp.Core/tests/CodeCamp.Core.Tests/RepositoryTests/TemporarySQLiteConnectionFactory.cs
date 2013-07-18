using System;
using System.IO;
using Cirrious.MvvmCross.Plugins.Sqlite;
using SQLite;

namespace CodeCamp.Core.Tests.RepositoryTests
{
    public class TemporarySQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        private readonly string _dirPath;

        public TemporarySQLiteConnectionFactory()
        {
#if !__ANDROID__
            string dbFolder = Directory.GetCurrentDirectory();
#else
            string dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#endif

            _dirPath = Path.Combine(dbFolder, Guid.NewGuid().ToString());
            Directory.CreateDirectory(_dirPath);
        }

        public ISQLiteConnection Create(string address)
        {
            var filePath = Path.Combine(_dirPath, address);

            return new SQLiteConnection(filePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        }

        public void CleanUp()
        {
            Directory.Delete(_dirPath, true);
        }
    }
}
