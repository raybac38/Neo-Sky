using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    private Vector3 actualMousePosition = Vector3.zero;
    public float moveSpeed;
    public GameObject cameraPivot;
    private float sensibility = 0.5f;
    public GameObject MainCamera;
    private float mouseSensitivity = 1.3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            rb.MovePosition(transform.forward * moveSpeed + transform.position);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.forward * -1 *  moveSpeed + transform.position);

        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.MovePosition(transform.right * -1 * moveSpeed + transform.position);

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.right * moveSpeed + transform.position);

        }
        if(true)
        {
            cameraPivot.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -1 * mouseSensitivity, 0,0 ) * sensibility);
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0) * sensibility);
        }
        MainCamera.transform.rotation.SetEulerAngles(new Vector3(MainCamera.transform.rotation.x, MainCamera.transform.rotation.y, 0)) ;
    }
}
