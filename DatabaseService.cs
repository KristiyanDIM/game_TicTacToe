using Dapper;
using Microsoft.Data.SqlClient;

public class DatabaseService
{
    private readonly string _connectionString = "Server=sqldatabasegame.database.windows.net;Database=sqlDBgame;User ID=KRIS;Password=mMkV8KraAr46R@f;TrustServerCertificate=True;";

    public SqlConnection CreateConnection() => new SqlConnection(_connectionString);

}