using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;

public class Initializer : IPreliminarySetup
{
    const string P2P_NETWORK_MANAGER_PREFAB_NAME = "P2P Network Manager";
    public void Setup()
    {
        if (!Application.isPlaying)
            return;

        ServiceLocator.SingletonRegistrator
            .Register(provider => 
            {
                var prefab = Resources.Load<P2PNetworkManager>(P2P_NETWORK_MANAGER_PREFAB_NAME);
                return Instantiator.InstantiateAndDontDestroy(prefab);
            });
    }
}