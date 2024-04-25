using Euphelia.Instantiation;
using Euphelia.Multiplayer;
using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

namespace Euphelia.Initialization
{
	public class Initializer : IPreliminarySetup
	{
		private const string NETWORK_MANAGER_PREFAB_NAME = "Network Manager";

		public void Setup()
		{
			if (!Application.isPlaying)
				return;

			ServiceLocator.SingletonRegistrator
			              .Register(_ =>
			              {
				              var prefab = Resources.Load<CustomNetworkManager>(NETWORK_MANAGER_PREFAB_NAME);
				              return Instantiator.InstantiateAndDontDestroy(prefab);
			              });
		}
	}
}