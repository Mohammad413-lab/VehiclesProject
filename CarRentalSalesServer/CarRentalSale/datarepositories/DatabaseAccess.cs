using Microsoft.Data.SqlClient;



public static class DatabaseAccess
{
    private static readonly string? connectionString;
    static DatabaseAccess()
    {

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        connectionString = config.GetConnectionString("MyConnection");
    }

    internal static SqlConnection GetConnection()
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is not configured.");
        }


        return new SqlConnection(connectionString);
    }
}