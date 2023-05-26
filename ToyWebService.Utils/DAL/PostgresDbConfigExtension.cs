namespace ToyWebService.Utils.DAL;

public static class PostgresDbConfigExtension
{
    public static void Validate(this PostgresDbConfig dbConfig)
    {
        if (string.IsNullOrEmpty(dbConfig.Host))
            throw new ArgumentNullException($"{nameof(dbConfig.Host)} is null or empty");
        if (dbConfig.Port == 0)
            throw new ArgumentNullException($"{nameof(dbConfig.Port)} is 0");
        if (string.IsNullOrEmpty(dbConfig.Database))
            throw new ArgumentNullException($"{nameof(dbConfig.Database)} is null or empty");
        if (string.IsNullOrEmpty(dbConfig.Username))
            throw new ArgumentNullException($"{nameof(dbConfig.Username)} is null or empty");
        if (string.IsNullOrEmpty(dbConfig.Password))
            throw new ArgumentNullException($"{nameof(dbConfig.Host)} is null or empty");
    }
}