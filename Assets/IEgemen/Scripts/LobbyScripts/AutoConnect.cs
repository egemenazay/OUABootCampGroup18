using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoConnect : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    public void JoinLocal()
    {
        _networkManager.networkAddress = "localHost";
        _networkManager.StartClient();
    }
}
