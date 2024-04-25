using LiteDB;

namespace Euphelia.Database
{
	public interface IDatabaseService
	{
		LiteDatabase GetContext();
	}
}