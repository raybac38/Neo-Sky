using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUI : MonoBehaviour
{
    public bool cursorLock = false;
    public PlayerMouvement playerMouvement;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cursorLock = true;
        playerMouvement.canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCursorMode();
        }
    }
    void ToggleCursorMode()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
            cursorLock = false;
            playerMouvement.canRotate = false;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLock = true;
            playerMouvement.canRotate = true;

        }
    }
}
