using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    [SyncVar] public int playerIndex;
    private NetworkMatch networkMatch;
    
    private void Start()
    {
        networkMatch = GetComponent<NetworkMatch>();
        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            UILobby.instance.SpawnPlayerUIPrefab(this);
        }
    }
    //HOST MATCH
    public void HostGame()
    {
        string matchID = MatchMaker.GenerateMatchID();
        CmdHostGame(matchID);
    }
    
    [Command]
    void CmdHostGame (string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.HostGame(_matchID, this, out playerIndex))
        {
            Debug.Log($"<color = green>Game Hosted Successfully");
            networkMatch.matchId = _matchID.ToGuid();
            TargetHostGame(true,_matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color = red>Game Hosted Failed");
            TargetHostGame(false,_matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID, int _playerIndex)
    {
        playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"MatchID = {matchID} == {_matchID}");
        UILobby.instance.HostSuccess(success, _matchID);
    }
    
    //JOIN MATCH 
    public void JoinGame(string _inputID)
    {
        CmdJoinGame(_inputID);
    }
    
    [Command]
    void CmdJoinGame (string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.JoinGame(_matchID, this, out playerIndex))
        {
            Debug.Log($"<color = green>Game Joined Successfully");
            networkMatch.matchId = _matchID.ToGuid();
            TargetJoinGame(true,_matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color = red>Game Joined Failed");
            TargetJoinGame(false,_matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID, int _playerIndex)
    {
        playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"MatchID = {matchID} == {_matchID}");
        UILobby.instance.JoinSuccess(success,_matchID);
    }
    
    //BEGIN MATCH
    public void BeginGame()
    {
        CmdBeginGame();
    }
    
    [Command]
    void CmdBeginGame ()
    {
        MatchMaker.instance.BeginGame(matchID);
        Debug.Log($"<color = red>Game Beginning");
    }

    public void StartGame()
    {
        TargetBeginGame();
    }
    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"MatchID = {matchID} | Beginning");
        //Additivelt start game
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        
    }
    
}
