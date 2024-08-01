using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public GameObject[] playerPrefabs;
    public Transform catTransform;
    public Transform keeperTransform;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        int playerIndex = NetworkServer.connections.Count - 1;
        GameObject playerPrefab = playerPrefabs[playerIndex % playerPrefabs.Length];
        GameObject player = Instantiate(playerPrefab, catTransform.transform);

        NetworkServer.AddPlayerForConnection(conn, player);
    }
    
}
