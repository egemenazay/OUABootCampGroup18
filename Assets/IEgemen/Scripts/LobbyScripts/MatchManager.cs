using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MatchManager : NetworkBehaviour
{
    public static MatchManager Instance;

    private Dictionary<string, List<NetworkConnection>> matches = new Dictionary<string, List<NetworkConnection>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HostGame(string matchID, NetworkConnection conn)
    {
        if (!matches.ContainsKey(matchID))
        {
            matches[matchID] = new List<NetworkConnection>();
            matches[matchID].Add(conn);
            return true;
        }
        return false;
    }

    public bool JoinGame(string matchID, NetworkConnection conn)
    {
        if (matches.ContainsKey(matchID))
        {
            matches[matchID].Add(conn);
            return true;
        }
        return false;
    }

    public void StartGame(string matchID)
    {
        if (matches.ContainsKey(matchID))
        {
            foreach (var conn in matches[matchID])
            {
                // Oyunu başlatma işlemi
                TargetStartGame(conn);
            }
        }
    }

    [TargetRpc]
    void TargetStartGame(NetworkConnection conn)
    {
        // Oyunu başlat
        // Örneğin, sahneyi yükleyin
        NetworkManager.singleton.ServerChangeScene("OnlineMirror");
    }
}
