using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    // Gere l'overlay du joueur en plein jeu
    private int hotbarState = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHotbarState();
    }

    private void UpdateHotbarState()
    {
        int mouseDelta = (int)Input.mouseScrollDelta.y;
        if (mouseDelta != 0)
        {
            if (hotbarState + mouseDelta > 9)
            {
                hotbarState += mouseDelta - 10;
            }
            else if (hotbarState + mouseDelta < 0)
            {
                hotbarState += mouseDelta + 10;
            }
            else
            {
                hotbarState += mouseDelta;
            }
        }
    }
}
