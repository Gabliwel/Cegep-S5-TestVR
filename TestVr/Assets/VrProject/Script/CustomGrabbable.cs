using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(XRGrabInteractable))]
[DisallowMultipleComponent]

//https://forum.unity.com/threads/grabbed-rigidbody-is-moving-my-xr-rig.1087481/
//remplacer par le layer

public class CustomGrabbable : MonoBehaviour
{
    private XRGrabInteractable XRGrab;
    private Collider[] Colliders;

    private void Start()
    {
        XRGrab = GetComponent<XRGrabInteractable>();
        Colliders = GetComponentsInChildren<Collider>();

        XRGrab.selectEntered.AddListener(Grab);
        XRGrab.selectExited.AddListener(Drop);
    }

    public void Grab(SelectEnterEventArgs args)
    {
        foreach (Collider collider in Colliders)
            collider.isTrigger = true;
    }

    public void Drop(SelectExitEventArgs args)
    {
        foreach (Collider collider in Colliders)
            collider.isTrigger = false;

    }
}
