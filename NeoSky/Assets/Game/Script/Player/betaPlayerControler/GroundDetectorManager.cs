using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetectorManager : MonoBehaviour
{
    // Detection du sol

    private BoxCollider box;
    public LayerMask canBeWalkOn;
    private float sphereRadius = 0.25f;
    private float sphereCastLongeure = 1.5f;
    public GameObject playerGameObject;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    public PlayerMove playerMove;
    private void OnTriggerEnter(Collider box)
    {
        playerMove.GroundedState(true);
    }
    private void OnTriggerExit(Collider box)
    {
        playerMove.GroundedState(false);
        playerGameObject.transform.parent = null;
    }
    private void OnTriggerStay(Collider box)
    {
        RaycastHit hit;
        playerMove.GroundedState(true);
        if (Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out hit, sphereCastLongeure, canBeWalkOn))
        {
            Transform parents = hit.collider.transform;
            playerGameObject.transform.SetParent(parents);
        }
    }
}
