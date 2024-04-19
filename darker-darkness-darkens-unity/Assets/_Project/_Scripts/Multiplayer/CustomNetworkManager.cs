using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class CustomNetworkManager : NetworkManager {
    
    [Header("Additional entities")]
    [SerializeField] private GameObject _voiceChatPrefab;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var start = FindFirstObjectByType<PlayerSpawner>().GetSpawnLocation();
        var player = Instantiate(playerPrefab, start, quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);
        
        // Spawn an additional entity and assign authority to the same connection
        var voiceChatInstance = Instantiate(_voiceChatPrefab);
        NetworkServer.Spawn(voiceChatInstance, conn);
    }
}