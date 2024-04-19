using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var start = FindFirstObjectByType<PlayerSpawner>().GetSpawnLocation();
        var player = Instantiate(playerPrefab, start, quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}