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
    public bool ghostTouch = false;

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

    public void SourisLeftCliqueUp()
    {
        Debug.Log(this.gameObject.name);
        myInventoryGrid.DropOnMeLeftClic(this);
    }
    public void SourisLeftCliqueDown()
    {
        if (myItem == null)
        {
            return;
        }
        Debug.Log(this.gameObject.name);
        myInventoryGrid.DragOnMeLeftClic(this);

    }
    public void SourisRightCliqueUp()
    {
        Debug.Log(this.gameObject.name);
        myInventoryGrid.DropOnMeRightClic(this);
    }
    public void SourisRightCliqueDown()
    {

        if (myItem == null)
        {
            return;
        }
        Debug.Log(this.gameObject.name);
        myInventoryGrid.DragOnMeRightClic(this);

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
        if(Input.GetMouseButton(0) | Input.GetMouseButtonDown(1))
        {
            ghostTouch = true;
        }
        else
        {
            ghostTouch = false;
        }
        if (onMe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SourisLeftCliqueDown();
                ghostTouch = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                SourisLeftCliqueUp();
                ghostTouch = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                SourisRightCliqueDown();
                ghostTouch = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                SourisRightCliqueUp();
                ghostTouch = false;
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
        iconFollow.gameObject.transform.SetAsLastSibling();
        iconFollow.enabled = true;       
    }
    public void ToggleOffFollowMouse()
    {
        iconFollow.enabled = false;
    }

}
