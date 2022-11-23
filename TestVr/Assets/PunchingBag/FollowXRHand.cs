using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForceBasedMove;


public class FollowXRHand : MonoBehaviour
{
    public GameObject realLifeObject;

    public GameObject ghost;
    private float showGhostDistance = 0.8f;

    private Rigidbody rb;
    private int collisions =0;

    private float forceCatchupScale = 14f;
    private float torqueCatchupScale = 14f;

    private bool isJoiningBack = true;
    private float joiningBackEndTime = -1;
    private float joiningBackDuration = 0.2f;

    void Awake()
    {
        if (rb == null) rb = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        if (Time.time > joiningBackEndTime) isJoiningBack = false;
    }

    void EnableRealLifeGhostMesh()
    {
        ghost.SetActive(true);
        //if (!meshDisabled) return;
        //meshDisabled = false;
        //foreach (var mesh in realLifeGhostMeshes)
        //{
        //    mesh.enabled = true;
        //}
    }

    void DisableRealLifeGhostMesh()
    {
        ghost.SetActive(false);
        //if (meshDisabled) return;
        //meshDisabled = true;
        //foreach (var mesh in realLifeGhostMeshes)
        //{
        //    mesh.enabled = false;
        //}
    }

    private void FixedUpdate()
    {
        //NO VR MODE
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float delta = 5 * Time.fixedDeltaTime;
        float up = Input.GetKeyDown("space") ? 1 : 0;
        realLifeObject.transform.Translate(new Vector3(horizontal * delta, up * delta, vertical * delta));
        //Debug.Log(realLifeObject.transform.position);


        if (IsColliding())
        {
            Debug.Log("COLLIDING");

            if (Vector3.Distance(transform.position, ghost.transform.position) >= showGhostDistance)
                EnableRealLifeGhostMesh();
            else DisableRealLifeGhostMesh();

            // Try to go back to real life object
            /*
            var delta = realLifeGrabSpotTransform.transform.position - physicsVRGrabSpotTransform.position;
            rb.AddForce(delta * forceCatchupScale);
            var deltaRotation = Quaternion.FromToRotation(physicsVRGrabSpotTransform.forward, realLifeGrabSpotTransform.transform.forward);
            rb.AddTorque(deltaRotation.eulerAngles* torqueCatchupScale);
            */

            var forceCatchupScale = this.forceCatchupScale;
            var torqueCatchupScale = this.torqueCatchupScale;
            if (isJoiningBack)
            {
                //forceCatchupScale *= 4;
                //torqueCatchupScale *= 4;
            }

            rb.AddForceTowards(transform.position, realLifeObject.transform.position, frequency: forceCatchupScale);
            rb.AddTorqueTowards(transform.rotation, realLifeObject.transform.rotation, frequency: torqueCatchupScale);

            // Small additional force if the hand is in the right position, but bended
            //rb.AddForceTowards(physicsVRCollisionSpotTransform.position, realLifeCollisionSpotTransform.position, frequency: forceCatchupScale / 2);
        }
        else if (isJoiningBack && false)
        {
            Debug.Log("JOINING BACK");
            return;
            var returnStart = joiningBackEndTime - joiningBackDuration;
            var returnProgress = (Time.time - returnStart) / joiningBackDuration;

            transform.position = Vector3.Lerp(transform.position, realLifeObject.transform.position, returnProgress);
            transform.rotation = Quaternion.Lerp(transform.rotation, realLifeObject.transform.rotation, returnProgress);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Debug.Log("FREE MOVEMENT");
            DisableRealLifeGhostMesh();

            transform.position = realLifeObject.transform.position;
            transform.rotation = realLifeObject.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        //if (isJoiningBack) return;
        //isCollisioning = true;

        //Vector3 localPosition = physicsVRObject.transform.InverseTransformPoint(collision.GetContact(0).point);
        //physicsVRCollisionSpotTransform.localPosition = localPosition;
        //realLifeCollisionSpotTransform.localPosition = localPosition;
    }

    private void OnCollisionStay(Collision collision)
    {
        //isCollisioning = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions--;
        //isCollisioning = false;
        //joiningBackEndTime = Time.time + joiningBackDuration;
        //isJoiningBack = true;
    }

    private bool IsColliding()
    {
        return collisions > 0;
    }
}