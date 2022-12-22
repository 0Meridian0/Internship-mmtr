namespace UrlCutter.Factory
{
    public class DatabaseFactory
    {
        private readonly string _connectionStr;
        private readonly string _databaseName;
        public DatabaseFactory(ConfigurationManager config)
        {
            _databaseName = config.GetConnectionString("DatabaseType");
            _connectionStr = config.GetConnectionString(_databaseName);
        }

        public IDatabaseType CreateConnection()
        {
            IDatabaseType data = _databaseName.ToLower() switch
            {
                "mysql" => new MySqlDatabase(_connectionStr),
                "sqlserver" => new SqlServerDatabase(_connectionStr),

                _ => throw new Exception($"Не удалось найти такое название бд: {_databaseName}")
            };
            return data;
        }
    }
}
