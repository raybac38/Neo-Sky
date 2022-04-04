using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// gere la position de la camera pour eviter qu'elle ne rentre dans un mur
/// se script est a poser SUR le pivot de la camera
/// </summary>
public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    public GameObject cameraPivot;
    public GameObject inverseCameraPivotRotation;
    public Camera cameraPlayer;
    private float distanceBehind;
    private float distanceMax = -6f;
    public LayerMask CameraCantPass;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("activation de la camera f5");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Debug.DrawRay(inverseCameraPivotRotation.transform.position, inverseCameraPivotRotation.transform.forward * -1, color: Color.black, 10f);
        if(Physics.Raycast(inverseCameraPivotRotation.transform.position, inverseCameraPivotRotation.transform.forward * -1, out hit,Mathf.Abs(distanceMax), CameraCantPass )){
            distanceBehind = Vector3.Distance(cameraPivot.transform.position, hit.point);
            if(distanceBehind <= Mathf.Abs(distanceMax))
            {
                cameraPlayer.transform.localPosition = new Vector3(0, 0, -1 * distanceBehind);
            }
            else
            {
                cameraPlayer.transform.localPosition = new Vector3(0, 0, distanceMax);
            }
        }
        else
        {
            cameraPlayer.transform.localPosition = new Vector3(0, 0, distanceMax);

        }
    }
}
