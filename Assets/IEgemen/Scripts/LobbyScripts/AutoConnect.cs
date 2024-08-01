using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoConnect : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    private void Start()
    {
        if (!Application.isBatchMode)
        {
            Debug.Log("***** Client Build *****");
            _networkManager.StartClient();
        }
        else
        {
            Debug.Log("***** Server Build *****");
        }
    }

    public void JoinLocal()
    {
        _networkManager.networkAddress = "localHost";
        _networkManager.StartClient();
    }
}
