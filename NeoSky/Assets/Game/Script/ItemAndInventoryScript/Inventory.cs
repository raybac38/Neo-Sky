using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public PlayerMouvement playerMouvement;
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
    public Grapin grapinScript;
    public InventoryGrid inventoryGrid;
    public CraftManager craftManager;

    public IntercalaireDesCrafts IntercalaireDesCrafts;
    private CraftingStationControler craftingStation1;
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
        if(hotBarState == 2)
        {

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
            inventoryGrid.AddItemInInventory(itemManager, Random.Range(1, 3));
            Debug.Log("add " + itemManager.name);
            StartCoroutine(coldown());
            objectDevantMoi = null;
        }
    }
    private void Update()
    {
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
        if(gameObject == null)
        {
            hotBarIndicator.text = hotBarState.ToString();
            return;
        }
        else
        {
            CraftingStationControler craftingStation;
            if(gameObject.TryGetComponent<CraftingStationControler>(out craftingStation))
            {
                hotBarIndicator.text = "press e to use : Crafting Station";
                interactableObject = "CraftingStation";
                craftingStation1 = craftingStation;

            }else if(gameObject.GetComponent<ShipDoking>() != null)
            {
                hotBarIndicator.text = " press e to use : ShipDock";
                interactableObject = "ShipDoking";
            }
            else
            {
                interactableObject = null;
                craftingStation1 = null;
            }

        }
    }
    public void InteractionRequest()
    {
        if (inInterface)
        {
            return;
        }
        if(interactableObject == "CraftingStation")
        {
            //shipcraft interface
            IntercalaireDesCrafts.unlockStationCraft = true;
            craftManager.OpenInCraftingStation(craftingStation1);
            
            OpenInventory();

        }
        else if(interactableObject == "ShipDock")
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
            canHarvest = true;
            grapinScript.canUseGrappin = true;
            //quitter l'interface
        }
        else
        {
            IntercalaireDesCrafts.unlockStationCraft = false;

            OpenInventory();
            //ouverture de l'inventaire 
        }
    }

    private void OpenInventory()
    {
        inInterface = true;
        canHarvest = false;
        grapinScript.canUseGrappin = false;
        Cursor.lockState = CursorLockMode.None;
        inventoryObject.enabled = true;
        viseurObject.enabled = false;
        UI.SetActive(true);

    }
    private void CloseInventory()
    {
        IntercalaireDesCrafts.unlockStationCraft = false;
        Cursor.lockState = CursorLockMode.Locked;
        inInterface = false;
        inventoryObject.enabled = false;
        UI.SetActive(false);
        viseurObject.enabled = true;
        craftManager.DeselectionCraft();

    }

}
