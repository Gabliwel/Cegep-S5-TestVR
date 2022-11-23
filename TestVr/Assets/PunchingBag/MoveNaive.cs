using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNaive : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed = 5f;

    Vector3 virtualPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        virtualPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float dspeed = speed * Time.fixedDeltaTime;
        virtualPosition += new Vector3(horizontal * dspeed, 0, vertical * dspeed);

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        Vector3 diff = virtualPosition - transform.position;
        double dist = Math.Sqrt(diff.x * diff.x + diff.y * diff.y);
        dist = Math.Sqrt(dist * dist + diff.z * diff.z);
        
        rigidbody.AddForce(diff * (float)dist * 50);
    }

}
