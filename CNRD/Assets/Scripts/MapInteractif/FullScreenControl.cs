using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenControl : MonoBehaviour
{
    public GameObject rubanFiction;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

}
