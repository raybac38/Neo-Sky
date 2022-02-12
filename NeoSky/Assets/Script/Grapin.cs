using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grapin : MonoBehaviour
{
    [SerializeField]
    public GameObject Camera;
    public GameObject player;
    public GameObject ropeStarter;
    public GameObject anchorPoint;
    public GameObject parentAnchorPoint;
    public GameObject inFrontOfMe;
    private Vector3 pointAncrage;
    public Vector3 angle;
    public Vector3 placementPoint;
    public bool isGrappin;
    public bool canPlace;
    public float ropeDistance;
    public float ropeMoveSpeed = 0.5f;
    public float ropeMinDistance = 0.25f;
    public float ropeMaxDistance;
    public float interactionDistance = 2f;
    public float placementDistanceMax = 10f;
    public float placementDistanceMin = 1f;
    public LineRenderer lr;
    private SpringJoint joint;
    public PlayerMouvement playerMouvement;
    public Rigidbody rb;
    public LayerMask fixePoint;
    public LayerMask haverstable;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        ropeMaxDistance = 50f;
    }
    void Update()
    {
        CheckInFrontOfMe();
        CheckPlacementPoint();
        if (Input.GetMouseButtonDown(1))
        {
            if (!isGrappin)
            {
                StartGrappin();
            }
            else
            {
                StopGrappin();
            }
        }
        if (isGrappin)
        {
            DistanceRopeFlood();
            joint.connectedAnchor = anchorPoint.transform.position;

            GrappinDistanceMove();
        }

    }

    void StartGrappin()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, ropeMaxDistance, fixePoint | haverstable))
        {
            SetAnchorPoint(hit.collider.transform);

            pointAncrage = hit.point;
            anchorPoint.transform.position = pointAncrage;

            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = anchorPoint.transform.position;

            float distanceFromPoint = Vector3.Distance(player.transform.position, pointAncrage);

            //la distance sur laquelle on vas rester du point d'ancrage

            joint.maxDistance = distanceFromPoint;
            joint.minDistance = 0f;

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
        RemoveAnchorPoint();
    }
    void DrawRope()
    {
        if (!joint) return; //ne pas dessiner une corde si tu n'as pas d'ancre
        lr.SetPosition(0, ropeStarter.transform.position);
        lr.SetPosition(1, anchorPoint.transform.position);

    }
    private void LateUpdate()
    {
        DrawRope();
    }
    void GrappinDistanceMove()
    {
        if (!isGrappin) return; // si il n'y a pas de joint, y a rien a voire
        joint = player.GetComponent<SpringJoint>();


        ropeDistance = joint.maxDistance;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(ropeDistance - (ropeMoveSpeed * Time.deltaTime) > ropeMinDistance)
            {
                ropeDistance -= (ropeMoveSpeed * Time.deltaTime);
                rb.WakeUp();
            }
        }
        if (Input.GetKey(KeyCode.LeftControl) & !playerMouvement.isGrounded)
        {
            if (ropeDistance + (ropeMoveSpeed * Time.deltaTime) < ropeMaxDistance)
            {
                ropeDistance += (ropeMoveSpeed * Time.deltaTime);
            }
        }
        joint.maxDistance = ropeDistance;
    }
    
    void DistanceRopeFlood()
    {
        if (playerMouvement.isGrounded)
        {
            joint = player.GetComponent<SpringJoint>();
            float distanceDuPoint = Vector3.Distance(player.transform.position, pointAncrage);
            if (ropeDistance + 2 > distanceDuPoint & ropeDistance > ropeMaxDistance)
            {
                joint.maxDistance = distanceDuPoint;
            }
        }
    }
    void SetAnchorPoint(Transform parent)
    {
        anchorPoint.transform.SetParent(parent);
    }
    void RemoveAnchorPoint()
    {
        anchorPoint.transform.SetParent(player.transform);
    }

    void CheckInFrontOfMe()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, interactionDistance, haverstable))
        {
            inFrontOfMe = hit.transform.gameObject;
        }
        else
        {
            inFrontOfMe = null;
        }
    }

    void CheckPlacementPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, placementDistanceMax, haverstable))
        {
            inFrontOfMe = hit.transform.gameObject;
            if(Vector3.Distance(hit.transform.position, Camera.transform.position) < placementDistanceMin)
            {
                placementPoint = Vector3.zero;
                canPlace = false;

            }else if(Vector3.Distance(hit.transform.position, Camera.transform.position) < placementDistanceMax)
            {
                placementPoint = hit.point;
                canPlace = true;
            }else
            canPlace = false;               
        }

    }
}
