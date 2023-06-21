namespace ToyWebService.Utils.DAL;

public class PostgresDbConfig
{
    public string Host { get; init; }
    
    public int Port { get; init; }
    
    public string Database { get; init; }
    
    public string Username { get; init; }
    
    public string Password { get; init; }

    //TODO не забудь использовать это
    public string PartialConnectionString { get; init; }
}