using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

// https://www.youtube.com/watch?v=mHHYI7hzZ6M&t=5s&ab_channel=Valem

public class Climber : MonoBehaviour
{
    private CharacterController player;
    public static XRController hand;
    private ContinuousMovement continuousMouvement;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<CharacterController>();
        continuousMouvement = gameObject.GetComponent<ContinuousMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hand)
        {
            continuousMouvement.enabled = false;
            Climb();
        }
        else
        {
            continuousMouvement.enabled = true;
        }
    }

    private void Climb()
    {
        InputDevices.GetDeviceAtXRNode(hand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 vector);
        player.Move(transform.rotation * -vector * Time.deltaTime);
    }
}
