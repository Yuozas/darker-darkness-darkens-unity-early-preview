using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using static Unity.Collections.Unicode;

[RequireComponent(typeof(NetworkRunner))]
[RequireComponent(typeof(NetworkSceneManagerDefault))]
public class P2PNetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;

    private readonly Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new();

    private NetworkRunner _networkRunner;

    private void Awake()
    {
        _networkRunner = GetComponent<NetworkRunner>();
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
        var startArgs = GetGameStartArguments(mode, hostIP);
        var result = await _networkRunner.StartGame(startArgs);
        if (!result.ErrorMessage.IsNullOrEmpty() && !string.Equals(result.ErrorMessage, "ok", System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogError($"Error starting game: {result.ErrorMessage}");
            return false;
        }
        return true;
    }

    private StartGameArgs GetGameStartArguments(GameMode mode, string hostIP)
    {
        var scene = SceneRef.FromIndex(2);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

        return new StartGameArgs
        {
            GameMode = mode,
            SessionName = "P2PSession",
            Scene = scene,
            SceneManager = GetComponent<NetworkSceneManagerDefault>(),
            Address = hostIP is null ? NetAddress.LocalhostIPv4() : NetAddress.CreateFromIpPort(hostIP, 0),
        };
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (!runner.IsServer)
            return;

        var spawnLocation = FindAnyObjectByType<PlayerSpawner>().GetSpawnLocation();
        var networkObject = runner.Spawn(_playerPrefab, spawnLocation, Quaternion.identity, player);
        _spawnedPlayers.Add(player, networkObject);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if(_spawnedPlayers.TryGetValue(player, out var networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedPlayers.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}