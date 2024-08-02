using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Relay;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject keeperWinUI;
    [SerializeField] private GameObject catWinUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameID;
    [SerializeField] private GameObject timer;
    [SerializeField] private RelayManager _relayManager;

    private void Update()
    {
        if (_slider.value == _slider.maxValue)
        {
            FinishGameCatClientRpc();
        }
        else if (TimeCounter.timeRemaining == 0)
        {
            FinishGameKeeperRpc();
        }

        if (NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            StartGameClientRpc();
        }
    }

    [ClientRpc(RequireOwnership = false)]
    public void FinishGameCatClientRpc()
    {
        catWinUI.GetComponent<Canvas>().enabled = true;
        gameUI.GetComponent<Canvas>().enabled = false;
    }
    public void FinishGameKeeperRpc()
    {
        keeperWinUI.GetComponent<Canvas>().enabled = true;
        gameUI.GetComponent<Canvas>().enabled = false;
    }
    
    [ClientRpc(RequireOwnership = false)]
    public void StartGameClientRpc()
    {
        gameID.SetActive(false);
        timer.SetActive(true);
    }
    
    /*[ClientRpc]
    public void RestartGameClientRpc()
    {
        timer.SetActive(false);
        TimeCounter.timeRemaining = 30;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        NetworkManager.Singleton.Shutdown();
    }
    [ServerRpc(RequireOwnership = false)]
    public void RestartGameServerRpc()
    {
        timer.SetActive(false);
        TimeCounter.timeRemaining = 30;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        NetworkManager.Singleton.Shutdown();
    }*/

    public void RestartGame()
    {
        TimeCounter.timeRemaining = 180;
        catWinUI.GetComponent<Canvas>().enabled = false;
        keeperWinUI.GetComponent<Canvas>().enabled = false;
        timer.SetActive(false);
        StartCoroutine(ResetGameCoroutine());
    }
    private IEnumerator ResetGameCoroutine()
    {
        // Shutdown the NetworkManager
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
        }

        // Clean up relay allocations
        if (_relayManager != null)
        {
            yield return _relayManager.CleanupRelayAsync().AsCoroutine();
        }

        // Wait a frame to ensure the shutdown is processed
        yield return null;

        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Wait for the scene to load
        yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);

        // Restart the NetworkManager
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartHost(); // or StartClient() based on your logic
        }
    }

    public void OpenCredits()
    {
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);
    }

    public void OpenLobby()
    {
        mainMenuUI.SetActive(false);
    }
    
}
public static class TaskExtensions
{
    public static IEnumerator AsCoroutine(this Task task)
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }
        if (task.IsFaulted)
        {
            Debug.LogError(task.Exception);
        }
    }
}
