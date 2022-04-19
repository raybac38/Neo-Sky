using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCraft : MonoBehaviour
{
    public CameraDock cameraDock;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            cameraDock.enabled = true;
            cameraDock.TakeControleOfMe();
        }
    }
}
