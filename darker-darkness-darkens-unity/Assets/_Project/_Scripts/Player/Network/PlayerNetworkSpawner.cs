using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetworkSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (!HasStateAuthority)
            return;
        var activeScene = SceneManager.GetActiveScene();
        Debug.Log($"PlayerJoined: {activeScene.name}, {activeScene.buildIndex}");

        //Players.Add(player);
        //var spawnPoint = GameObject.FindWithTag("SpawnPoint");
        //Debug.Log(spawnPoint.name);
        Runner.Spawn(_playerPrefab, transform.position, Quaternion.identity, player);
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (!HasStateAuthority)
            return;
        Debug.Log("Player Left.");

        //Players.Remove(player);
    }

    public void SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
    {
        Debug.Log("scene load done.");
    }

    public void SpawnPlayers(int sceneIndex)
    {
        //var spawnPoint = GameObject.FindWithTag("SpawnPoint");
        //foreach(var player in Players)
        //    Runner.Spawn(_playerPrefab, spawnPoint.transform.position, Quaternion.identity, player);
    }
}