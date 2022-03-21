using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grapin : MonoBehaviour
{
    [SerializeField]
    public GameObject pivotCamera;
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
    public float ropeMaxDistance = 50f;
    public float interactionDistance = 2f;
    public float placementDistanceMax = 10f;
    public float placementDistanceMin = 1f;
    public LineRenderer lr;
    private SpringJoint joint;
    public PlayerMouvement playerMouvement;
    public Rigidbody rb;
    public LayerMask fixePoint;
    public LayerMask haverstable;

    private List<GameObject> anchorPositionListe = new List<GameObject>();


    //nouvelle variables
    public List<Vector3> appartientAuPlan = new List<Vector3>();
    public float tolerancePlan = 0.2f;

    public void CommenceGrappin()
    {
        RaycastHit raycast;
        if(Physics.Raycast(pivotCamera.transform.position, pivotCamera.transform.eulerAngles, out raycast, ropeMaxDistance, fixePoint | haverstable))
        {
            FindAnchorPointLocation(raycast);
        }
    }

    public void FindAnchorPointLocation(RaycastHit raycast)
    {
        // crée un plan
        Vector3 normal = raycast.normal;
        Vector3 point = raycast.point;
        float d = -normal.x * point.x - normal.y * point.y - normal.z * point.z;
        Vector4 plan = new Vector4(normal.x, normal.y, normal.z, d);

        //chercher TOUT les points appartenant au plan
        Vector3[] vertices;
        Mesh mesh;
        mesh = raycast.transform.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 coordonnee = vertices[i];

            float nb = plan.x * coordonnee.x + plan.y * coordonnee.y + plan.z * coordonnee.z + plan.w;
            if (-tolerancePlan < nb & nb < tolerancePlan)
            {
                appartientAuPlan.Add(vertices[i]);
            }
        }

        print(appartientAuPlan);
        //retourne tout les coordonnée de point appartenant a la liste
        print(appartientAuPlan.Count);
        //retourne le nb de sommet
    }



























    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        ropeMaxDistance = 50f;
    }
    void Update()
    {
        CheckInFrontOfMe();
        CheckPlacementPoint();
        if (Input.GetKeyDown(KeyCode.H))
        {
            CommenceGrappin();
        }
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
            jointManager();
            GrappinDistanceMove();
            CheckNewAnchorPoint();
        }

    }

    void StartGrappin()
    {
        RaycastHit hit;
        if(Physics.Raycast(pivotCamera.transform.position, pivotCamera.transform.forward, out hit, ropeMaxDistance, fixePoint | haverstable))
        {
            SetFirstAnchorPoint(hit.collider.transform, hit.point);
            isGrappin = true;

            lr.positionCount = 2;
            jointManager();


        }
    }
    void StopGrappin()
    {
        isGrappin = false;
        lr.positionCount = 0;
        Destroy(joint);
        RemoveAllAnchorPoint();
    }
    void DrawRope()
    {
        if (!joint) return; //ne pas dessiner une corde si tu n'as pas d'ancre
        lr.positionCount = 1 + anchorPositionListe.Count;
        lr.SetPosition(0, ropeStarter.transform.position);
        for (int i = 0; i < anchorPositionListe.Count; i++)
        {
            lr.SetPosition(i + 1, anchorPositionListe[i].transform.position);
        }
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
    void SetFirstAnchorPoint(Transform parent, Vector3 position)
    {
        if(anchorPositionListe.Count != 0)
        {
            return;
        }
        anchorPositionListe.Add(ropeStarter);
        anchorPositionListe.Add(Instantiate(anchorPoint, parent));
        anchorPositionListe[1].transform.position = position;
        //premier point d'ancrage
    }
    void RemoveAllAnchorPoint()
    {
        for (int i = 0; i < anchorPositionListe.Count; i++)
        {
            if(anchorPositionListe[i] == ropeStarter)
            {

            }
            else
            {
            Destroy(anchorPositionListe[i], 0.1f);

            }
        }
        anchorPositionListe = new List<GameObject>();
        
        //suppresion de TOUT les points d'ancrage
    }

    void CheckInFrontOfMe()
    {
        RaycastHit hit;
        if (Physics.Raycast(pivotCamera.transform.position, pivotCamera.transform.forward, out hit, interactionDistance, haverstable))
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
        if (Physics.Raycast(pivotCamera.transform.position, pivotCamera.transform.forward, out hit, placementDistanceMax, haverstable))
        {
            inFrontOfMe = hit.transform.gameObject;
            if(Vector3.Distance(hit.transform.position, pivotCamera.transform.position) < placementDistanceMin)
            {
                placementPoint = Vector3.zero;
                canPlace = false;

            }else if(Vector3.Distance(hit.transform.position, pivotCamera.transform.position) < placementDistanceMax)
            {
                placementPoint = hit.point;
                canPlace = true;
            }else
            canPlace = false;               
        }

    }

    void CheckNewAnchorPoint()
    {
        if(anchorPositionListe.Count == 1 & isGrappin == false)
        {
            
            return;
        }
        for (int i = 0; i < anchorPositionListe.Count - 1 ; i++)
        {
            RaycastHit raycastHit;
            RaycastHit ray;
            anchorPositionListe[i].transform.LookAt(anchorPositionListe[i + 1].transform);
            Debug.DrawRay(anchorPositionListe[i].transform.position, anchorPositionListe[i].transform.eulerAngles);
            if (Physics.Raycast(anchorPositionListe[i].transform.position, anchorPositionListe[i].transform.forward, out raycastHit, 100f, fixePoint | haverstable))
            {
                //il a toucher quellque chose
                if(!(raycastHit.transform == anchorPositionListe[i + 1].transform) & anchorPositionListe.Count < 10)
                {
                    //sa a toucher un truc, mais c'est pas bon ... 
                    if(Vector3.Distance(raycastHit.point, anchorPositionListe[i].transform.position) < 0.1 | Vector3.Distance(raycastHit.point, anchorPositionListe[i +1 ].transform.position) < 0.1)
                    {
                    }
                    else
                    {
                        if(Physics.SphereCast(anchorPositionListe[i].transform.position, 0.2f, anchorPositionListe[i].transform.forward, out ray, 100f, fixePoint | haverstable))
                        {
                        StartCoroutine(createMiddleAchorPoint(ray, i));
                            //on crée un sphere cast pour que le point ne soit pas sur la surface

                        }

                    }
                }
                if(anchorPositionListe[i].transform.eulerAngles == anchorPositionListe[i + 1].transform.eulerAngles)
                {
                    //un point d'ancrage est devenu inutile
                    StartCoroutine(SuppresMiddleAnchorPoint(i));
                }
            }
        }
    }


    IEnumerator createMiddleAchorPoint(RaycastHit hit, int i)
    {
        yield return new WaitForFixedUpdate();
        anchorPositionListe.Insert(i +1 , Instantiate(anchorPoint, hit.transform));
        anchorPositionListe[i + 1].transform.position = hit.point;
        Debug.Log("creation d'un point d'ancrage");

    }
    IEnumerator SuppresMiddleAnchorPoint(int i)
    {
        yield return new WaitForFixedUpdate();
        anchorPositionListe.RemoveAt(i);
        Debug.Log("suppresion d'un point d'ancrage");

    }

    void jointManager()
    {
        if(!(player.TryGetComponent<SpringJoint>(out joint)))
        {
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            Debug.Log("1er joint crée");

        }

        joint.connectedAnchor = anchorPositionListe[anchorPositionListe.Count - 1].transform.position;

        float distanceFromPoint = Vector3.Distance(player.transform.position, anchorPositionListe[anchorPositionListe.Count - 1].transform.position);

        //la distance sur laquelle on vas rester du point d'ancrage

        joint.maxDistance = distanceFromPoint;
        joint.minDistance = 0f;

        //reglage de la corde
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;



    }
}
