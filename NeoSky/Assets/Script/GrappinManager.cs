using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappinManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> anchorPoint = new List<GameObject>();
    public GameObject ropeStarter;
    public GameObject anchor;
    public bool isGrappin;

    public LayerMask Grappin;

    // l'ellement n°0 est la ou l'on se situe
    private void Awake()
    {
        anchorPoint.Add(ropeStarter);
        isGrappin = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrappin)
        {

        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {

            }
        }
    }

    void StartGrappin()
    {

    }
}
