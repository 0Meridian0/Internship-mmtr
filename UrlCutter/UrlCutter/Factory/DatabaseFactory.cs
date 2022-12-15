namespace UrlCutter.Factory
{
    public class DatabaseFactory
    {
        public string connectionStr;
        public string databaseName;
        public DatabaseFactory(ConfigurationManager config)
        {
            databaseName = config.GetConnectionString("DatabaseType");
            connectionStr = config.GetConnectionString(databaseName);
        }

        public IDatabaseType CreateConnection()
        {
            IDatabaseType data = databaseName.ToLower() switch
            {
                "mysql" => new MySqlDatabase(connectionStr),
                "sqlserver" => new SqlServerDatabase(connectionStr),

                _ => throw new Exception($"Не удалось найти такое название бд: {databaseName}")
            };
            return data;
        }
    }
}
