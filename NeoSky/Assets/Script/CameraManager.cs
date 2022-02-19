using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cameraPlayer;
    public float state = 0;
    public GameObject CameraPivot;
    public float visioCap = 85;
    public float currentRotationX;
    public CameraPosition cameraPosition;
    

    private void Awake()
    {
        currentRotationX = transform.eulerAngles.x;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ToggleCameraState();
        }
        if(state == 1)
        {
            //tout les calcules pour savoir le placement de la camera en f5
            cameraPlayer.transform.LookAt(CameraPivot.transform.position);
        }
        
    }
    private void ToggleCameraState()
    {
        if(state == 1)
        {
            state = 0;
            cameraPlayer.transform.localPosition = new Vector3(0, 0, 0);
            CameraPivot.transform.localPosition = new Vector3(0, 1.1f, 0);
            cameraPlayer.transform.localEulerAngles = Vector3.zero;
            cameraPosition.enabled = false;
        }else if(state == 0)
        {
            state = 1;
            cameraPlayer.transform.localPosition = new Vector3(0f, 0, -6f);
            CameraPivot.transform.localPosition = new Vector3(0.9f, 1.1f, 0);
            cameraPosition.enabled = true;
        }
    }
    public void CameraUpdater(float rotationX)
    {
        currentRotationX = CameraPivot.transform.localEulerAngles.x;
        float newRotationX = rotationX + currentRotationX;
        if ((newRotationX >= visioCap * -1 & newRotationX <= visioCap) |
            (newRotationX <= 444 & newRotationX >= 360 - visioCap) & newRotationX != currentRotationX)
        {
            CameraPivot.transform.localEulerAngles = new Vector3(newRotationX, 0, 0);
        }
        else if (275 > newRotationX & newRotationX > 180)
        {
            CameraPivot.transform.localEulerAngles = new Vector3(-visioCap, 0, 0);
        }
        else if (visioCap < newRotationX & newRotationX <= 180)
        {
            CameraPivot.transform.localEulerAngles = new Vector3(visioCap, 0, 0);
        }
        else
        {

        }
    }
}
 