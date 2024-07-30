using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class MatchMaker : NetworkBehaviour
{
    public NetworkManager networkManager;

    public Button hostButton;
    public Button joinButton;
    public TMP_InputField matchIDInput;


    public void HostGame()
    {
        string matchID = GenerateMatchID();
        Debug.Log($"Generated Match ID: {matchID}");

        if (MatchManager.Instance.HostGame(matchID, NetworkServer.localConnection))
        {
            NetworkManager.singleton.StartHost();
        }
        else
        {
            Debug.Log("Failed to create match, match ID already exists.");
        }
    }

    public void JoinGame()
    {
        string matchID = matchIDInput.text;
        if (matchID.Length != 5)
        {
            Debug.Log("Match ID must be 5 characters long.");
            return;
        }

        NetworkManager.singleton.networkAddress = "localhost"; // Sunucu adresi
        NetworkManager.singleton.StartClient();


        CmdJoinGame(matchID);
    }

    [Command]
    void CmdJoinGame(string matchID)
    {
        if (MatchManager.Instance.JoinGame(matchID, connectionToClient))
        {
            // Başarılı giriş
        }
        else
        {
            // Başarısız giriş
        }
    }

    private string GenerateMatchID()
    {
        string _id = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int random = Random.Range(0, 36);
            if (random < 26)
            {
                _id += (char)(random + 65);
            }
            else
            {
                _id += (random - 26).ToString();
            }
        }
        return _id;
    }

    private void OnMatchMessage(NetworkConnection conn, MatchMessage message)
    {
        if (message.success)
        {
            Debug.Log("Joined match successfully.");
            // Oyunu başlat
            MatchManager.Instance.StartGame(message.matchID);
        }
        else
        {
            Debug.Log("Failed to join match.");
        }
    }
}

public struct MatchMessage : NetworkMessage
{
    public bool success;
    public string matchID;
}

    

