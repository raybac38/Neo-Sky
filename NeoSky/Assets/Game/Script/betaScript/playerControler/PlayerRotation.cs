using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Vector3 angleInput;
    private float sensibiliter = 1.5f;
    private float visioCap = 85f;
    public GameObject player;

    public GameObject cameraPivot;
    private void Update()
    {
        angleInput = new Vector3(Input.GetAxis("Mouse Y") * -1 * sensibiliter, Input.GetAxis("Mouse X") * sensibiliter, 0);
        
        float currentRotationX = cameraPivot.transform.localEulerAngles.x;
        float newRotationX = angleInput.x + currentRotationX;
        if ((newRotationX >= visioCap * -1 & newRotationX <= visioCap) |
            (newRotationX <= 444 & newRotationX >= 360 - visioCap) & newRotationX != currentRotationX)
        {
            cameraPivot.transform.localEulerAngles = new Vector3(newRotationX, 0, 0);
        }
        else if (275 > newRotationX & newRotationX > 180)
        {
            cameraPivot.transform.localEulerAngles = new Vector3(-visioCap, 0, 0);
        }
        else if (visioCap < newRotationX & newRotationX <= 180)
        {
            cameraPivot.transform.localEulerAngles = new Vector3(visioCap, 0, 0);
        }
        transform.Rotate(0 , angleInput.y, 0);

    }



}
