using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// https://circuitstream.com/blog/two-handed-interactions-with-unity-xr-interaction-toolkit/
public class DoubleXrGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Transform secondAttachPoint;

    protected override void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
    }

    // Called every frame, so we can do our gestion here!
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(interactorsSelecting.Count == 1)
        {
            base.ProcessInteractable(updatePhase);
        }
        else if(interactorsSelecting.Count == 2 & updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            TwoHandGestion();
        }
    }


    protected override void Grab()
    {
        if(interactorsSelecting.Count == 1)
        {
            base.Grab();
        }
    }

    protected override void Drop()
    {
        if(!isSelected)
        {
            base.Drop();
        }
    }

    private void TwoHandGestion()
    {
        Transform firstAttach = GetAttachTransform(null);
        Transform firstHand = interactorsSelecting[0].transform;

        Transform secondAttach = secondAttachPoint;
        Transform secondHand = interactorsSelecting[0].transform;

        Vector3 directionBetween = secondHand.position - firstHand.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionBetween, firstHand.up);

        Vector3 worldDirection = transform.position - firstAttach.position;
        Vector3 localDirection = transform.InverseTransformDirection(worldDirection);
        Vector3 targetPosition = firstHand.position + targetRotation * localDirection;

        transform.SetPositionAndRotation(targetPosition, targetRotation);
    }
}
