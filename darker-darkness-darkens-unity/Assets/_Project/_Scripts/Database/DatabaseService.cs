using LiteDB;

public class DatabaseService : IDatabaseService
{
    private readonly SavesConfiguration _savesConfiguration;

    public DatabaseService(SavesConfiguration savesConfiguration)
    {
        _savesConfiguration = savesConfiguration;
    }

    public LiteDatabase GetContext()
    {
        return new LiteDatabase(_savesConfiguration.CurrentSavePath);
    }
}
