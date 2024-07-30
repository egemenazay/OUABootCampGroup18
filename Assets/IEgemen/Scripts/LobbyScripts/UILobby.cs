using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UILobby : MonoBehaviour
{
    [SerializeField] private TMP_InputField joinMatchInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button hostButton;
    
    public void Host()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
    }

    public void Join()
    {
        joinButton.interactable = false;
        hostButton.interactable = false;
        joinMatchInput.interactable = false;
    }
}
