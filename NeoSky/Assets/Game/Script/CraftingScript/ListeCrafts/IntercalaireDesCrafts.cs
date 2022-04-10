using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntercalaireDesCrafts : MonoBehaviour
{
    public Button playerIntercalaireButton;
    public Button craftingStationIntercalaireButton;

    public Image playerFond;
    public Image craftingStationFond;
    public int state = 0;
    private void Awake()
    {
        ButtonPlayer();
    }
    private void Start()
    {
        ButtonPlayer();
    }
    public void ButtonPlayer()
    {
        state = 0;
        RefreshState();
    }
    public void ButtonCraftingStation()
    {
        state = 1;
        RefreshState();
    }
    public void RefreshState()
    {
        if(state == 0)
        {
            playerFond.color = new Color(255, 255, 255, 0);
            craftingStationFond.color = new Color(255, 255, 255, 50);
            playerIntercalaireButton.enabled = false;
            craftingStationIntercalaireButton.enabled = true;
        }
        if(state == 1)
        {
            craftingStationFond.color = new Color(255, 255, 255, 0);
            playerFond.color = new Color(255, 255, 255, 50);
            craftingStationIntercalaireButton.enabled = false;
            playerIntercalaireButton.enabled = true;
        }
    }
}
