using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetectorManager : MonoBehaviour
{
    // Detection du sol

    public BoxCollider box;

    public PlayerMouvement playerMouvement;
    private RaycastHit hit;
    public Transform parents;
    public LayerMask layerMask;
    public float sphereRadius = 0.25f;
    public float sphereCastLongeure = 1.5f;
    public GameObject player;

    private void OnTriggerEnter(Collider box)
    {
        playerMouvement.isGrounded = true;
    }
    private void OnTriggerExit(Collider box)
    {
        playerMouvement.isGrounded = false;
        player.transform.parent = null;
    }
    private void OnTriggerStay(Collider box)
    {
        playerMouvement.isGrounded = true;
        if(Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out hit, sphereCastLongeure, layerMask))
        {
            parents = hit.collider.transform;
            player.transform.SetParent(parents);
        }
    }
}
