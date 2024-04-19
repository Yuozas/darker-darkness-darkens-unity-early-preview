using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;

public class Initializer : IPreliminarySetup
{
    private const string P2P_NETWORK_MANAGER_PREFAB_NAME = "Network Manager";
    public void Setup()
    {
        if (!Application.isPlaying)
            return;

        ServiceLocator.SingletonRegistrator
            .Register(_ => 
            {
                var prefab = Resources.Load<CustomNetworkManager>(P2P_NETWORK_MANAGER_PREFAB_NAME);
                return Instantiator.InstantiateAndDontDestroy(prefab);
            });
    }
}