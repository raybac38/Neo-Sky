using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUI : MonoBehaviour
{
    public bool cursorLock = false;
    public PlayerMouvement playerMouvement;
    public float mouseScrolleCount = 0;
    public int hotBarCase;
    public GameObject hotBar;

    public GameObject selector;
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
        HotBarScrolling();

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

    void HotBarScrolling()
    {
        mouseScrolleCount += Input.mouseScrollDelta.y;
        hotBarCase = (int)mouseScrolleCount % 10;
        selector.transform.localPosition = new Vector3(-238 + (49 * Mathf.Sqrt(hotBarCase*hotBarCase)), 36, 0);

    }
}
