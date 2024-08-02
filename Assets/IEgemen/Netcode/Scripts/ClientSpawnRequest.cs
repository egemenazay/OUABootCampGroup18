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
            if (CustomPlayerSpawner.Instance != null)
            {
                CustomPlayerSpawner.Instance.RequestSpawnClientCharacterServerRpc();
            }
            else
            {
                Debug.LogError("CustomPlayerSpawner instance is not set.");
            }
        }
        Destroy(gameObject); // Destroy the base player prefab since it is not needed anymore
    }
}
