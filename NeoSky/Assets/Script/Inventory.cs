using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public float harvestRange = 20f;
    public int hotBarState = 0;
    public float interactionDistance = 4f;
    public Text hotBarIndicator;
    public Grapin grapin;
    public Transform objectDevantMoi;
    public bool canHarvest = true;
    public string interactableObject;
    public bool inInterface;
    private void Awake()
    {
        inInterface = false;
    }
    public void LeftClic()
    {
        if(hotBarState == 1)
        {
            Harvest();
        }
    }
    public void Harvest()
    {
        GameObject game = grapin.inFrontOfMe;
        if(game != null & canHarvest)
        {
            StartCoroutine(HarvesteTimer());
        }
    }
    private void Update()
    {
        Debug.Log("tt");
        UpdateHotBar();
        CheckInteraction();
    }
    public void UpdateHotBar()
    {
        int mouseDelta = (int)Input.mouseScrollDelta.y;
        if(mouseDelta != 0)
        {
            if(hotBarState + mouseDelta > 9)
            {
                hotBarState += mouseDelta - 10;
            }else if(hotBarState + mouseDelta < 0)
            {
                hotBarState += mouseDelta + 10;
            }
            else
            {
                hotBarState += mouseDelta;
            }
        }
    }
    IEnumerator HarvesteTimer()
    {
        canHarvest = false;
        yield return new WaitForSeconds(2);
        objectDevantMoi = null;
        canHarvest = true;
    }
    private void RequestAddObject()
    {
        Debug.Log("can i add some object ?");

    }
    void CheckInteraction()
    {
        GameObject gameObject = grapin.inFrontOfMe;
        if(gameObject == null)
        {
            hotBarIndicator.text = hotBarState.ToString();
            return;
        }
        else
        {
            Debug.Log(gameObject.GetComponent<ShipCrafting>());
            if(gameObject.GetComponent<ShipCrafting>() != null)
            {
                hotBarIndicator.text = "press e to use : ShipCraft";
                interactableObject = "ShipCraft";

            }else if(gameObject.GetComponent<ShipDoking>() != null)
            {
                hotBarIndicator.text = " press e to use : ShipDock";
                interactableObject = "ShipDoking";
            }
            else
            {
                interactableObject = null;
            }

        }
    }
    public void Interactable()
    {
        if (inInterface)
        {
            return;
        }
        if(interactableObject == "ShipCraft")
        {
            //shipcraft interface
        }else if(interactableObject == "ShipDock")
        {
            //shipdock interface
        }
        else
        {
            return;
        }
    }
}
