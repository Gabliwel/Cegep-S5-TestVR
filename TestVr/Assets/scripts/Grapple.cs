using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grapple : MonoBehaviour
{

    private LineRenderer rope;
    private XRRayInteractor controllerRayLine;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform player;
    private float maxGrappleDistance = 1000f;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;

    void Awake()
    {
        controllerRayLine = gameObject.GetComponent<XRRayInteractor>();
        rope = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") ||  Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("Fire1"))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(controllerRayLine.transform.position, controllerRayLine.transform.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            rope.positionCount = 2;
            currentGrapplePosition = controllerRayLine.transform.position;
        }
    }


    void StopGrapple()
    {
        rope.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        rope.SetPosition(0, controllerRayLine.transform.position);
        rope.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }
}
