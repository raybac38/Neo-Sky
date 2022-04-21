using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrappinHock : MonoBehaviour
{
    //zone des constantes
    private float grappinMaxLenght = 50f;
    private float grappinMinLenght = 0.25f;

    //variable
    
    private List<GameObject> anchorPoint = new List<GameObject>();
    private bool isGrappin =false;
    private float ropeDistance = 0;

    //les depandances
    public PlayerMove playerMove;
    public GameObject anchorPointPrefab;
    public LayerMask isGrappinable;
    public Transform cameraPivot;

    //desactiver le script pour empecher l'utilisation du grappin
    private void Start()
    {
        isGrappin = false;
        if(anchorPoint.Count == 0)
        {
            anchorPoint.Add(Instantiate(anchorPointPrefab, this.transform));
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(1) & !isGrappin)
        {
            CreateFirtsPoint();
        }
        if (isGrappin)
        {
            DetecteNewAnchorPoint();
        }
    }
    private void CreateFirtsPoint()
    {
        GameObject newAnchorPoint = RequestRaycastForAnchorPoint(anchorPoint[0].transform.position, cameraPivot.forward, grappinMaxLenght, isGrappinable);
        if(newAnchorPoint == null)
        {
            return;
        }
        else
        {
            anchorPoint.Add(newAnchorPoint);
            isGrappin = true;
        }
    }
    public void DetecteNewAnchorPoint()
    {
        if(anchorPoint.Count < 1)
        {
            return;
        }
        for (int i = 0; i < anchorPoint.Count - 1; i++)
        {
            Vector3 direction = anchorPoint[i + 1].transform.position - anchorPoint[i].transform.position;
            //mettre
        }
    }
    private GameObject RequestRaycastForAnchorPoint(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layer)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layer )){

            GameObject newAnchoirPoint = Instantiate(anchorPointPrefab, hit.transform);
            newAnchoirPoint.transform.position = hit.point;
            return newAnchoirPoint;
        }
        else
        {
            return null;
        }
    }
}
