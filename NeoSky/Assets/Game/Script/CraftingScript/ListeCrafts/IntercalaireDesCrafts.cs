using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntercalaireDesCrafts : MonoBehaviour
{
    public Button playerIntercalaireButton;
    public Button craftingStationIntercalaireButton;

    public GameObject playerbutton;
    public GameObject craftingStationButton;

    public Image playerFond;
    public Image craftingStationFond;
    public int state = 0;
    public CraftContenant craftContenant;
    public bool unlockStationCraft = false;
    private void Awake()
    {
        ButtonPlayer();
        StartCoroutine(HideCraftingButton());

    }
    private void Start()
    {
        ButtonPlayer();
        unlockStationCraft = true;
        RefreshState();
        StartCoroutine(HideCraftingButton());
    }
    public void ButtonPlayer()
    {
        state = 0;
        RefreshState();
    }
    public void ButtonCraftingStation()
    {
        if (!unlockStationCraft)
        {
            return;
        }
        state = 1;
        RefreshState();
    }

    public void RefreshState()
    {
        if(state == 0)
        {
            playerFond.color = new Color(255, 255, 255, 0);
            craftingStationFond.color = new Color(255, 255, 255, 75);
            playerIntercalaireButton.enabled = false;
            craftingStationIntercalaireButton.enabled = true;
            craftContenant.ShowChange(1);

        }
        if(state == 1)
        {
            craftingStationFond.color = new Color(255, 255, 255, 0);
            playerFond.color = new Color(255, 255, 255, 75);
            craftingStationIntercalaireButton.enabled = false;
            playerIntercalaireButton.enabled = true;
            craftContenant.ShowChange(2);
        }
        StartCoroutine(HideCraftingButton());

    }
    IEnumerator HideCraftingButton()
    {
        yield return new WaitForEndOfFrame();
        if (unlockStationCraft)
        {
            craftingStationButton.SetActive(true);
        }
        else
        {
            craftingStationButton.SetActive(false);
        }
    }
}
