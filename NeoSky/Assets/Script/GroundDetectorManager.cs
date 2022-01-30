using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetectorManager : MonoBehaviour
{
    // Detection du sol

    public BoxCollider box;

    public PlayerMouvement playerMouvement;

    private void OnTriggerEnter(Collider box)
    {
        playerMouvement.isGrounded = true;
    }
    private void OnTriggerExit(Collider box)
    {
        playerMouvement.isGrounded = false;
    }
    private void OnTriggerStay(Collider box)
    {
        playerMouvement.isGrounded = true;
    }

}
