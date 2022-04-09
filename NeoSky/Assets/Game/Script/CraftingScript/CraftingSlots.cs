using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CraftingSlots : MonoBehaviour
{
    public Animator myAnimator;
    public int myItemNumber;
    public ItemManager myItem;

    public int requiredItemNumber;
    public ItemManager requiredMyItem;

    public RawImage progresseBar;
    public RawImage itemSlots;
    public TextMeshProUGUI UINombreItem;
    public TextMeshProUGUI UIItemName;

    public GameObject progressionBar;
    public InventoryGrid inventoryGrid;
    public GameObject dropedItem;
    public GameObject player;

    public bool full = false;

    private void Start()
    {
        if (requiredItemNumber > 0)
        {
            progressionBar.transform.localScale = new Vector3(requiredItemNumber / myItemNumber, 1, 1);
        }
    }
    private void OnDisable()
    {
        DumpItem();
    }
    /// <summary>
    /// Permet l'ajout d'item dans un slots de craft
    /// </summary>
    /// <param name="nombre">le nombre d'item envoyer</param>
    /// <param name="itemManager">le nom de l'item</param>
    /// <returns> retourne le nombre d'item qu'il y a en trops</returns>
    public int AddItem(int nombre, ItemManager itemManager)
    {
        if (requiredMyItem == null)
        {
            //pas besion d'item pour se craft
            return nombre;
        }
        if (itemManager.name == requiredMyItem.name)
        {
            myItemNumber += nombre;
            if (myItemNumber > requiredItemNumber)
            {
                int difference = myItemNumber - requiredItemNumber;
                myItemNumber = requiredItemNumber;
                UpdateItemSlots();
                return difference;
            }
            UpdateItemSlots();
            return 0;
        }
        else
        {
            return nombre;
        }
    }
    public void UpdateItemSlots()
    {
        if (requiredMyItem == null)
        {
            //pas besion d'item : a = 0.5f
            progresseBar.color = new Color(progresseBar.color.r, progresseBar.color.g, progresseBar.color.b, 0.5f);
            itemSlots.color = new Color(itemSlots.color.r, itemSlots.color.g, itemSlots.color.b, 0.5f);
            UINombreItem.color = new Color(UINombreItem.color.r, UINombreItem.color.g, UINombreItem.color.b, 0.5f);
            UIItemName.color = new Color(UIItemName.color.r, UIItemName.color.g, UIItemName.color.b, 0.5f);
        }
        else
        {
            //need a item : a = 0.5f
            progresseBar.color = new Color(progresseBar.color.r, progresseBar.color.g, progresseBar.color.b, 1f);
            itemSlots.color = new Color(itemSlots.color.r, itemSlots.color.g, itemSlots.color.b, 1f);
            UINombreItem.color = new Color(UINombreItem.color.r, UINombreItem.color.g, UINombreItem.color.b, 1f);
            UIItemName.color = new Color(UIItemName.color.r, UIItemName.color.g, UIItemName.color.b, 1f);

            //information pour le joueur;
            if (requiredItemNumber > 0)
            {
                if(myItemNumber > 0)
                {
                    progressionBar.transform.localScale = new Vector3(requiredItemNumber / myItemNumber, 1, 1);
                }
                else
                {
                    progressionBar.transform.localScale = new Vector3(0, 1, 1);
                    UINombreItem.text = myItemNumber + "/" + requiredItemNumber;
                    UIItemName.text = myItem.name;
                }

            }
            else
            {
                progressionBar.transform.localScale = new Vector3(0, 1, 1);
                UINombreItem.text = null;
                UIItemName.text = null;
            }
            if (requiredItemNumber == myItemNumber)
            {
                full = true;
            }
            else
            {
                full = false;
            }
        }

    }
    /// <summary>
    /// change la case pour un autre craft et ou ne rien mettre dedans
    /// </summary>
    /// <param name="itemManager">le nouvelle item requit / mettre null pour effacer la case</param>
    /// <param name="nombreRequit">le nombre d'item requit</param>
    public void ChangeRequieredItem(ItemManager itemManager, int nombreRequit)
    {
        DumpItem();
        if(itemManager == null)
        {
            requiredItemNumber = 0;
            myItem = itemManager;

            requiredMyItem = null;
            UpdateItemSlots();
            return;
        }
        myItem = itemManager;
        requiredMyItem = itemManager;
        requiredItemNumber = nombreRequit;
        UpdateItemSlots();
        return;
    }
    /// <summary>
    /// enlève tout les items de la case ==> mettre dans l'inventaire et ou par terre
    /// </summary>
    public void DumpItem()
    {
        if(myItemNumber == 0)
        {
            return;
        }
        int nombreEnTrop = inventoryGrid.AddItemInInventory(myItem, myItemNumber);
        if (nombreEnTrop != 0)
        {
            for (int i = 0; i < nombreEnTrop; i++)
            {
                GameObject ram;
                ram = Instantiate(dropedItem);
                ram.GetComponent<Ressource>().itemManager = myItem;
                ram.transform.position = player.transform.position;
            }
        }
    }
    
}
