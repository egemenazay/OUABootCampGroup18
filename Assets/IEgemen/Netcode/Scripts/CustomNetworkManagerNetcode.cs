using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CustomNetworkManagerNetcode : MonoBehaviour
{
    public GameObject hostPrefab;
    public GameObject clientPrefab;
    public Transform spawnLoc;

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    }

    private void HandleServerStarted()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            // Sunucu tarafında oyuncuları spawn et
            SpawnPlayer(NetworkManager.Singleton.LocalClientId, true);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        // İstemci tarafında spawn etme işlemi yapılmaz
        // Sunucunun bu işlemi yapmasını bekleriz
    }

    private void SpawnPlayer(ulong clientId, bool isHost)
    {
        if (!NetworkManager.Singleton.IsServer) return; // Spawn işlemini sadece sunucu yapabilir

        GameObject playerPrefab = isHost ? hostPrefab : clientPrefab;
        var playerInstance = Instantiate(playerPrefab, spawnLoc.position, playerPrefab.transform.rotation);
        NetworkObject networkObject = playerInstance.GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId);
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
    }
}
