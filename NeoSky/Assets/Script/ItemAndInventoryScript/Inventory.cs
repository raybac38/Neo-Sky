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

    public Canvas inventoryObject;
    public GameObject UI;
    public Image viseurObject;

    [SerializeField]
    public GameObject[,] caseInventaire;
    public CeintureInventory ceintureInventory;

    public float time;

    IEnumerator coldown()
    {
        yield return new WaitForSeconds(2);
        canHarvest = true;
    }

    private void Awake()
    {
        inInterface = false;
        viseurObject.enabled = true;
        CloseInventory();
    }
    public void LeftClic()
    {
        if(hotBarState == 1 && canHarvest)
        {
            Harvest();
        }
    }
    public void Harvest()
    {

        GameObject game = grapin.inFrontOfMe;
        if(game == null)
        {
            return;
        }
        if (!canHarvest)
        {
            return;
        }
        else
        {
            canHarvest = false;
        }
        Ressource ressource;
        Debug.Log("harvers");
        if(game.TryGetComponent<Ressource>(out ressource))
        {
            ItemManager itemManager = ressource.itemManager;
            ceintureInventory.RequestAddItem(1, itemManager);
            Debug.Log("add " + ressource.name);
            StartCoroutine(coldown());
            objectDevantMoi = null;
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
    public void InteractionRequest()
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
    public void InventoryRequest()
    {
        if (inInterface)
        {
            CloseInventory();
            //quitter l'interface
        }
        else
        {
            inInterface = true;
            OpenInventory();
            //ouverture de l'inventaire 
        }
    }

    private void OpenInventory()
    {
        inventoryObject.enabled = true;
        viseurObject.enabled = false;
        UI.SetActive(true);

    }
    private void CloseInventory()
    {
        inInterface = false;
        inventoryObject.enabled = false;
        UI.SetActive(false);
        viseurObject.enabled = true;

    }
}
