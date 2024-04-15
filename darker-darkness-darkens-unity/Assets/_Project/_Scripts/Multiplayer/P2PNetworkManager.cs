using Fusion;
using Fusion.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;

[RequireComponent(typeof(NetworkRunner))]
public class P2PNetworkManager : MonoBehaviour
{
    private NetworkRunner _networkRunner;

    private void Awake()
    {
        _networkRunner = GetComponent<NetworkRunner>();    
    }

    private void Start()
    {
        // Optionally, automatically start hosting or joining for testing
        //StartHost();
    }

    public NetworkRunner GetNetworkRunner()
    {
        return _networkRunner;
    }

    public Task<bool> StartHost()
    {
        return StartRunner(GameMode.Host);
    }

    public Task<bool> StartClient(string hostIP)
    {
        return StartRunner(GameMode.Client, hostIP);
    }

    private async Task<bool> StartRunner(GameMode mode, string hostIP = null)
    {
        var startArgs = new StartGameArgs
        {
            GameMode = mode,
            SessionName = "P2PSession",
            Scene = SceneRef.FromIndex(2),
            Address = hostIP is null ? NetAddress.LocalhostIPv4() : NetAddress.CreateFromIpPort(hostIP, 0),
        };

        var result = await _networkRunner.StartGame(startArgs);
        if (!result.ErrorMessage.IsNullOrEmpty() && !string.Equals(result.ErrorMessage, "ok", System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogError($"Error starting game: {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    public async Task LoadScene(int index = 2)    {
        await _networkRunner.LoadScene(SceneRef.FromIndex(index));
        //var spawner = GetComponent<PlayerNetworkSpawner>();
        //spawner.SpawnPlayers(index);
    }
}