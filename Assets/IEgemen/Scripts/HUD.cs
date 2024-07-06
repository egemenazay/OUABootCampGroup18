using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject massagePanel;
    
    public void OpenMassagePanel()
    {
        massagePanel.SetActive(true);
    }

    public void CloseMassagePanel()
    {
        massagePanel.SetActive(false);
    }
}
