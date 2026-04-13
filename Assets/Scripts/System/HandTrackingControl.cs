using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Hands;

public class HandTrackingControl : MonoBehaviour
{
    [SerializeField] GameObject HandsParent;
    [SerializeField] GameObject ControllersParent;
    [SerializeField] bool isTrackingControllers;
    private const string ControllerDevice_Name = "Oculus Touch Controller (OpenXR)";
    List<XRHandSubsystem> HandSubsystems = new List<XRHandSubsystem>();

    private void OnEnable()
    {
        SubsystemManager.GetSubsystems(HandSubsystems);
        if (HandSubsystems.Count == 0)
        {
            Debug.LogWarning("Hand Tracking Subsystem not found, can't subscribe to hand tracking status. Enable that feature in the OpenXR project settings and ensure OpenXR is enabled as the plug-in provider.", this);
        }
        else
        {
            HandSubsystems[0].trackingAcquired += OnHandTrackingAquired;
            HandSubsystems[0].trackingLost += OnHandTrackingLost;
        }
        InputSystem.onDeviceChange += OnDeviceChange;
    }
    private void OnDisable()
    {
        if (HandSubsystems.Count == 0)
        {
            Debug.LogWarning("Hand Tracking Subsystem not found, can't subscribe to hand tracking status. Enable that feature in the OpenXR project settings and ensure OpenXR is enabled as the plug-in provider.", this);
        }
        else
        {
            HandSubsystems[0].trackingAcquired -= OnHandTrackingAquired;
            HandSubsystems[0].trackingLost -= OnHandTrackingLost;
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {

        if (change == InputDeviceChange.Added && !device.added)
        {
            if (device.displayName == ControllerDevice_Name)
            {
                isTrackingControllers = true;
                ControllersParent.SetActive(true);
                HandsParent.SetActive(false);
            }

            //Debug.Log(device.displayName);
        }
        else if (change == InputDeviceChange.Removed && device.displayName == ControllerDevice_Name)
        {
            InputSystem.FlushDisconnectedDevices();
        }
        Debug.Log(device.displayName + change);
    }

    private void OnHandTrackingLost(XRHand hand)
    {
        //Debug.Log("OnHandTrackingLost");
    }

    private void OnHandTrackingAquired(XRHand hand)
    {
        HandsParent.SetActive(true);
        ControllersParent.SetActive(false);
        isTrackingControllers = false;
        //InputSystem.FlushDisconnectedDevices();
        Debug.Log("OnHandTrackingAquired");
    }
}
