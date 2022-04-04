using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryCase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool onMe = false;
    public string myNameCase;
    public InventoryGrid myInventoryGrid;
    public ItemManager myItem;
    public RawImage itemIcon;
    public TextMeshProUGUI conteur;
    public int MyItemNumber;

    public IconFollow iconFollow;

    public Collider2D collider2;
    // Start is called before the first frame update
    public void resetCase()
    {
        myItem = null;
        itemIcon.texture = null;
    }
    public int RequestAddItem(ItemManager item, int nombre)
    {
        if (myItem == null) 
        {
            //la case est vide, du pas besion de 
            myItem = item;
            SetItemIcon(item);
            MyItemNumber = nombre;
            SetItemConteur(MyItemNumber);
            return 1;
        }
        if(myItem.name == item.name)
        {
            MyItemNumber = MyItemNumber + nombre;
            if(myItem.stackNumber >= MyItemNumber)
            {
                
                SetItemConteur(MyItemNumber);
                return 1;
            }
            int itemEnTrop = MyItemNumber - myItem.stackNumber;
            MyItemNumber = myItem.stackNumber;
            SetItemConteur(MyItemNumber);
            Debug.Log("nouveau stack");
            return itemEnTrop + 10; //renvoie le nombre d'item en trop + 10

        }
        return 0;
    }

    public void SourisCliqueUp()
    {
        Debug.Log(this.gameObject.name);
        myInventoryGrid.DropOnMe(this);
    }
    public void SourisCliqueDown()
    {
        
        if (myItem == null)
        {
            return;
        }
        Debug.Log(this.gameObject.name);
        myInventoryGrid.DragOnMe(this);

    }

    
    private void SetItemIcon(ItemManager item)
    {
        if(item == null)
        {
            return;
        }
        itemIcon.texture = item.icon;
    }
    private void SetItemConteur(int nombre)
    {
        MyItemNumber = nombre;
        conteur.text = MyItemNumber.ToString();
    }

    public void RequestItemErase()
    {
        myItem = null;
        MyItemNumber = 0;
        itemIcon.texture = null;
        conteur.text = null;

    }

    private void Update()
    {
        if (onMe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SourisCliqueDown();
            }
            if (Input.GetMouseButtonUp(0))
            {
                SourisCliqueUp();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse in");
        onMe = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        onMe = false;
    }

    public void ToggleOnFollowMouse()
    {
        iconFollow.gameObject.transform.SetSiblingIndex(1);
        iconFollow.enabled = true;       
    }
    public void ToggleOffFollowMouse()
    {
        iconFollow.enabled = false;
    }

}
