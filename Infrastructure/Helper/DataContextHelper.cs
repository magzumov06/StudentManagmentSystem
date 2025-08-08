namespace Infrastructure.Helper;

public class DataContextHelper
{
    private static  readonly  string connectionString ="Server=localhost;Database=StudentManagmentSystem_DB;Username=postgres;Password=12345";

    public static string GetConnectionString()
    {
        return connectionString;
    }
}