using System.IO;
using Euphelia.Database;
using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

namespace Euphelia.Initialization
{
	public class ServiceRegistry : IPreliminarySetup
	{
		public int Order => IPreliminarySetup.REGISTER;

		public void Setup() => SetupDb();

		private static void SetupDb() => ServiceLocator.SingletonRegistrator
		                                               .Register(new SavesConfiguration
		                                               {
			                                               CurrentSave  = "1.db",
			                                               LocationPath = Path.Combine(Application.streamingAssetsPath, "_Saves")
		                                               })
		                                               .Register<IDatabaseService, DatabaseService>();
	}
}