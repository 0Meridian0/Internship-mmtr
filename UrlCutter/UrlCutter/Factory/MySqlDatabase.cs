using Microsoft.EntityFrameworkCore;

namespace UrlCutter.Factory
{
    public class MySqlDatabase : IDatabaseType
    {
        public string connection;
        public MySqlDatabase(string connStr)
        {
            connection = connStr;
        }

        public void UseDatabase(DbContextOptionsBuilder option)
        {
            option.UseMySQL(connection);
        }
    }
}
