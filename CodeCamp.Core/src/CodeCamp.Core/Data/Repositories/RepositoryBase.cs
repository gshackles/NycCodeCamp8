using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace CodeCamp.Core.Data.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly ISQLiteConnectionFactory _connectionFactory;

        protected RepositoryBase(ISQLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected ISQLiteConnection GetConnection()
        {
            return _connectionFactory.Create("App.db");
        }
    }
}
