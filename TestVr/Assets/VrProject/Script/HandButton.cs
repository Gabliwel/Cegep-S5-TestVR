using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRSimpleInteractable
{
    //Non fonctionnel
    /*
    private XRController hand = null;
    private Vector3 initialPosition;
    private float yLimit = 0.93f;
    private bool wasPressed = false;

    private void Awake()
    {
        base.Awake();

        initialPosition = transform.position;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if (interactor is XRDirectInteractor)
            hand = interactor.GetComponent<XRController>();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if (interactor is XRDirectInteractor)
        {
            if (hand && hand.name == interactor.name)
            {
                hand = null;
            }
        }
    }

    private void ButtonPress()
    {
        Debug.Log("press!");
        wasPressed = true;
    }

    private void Update()
    {
        
    }*/
}
