using LiteDB;

namespace Euphelia.Database
{
	// TODO: use class, delete disable once.
	// ReSharper disable once ClassNeverInstantiated.Global
	public class DatabaseService : IDatabaseService
	{
		private readonly SavesConfiguration _savesConfiguration;

		public DatabaseService(SavesConfiguration savesConfiguration) => _savesConfiguration = savesConfiguration;

		public LiteDatabase GetContext() => new(_savesConfiguration.CurrentSavePath);
	}
}