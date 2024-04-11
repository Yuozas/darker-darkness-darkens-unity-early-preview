using Fusion;
using Fusion.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;

public class P2PNetworkManager : MonoBehaviour
{
    public NetworkRunner runnerPrefab; // Assign in inspector
    private NetworkRunner runnerInstance;

    private void Start()
    {
        // Optionally, automatically start hosting or joining for testing
        //StartHost();
    }

    public NetworkRunner GetNetworkRunner()
    {
        return runnerInstance;
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
        runnerInstance = Instantiate(runnerPrefab);
        var startArgs = new StartGameArgs
        {
            GameMode = mode,
            SessionName = "P2PSession",
            Address = hostIP is null ? NetAddress.LocalhostIPv4() : NetAddress.CreateFromIpPort(hostIP, 0),
        };

        var result = await runnerInstance.StartGame(startArgs);
        if (!result.ErrorMessage.IsNullOrEmpty() && !string.Equals(result.ErrorMessage, "ok", System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogError($"Error starting game: {result.ErrorMessage}");
            return false;
        }
        return true;
    }
}
