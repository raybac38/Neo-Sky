using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController characterController;

    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            characterController.SimpleMove(Vector3.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            characterController.SimpleMove(Vector3.back * moveSpeed);

        }
        if (Input.GetKey(KeyCode.Q))
        {
            characterController.SimpleMove(Vector3.left * moveSpeed);

        }
        if (Input.GetKey(KeyCode.D))
        {
            characterController.SimpleMove(Vector3.right * moveSpeed);

        }
    }
}
