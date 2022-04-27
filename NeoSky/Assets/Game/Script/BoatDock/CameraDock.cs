using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDock : MonoBehaviour
{
    public CameraManager cameraManager;

    public Camera dockCamera;
    public float distanceMax;
    public float distanceMin;
    public float sensibiliter;

    public GameObject pivot;
    private Vector2 mouseOldPostion;
    private Vector2 mouseNewPostion;
    public Vector3 angle;
    private void Awake()
    {
        this.enabled = false;
        dockCamera.enabled = false;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        mouseOldPostion = Input.mousePosition;
        transform.localPosition = new Vector3(0, 0, -1.5f);
    }
    public void Update()
    {
        mouseNewPostion = Input.mousePosition;
        transform.Translate(0, 0, Input.mouseScrollDelta.y);
        if (Input.GetMouseButton(1))
        {
            angle = new Vector3((mouseOldPostion.y - mouseNewPostion.y) * -1, (mouseOldPostion.x - mouseNewPostion.x) * -1, transform.rotation.z * -1);
            pivot.transform.localEulerAngles = angle;

        }
        mouseOldPostion = Input.mousePosition;
    }
    public void TakeControleOfMe()
    {
        
    }

}
