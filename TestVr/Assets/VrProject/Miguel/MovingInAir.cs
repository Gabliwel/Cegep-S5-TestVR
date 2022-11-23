using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MovingInAir : MonoBehaviour
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject startingCollider;
    [SerializeField] private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if maincamera is inside startingCollider
        if (transform.position.y > 950)
        {
            Debug.Log("inside chu dedans");
        }
        else
        {
            if (Math.Abs(leftHand.transform.position.y - rightHand.transform.position.y) <= 0.1)
            {
                transform.position += mainCamera.transform.TransformDirection(Vector3.forward) * Time.deltaTime * 50;
            }
            //if right hand is higher than left hand then move left
            else if (rightHand.transform.position.y > leftHand.transform.position.y)
            {
                transform.position += mainCamera.transform.TransformDirection(Vector3.left) * Time.deltaTime * 50;
            }
            //if left hand is higher than right hand then move right
            else if (leftHand.transform.position.y > rightHand.transform.position.y)
            {
                transform.position += mainCamera.transform.TransformDirection(Vector3.right) * Time.deltaTime * 50;
            }
        }

        // if mainCamera y < 100 then move to 1576 1001 1315 (spawn)
        Debug.Log(transform.position.y);
        if (transform.position.y < 100)
        {
            transform.position = new Vector3(1576, 1001, 1315);
        }
    }
}
