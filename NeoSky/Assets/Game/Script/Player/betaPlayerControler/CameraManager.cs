using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //a attacher sur a camera directement
    public GameObject cameraPivot;
    public float state = 0;

    private bool isThirdPerson;

    private Camera cameraUse;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ToggleCameraState();
        }
        if(isThirdPerson)
        {
            transform.LookAt(cameraPivot.transform.position);
        }
        
    }
    private void ToggleCameraState()
    {
        if (isThirdPerson)
        {
            //passage a la premiere personne
            transform.localPosition = new Vector3(0, 0, 0);
            isThirdPerson = false;
        }
        else
        {
            //passage a la troisieme personne
            transform.localPosition = new Vector3(0, 0, -1.5f);
            isThirdPerson = true;
        }
    }
    
}
 