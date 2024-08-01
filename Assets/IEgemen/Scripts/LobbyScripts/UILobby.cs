using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UILobby : MonoBehaviour
{
    public static UILobby instance;
    
    [Header("HostJoin")]
    [SerializeField] private TMP_InputField joinMatchInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Canvas lobbyCanvas;

    [Header("Lobby")]
    [SerializeField] private Transform UIPlayerParent;
    [SerializeField] private GameObject UIPlayerPrefab;
    [SerializeField] private TMP_Text matchIDText;

    [SerializeField] private GameObject beginGameButton;
    
    private void Start()
    {
        instance = this;

    }

    public void Host()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
        
        Player.localPlayer.HostGame(); 
    }

    public void HostSuccess(bool success, string matchID)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;
            
            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = matchID;
            beginGameButton.SetActive(true);
        }
        else
        {
            joinButton.interactable = true;
            hostButton.interactable = true;
            joinMatchInput.interactable = true; 
        }
    }
    public void Join()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
        
        Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
    }

    public void JoinSuccess(bool success, string matchID)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;
            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIDText.text = matchID;
        }
        else
        {
            joinButton.interactable = true;
            hostButton.interactable = true;
            joinMatchInput.interactable = true;
        }
    }

    public GameObject SpawnPlayerUIPrefab(Player player)
    {
        GameObject newUIPlayer = Instantiate (UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<UIPlayer> ().SetPlayer (player);
        newUIPlayer.transform.SetSiblingIndex (player.playerIndex - 1);

        return newUIPlayer;
    }

    public void BeginGame()
    {
        Player.localPlayer.BeginGame();
    }
}
