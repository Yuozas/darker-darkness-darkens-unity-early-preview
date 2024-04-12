using Fusion;
using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class PlayerNetworkSpawner : MonoBehaviour
{
    private NetworkRunner _runner;
    public GameObject playerPrefab; // Assign your player prefab in the inspector

    private async void Start()
    {
        _runner = ServiceLocator.GetSingleton<P2PNetworkManager>().GetNetworkRunner();
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (_runner.IsCloudReady && _runner.IsPlayer)
        {
            _runner.Spawn(playerPrefab, transform.position, Quaternion.identity, _runner.LocalPlayer);
        }
    }
}