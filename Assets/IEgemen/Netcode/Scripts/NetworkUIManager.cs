using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class NetworkUIManager : NetworkBehaviour
{
    public TMP_Text uiText;
    private NetworkVariable<string> uiTextNetworkVariable = new NetworkVariable<string>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Set initial value for the NetworkVariable
            uiTextNetworkVariable.Value = "Welcome to the game!";
        }

        // Subscribe to changes in the NetworkVariable
        uiTextNetworkVariable.OnValueChanged += OnUIValueChanged;

        // Update UI with initial value
        uiText.text = uiTextNetworkVariable.Value;
    }

    public override void OnDestroy()
    {
        uiTextNetworkVariable.OnValueChanged -= OnUIValueChanged;
    }

    private void OnUIValueChanged(string oldValue, string newValue)
    {
        uiText.text = newValue;
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateUITextServerRpc(string newText)
    {
        uiTextNetworkVariable.Value = newText;
    }
    
    [ClientRpc]
    public void UpdateUITextClientRpc(string newText)
    {
        if (!IsServer)
        {
            uiTextNetworkVariable.Value = newText;
        }
    }
}
