using Cinemachine;
using Fusion;
using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class PlayerCameraNetworkHandler : NetworkBehaviour
{
    public override void Spawned()
    {
        base.Spawned();

        var localNetworkRunner = ServiceLocator.GetSingleton<P2PNetworkManager>().GetNetworkRunner();
        Debug.Log($"{localNetworkRunner.UserId != Runner.UserId}, {localNetworkRunner.UserId}, {Runner.UserId}");
        if (localNetworkRunner.UserId != Runner.UserId)
            return;

        // Temporary.
        var virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        virtualCamera.Follow = transform;
        Debug.Log("Cameras setup.");
    }
}
