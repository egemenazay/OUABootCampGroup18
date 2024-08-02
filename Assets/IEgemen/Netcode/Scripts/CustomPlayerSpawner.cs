using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CustomPlayerSpawner : NetworkBehaviour
{
    public static CustomPlayerSpawner Instance { get; private set; }

    public GameObject HostCharacterPrefab;
    public GameObject ClientCharacterPrefab;
    public GameObject spawnLocation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this spawner across scene loads
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            SpawnCharacter(clientId, true);
        }
        else
        {
            SpawnCharacter(clientId, false);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestSpawnClientCharacterServerRpc(ServerRpcParams rpcParams = default)
    {
        SpawnCharacter(rpcParams.Receive.SenderClientId, false);
    }

    private void SpawnCharacter(ulong clientId, bool isHost)
    {
        GameObject characterPrefab = isHost ? HostCharacterPrefab : ClientCharacterPrefab;
        GameObject playerInstance = Instantiate(characterPrefab, spawnLocation.transform.position, Quaternion.identity);
        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }
}
