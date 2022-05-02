using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinHock : MonoBehaviour
{
    //partie du script s'occupant du grappin;

    //zone des constantes
    private float grappinMaxLenght = 50f;
    private float grappinMinLenght = 0.25f;

    //variable

    public List<GameObject> anchorPoint = new List<GameObject>();
    private bool coolDown = false;
    private bool isGrappin = false;
    private float ropeDistance = 0;
    private bool destroyFrame = false;

    private bool alterne = false;

    //les depandances
    public PlayerMove playerMove;
    public GameObject anchorPointPrefab;
    public LayerMask isGrappinable;
    public Transform cameraPivot;
    public GameObject ropeStarter;


    //desactiver le script pour empecher l'utilisation du grappin
    private void Start()
    {
        isGrappin = false;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) & !isGrappin & !coolDown)
        {
            CreateFirtsPoint();
            StartCoroutine(CoolDownTimer());
        }
        else
        {
            if (Input.GetMouseButtonDown(1) & isGrappin & !coolDown)
            {
                StartCoroutine(EraseAllAnchorPoint());
                StartCoroutine(CoolDownTimer());

            }
        }
        if (isGrappin)
        {
            anchorPoint.RemoveAll(x => !x);
            destroyFrame = false;
            DetecteNewAnchorPoint();
            RemoveUselessAnchorPoint();
        }
    }
    IEnumerator EraseAllAnchorPoint()
    {
        destroyFrame = true;
        yield return new WaitForEndOfFrame();
        foreach (GameObject item in anchorPoint)
        {
            if (item.CompareTag("anchorPoint"))
            {
                Destroy(item);
            }
        }
        anchorPoint.Clear();
        isGrappin = false;

    }
    IEnumerator CoolDownTimer()
    {
        coolDown = true;
        yield return new WaitForSeconds(0.25f);
        coolDown = false;
    }
    private void CreateFirtsPoint()
    {

        GameObject newAnchorPoint = RequestRaycastForAnchorPoint(ropeStarter.transform.position, cameraPivot.forward, grappinMaxLenght, isGrappinable);
        anchorPoint.Add(ropeStarter);
        anchorPoint.Add(newAnchorPoint);
        isGrappin = true;
    }
    public void DetecteNewAnchorPoint()
    {
        List<GameObject> temporaryAnchorPoint = new List<GameObject>();
        temporaryAnchorPoint = anchorPoint;
        if (anchorPoint.Count < 1)
        {
            return;
        }

        int breakPoint = 0;
        for (int k = 0; k < anchorPoint.Count - 1; k++)
        {
            breakPoint++;
            if(breakPoint == 500)
            {
                Debug.LogError("break");
                break;
            }
            Vector3 direction = anchorPoint[k + 1].transform.position - anchorPoint[k].transform.position;
            //mettre

            RaycastHit hit;
            Debug.DrawRay(anchorPoint[k].transform.position, direction, Color.cyan, 0.5f);
            if (Physics.Raycast(anchorPoint[k].transform.position, direction, out hit, grappinMaxLenght - ropeDistance, isGrappinable))
            {
                if (!hit.transform.CompareTag("anchorPoint"))
                {// & Vector3.Distance(hit.point, anchorPoint[k+1].transform.position) > 0.5 & Vector3.Distance(hit.point, anchorPoint[k].transform.position) > 0.5
                    GameObject newAnchoirPoint = Instantiate(anchorPointPrefab, hit.transform);
                    newAnchoirPoint.transform.position = hit.point;
                    temporaryAnchorPoint.Insert(k + 1, newAnchoirPoint);
                }
            }
        }
        anchorPoint = temporaryAnchorPoint;
    }

    public void RemoveUselessAnchorPoint()
    {
        List<int> indiceAnchorPoint = new List<int>();
        if (anchorPoint.Count <= 2)
        {
            return;
        }
        for (int i = 0; i < anchorPoint.Count - 2; i++)
        {
            Vector3 pointC = new Vector3(0, 0, 0);
            Vector3 pointA = transform.TransformPoint(anchorPoint[i].transform.position);
            Vector3 pointB = transform.TransformPoint(anchorPoint[i + 1].transform.position);
            MeshFilter meshFilter = anchorPoint[i + 1].transform.parent.GetComponent<MeshFilter>();
            Mesh meshC = meshFilter.mesh;
            if (meshC == null)
            {
                Debug.LogError("Cmesh Missing");
                return;
            }
            Debug.Log(pointB);
            for (int o = 0; o < meshC.vertexCount; o++)
            {
                float distance = 99999f;
                Vector3[] vertexPosition = meshC.vertices;
                if (Vector3.Distance(pointB, vertexPosition[o]) < distance)
                {
                    distance = Vector3.Distance(pointB, vertexPosition[o]);
                    pointC = transform.TransformPoint(meshC.vertices[o]);
                }
            }
            //GameObject ram = Instantiate(anchorPointPrefab);
            //ram.transform.position = pointC;




            Vector3 normal;
            Vector3 side1 = pointB - pointA;
            Vector3 side2 = pointC - pointA;
            normal = Vector3.Cross(side1, side2);
            normal = Vector3.Normalize(normal);

            //normal ==> vecteur n du plan
            float d = ((normal.x * pointA.x) + (normal.y * pointA.y) + (normal.z * pointA.z)) * -1;
            Vector4 equationPlan = new Vector4(normal.x, normal.y, normal.z, d);
            Vector3 pointD = anchorPoint[i + 2].transform.position;
            float resultat = equationPlan.x * pointD.x + equationPlan.y * pointD.y * equationPlan.z * pointD.z + equationPlan.w;
            Debug.Log("resultat" + resultat);
            if (resultat > 0f)
            {
                indiceAnchorPoint.Add(i + 1);
                Debug.Log("supresse");
            }
        }
        for (int i = 0; i < indiceAnchorPoint.Count; i++)
        {
            Debug.Log("point n°" + indiceAnchorPoint[i]);
            Destroy(anchorPoint[indiceAnchorPoint.Count - i]);
        }
        indiceAnchorPoint.Clear();
        for (int i = 0; i < anchorPoint.Count; i++)
        {
            if(anchorPoint[i] == null)
            {
                GameObject gameObject = anchorPoint[i];
                anchorPoint.RemoveAt(i);
                anchorPoint.Remove(null);
                Destroy(gameObject);
            }
            anchorPoint.Remove(null);

        }
        anchorPoint.RemoveAll(x => !x);

    }

    private GameObject RequestRaycastForAnchorPoint(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layer)
    {
        RaycastHit hit;
        Debug.DrawRay(origin, direction);
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layer))
        {

            GameObject newAnchoirPoint = Instantiate(anchorPointPrefab, hit.transform);
            newAnchoirPoint.transform.position = hit.point;
            return newAnchoirPoint;
        }
        else
        {
            return null;
        }
    }

    // partie du script qui s'occupe de tracer la corde
    public LineRenderer lineRenderer;
    private void LateUpdate()
    {
        anchorPoint.RemoveAll(x => !x);

        DrawRope();
    }
    private void DrawRope()
    {

        if (destroyFrame) return;
        if (!isGrappin)
        {
            lineRenderer.positionCount = 0;
            return; 
        }
        lineRenderer.positionCount = 1 + anchorPoint.Count;
        lineRenderer.SetPosition(0, ropeStarter.transform.position);
        for (int i = 0; i < anchorPoint.Count; i++)
        {
            lineRenderer.SetPosition(i + 1, anchorPoint[i].transform.position);
        }
    }

}
