using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grapin : MonoBehaviour
{
    [SerializeField]
    private Vector3 pointAncrage;
    public bool isGrappin;
    public float ropeMaxDistance;
    public LineRenderer lr;
    public GameObject Camera;
    public LayerMask fixePoint;
    private SpringJoint joint;
    public GameObject player;
    public GameObject ropeStarter;
    public Vector3 angle;
    public float ropeDistance;
    public float ropeMoveSpeed = 0.5f;
    public float ropeMinDistance = 0.25f;
    public PlayerMouvement playerMouvement;
    public Rigidbody rb;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        ropeMaxDistance = 50f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isGrappin)
            {
                StartGrappin();
                Debug.Log("tentatide grappin");
            }
            else
            {
                StopGrappin();
            }
        }
        if (isGrappin)
        {
            DistanceRopeFlood();
            GrappinDistanceMove();
        }

    }

    void StartGrappin()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, ropeMaxDistance, fixePoint))
        {
            pointAncrage = hit.point;
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = pointAncrage;

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
    void GrappinDistanceMove()
    {
        if (!isGrappin) return; // si il n'y a pas de joint, y a rien a voire
        joint = player.GetComponent<SpringJoint>();

        Debug.Log("modification longeur");


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
}
