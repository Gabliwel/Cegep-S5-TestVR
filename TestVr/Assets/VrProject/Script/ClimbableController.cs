using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// https://www.youtube.com/watch?v=mHHYI7hzZ6M&t=5s&ab_channel=Valem

public class ClimbableController : XRBaseInteractable
{
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if(interactor is XRDirectInteractor)
            Climber.hand = interactor.GetComponent<XRController>();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if (interactor is XRDirectInteractor)
        {
            if(Climber.hand && Climber.hand.name == interactor.name)
            {
                Climber.hand = null;
            }
        }
    }
}
