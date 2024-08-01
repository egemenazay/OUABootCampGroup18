using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnlineBuffSpawner : NetworkBehaviour
{
    [SerializeField] private Transform spawnedObjectPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(spawnedObjectPrefab); 
        }
    }
}
