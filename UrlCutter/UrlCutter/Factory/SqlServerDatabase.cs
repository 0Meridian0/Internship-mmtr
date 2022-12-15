using Microsoft.EntityFrameworkCore;

namespace UrlCutter.Factory
{
    public class SqlServerDatabase : IDatabaseType
    {
        public string connection;
        public SqlServerDatabase(string connStr) 
        { 
            connection = connStr;
        }

        public void UseDatabase(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(connection);
        }
    }
}
