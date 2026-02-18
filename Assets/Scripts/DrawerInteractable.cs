using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DrawerInteractable : MonoBehaviour
{
[SerializeField] XRSocketInteractor keySocket;
[SerializeField] bool isLocked;

    void Start()
    {
        if(keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);

        }
    }

    void Update()
    {
        
    }

    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
        isLocked = true;
        Debug.Log("**** DRAWER LOCKED! ****");
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        isLocked = false;
        Debug.Log("**** DRAWER UNLOCKED! ****");
    }
}
