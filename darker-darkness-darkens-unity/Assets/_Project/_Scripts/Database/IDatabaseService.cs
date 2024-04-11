using LiteDB;

public interface IDatabaseService
{
    LiteDatabase GetContext();
}