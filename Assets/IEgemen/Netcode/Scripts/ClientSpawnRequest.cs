using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ClientSpawnRequest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsClient && !IsHost)
        {
            CustomPlayerSpawner customPlayerSpawner = FindObjectOfType<CustomPlayerSpawner>();
            customPlayerSpawner.RequestSpawnClientCharacterServerRpc();
        }
        Destroy(gameObject); // Destroy the base player prefab since it is not needed anymore
    }
}
