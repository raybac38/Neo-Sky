using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grapin : MonoBehaviour
{
    [SerializeField]
    private Vector3 pointAncrage;
    public bool isGrappin;
    public float ropeDistance;
    public LineRenderer lr;
    public GameObject Camera;
    public LayerMask fixePoint;
    private SpringJoint joint;
    public GameObject player;
    public GameObject ropeStarter;
    public Vector3 angle;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        ropeDistance = 100f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isGrappin)
            {
                StartGrappin();
                Debug.Log("tentativde grappin");
            }
            else
            {
                StopGrappin();
            }
        }
    }

    void StartGrappin()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, ropeDistance, fixePoint))
        {
            pointAncrage = hit.point;
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = pointAncrage;

            float distanceFromPoint = Vector3.Distance(player.transform.position, pointAncrage);

            //la distance sur laquelle on vas rester du point d'ancrage
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //reglage de la corde
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            isGrappin = true;
            lr.positionCount = 2;
        }
    }
    void StopGrappin()
    {
        isGrappin = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
    void DrawRope()
    {
        if (!joint) return; //ne pas dessiner une corde si tu n'as pas d'ancre
        lr.SetPosition(0, ropeStarter.transform.position);
        lr.SetPosition(1, pointAncrage);

    }
    private void LateUpdate()
    {
        DrawRope();
    }
}
