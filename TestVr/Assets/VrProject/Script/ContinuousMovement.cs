using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
//using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    [SerializeField] private XRNode source;

    private XROrigin vr;
    private CharacterController player;
    private Vector2 inputAxis;

    [SerializeField] private LayerMask groundLayer;

    private float speed = 1f;
    private float gravity = -9.8f;
    private float fallingSpeed;
    private float defaultHeight = 0.2f;


    void Start()
    {
        player = GetComponent<CharacterController>();
        vr = GetComponent<XROrigin>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(source);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        // https://www.youtube.com/watch?v=5NRTT8Tbmoc&t=272s&ab_channel=Valem + comment de la vidéo
        // https://forum.unity.com/threads/xr-interaction-character-controller-and-teleportation-disconnect.1070450/

        Quaternion head = Quaternion.Euler(0, vr.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = head * new Vector3(inputAxis.x, 0, inputAxis.y);
        player.Move(direction * Time.fixedDeltaTime * speed);

        bool isGrounded = CheckIfGrounded();

        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        player.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(player.center);
        float rayLenght = player.center.y + 0.01f;
        return Physics.SphereCast(rayStart, player.radius, Vector3.down, out RaycastHit hitInfo, rayLenght, groundLayer);
    }

    void CapsuleFollowHeadset()
    {
        player.height = vr.CameraInOriginSpaceHeight + defaultHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(vr.Camera.transform.position);
        player.center = new Vector3(capsuleCenter.x, player.height / 2 + player.skinWidth, capsuleCenter.z);
    }
}
