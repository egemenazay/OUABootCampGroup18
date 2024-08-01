using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CustomPlayerSpawner : NetworkBehaviour
{
    public GameObject HostCharacterPrefab;
    public GameObject ClientCharacterPrefab;

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
        GameObject playerInstance = Instantiate(characterPrefab);
        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }
}
