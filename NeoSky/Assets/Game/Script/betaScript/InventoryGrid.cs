using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour

{
    public InventoryCase dragPoint;
    public InventoryCase dropPoint;

    public InventoryCase[] cases; //deux ligne de 4 case

    public Inventory inventory;
    public Grapin grapin;
    // Start is called before the first frame update

    /// <summary>
    /// true = dans l'inventaire, false = inventory full
    /// </summary>
    /// <param name="item">ScripableObjectde l'item</param>
    /// <param name="nombre">Nombre d'item dans le stack (a ajouter)</param>
    /// <returns></returns>

    public ItemManager testeItem;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(SupressUslessDrag());
        }
        if (Input.GetMouseButtonUp(1))
        {
            StartCoroutine(SupressUslessDrag());
        }
    }
    IEnumerator SupressUslessDrag()
    {
        yield return new WaitForEndOfFrame();
        if(dragPoint != null)
        {
        dragPoint.ToggleOffFollowMouse();
        }
        dragPoint = null;
        dropPoint = null;
    }
    public void teste()
    {
        bool ram;

        ram = AddItemInInventory(testeItem, 1);
        if (!ram)
        {
            Debug.LogError("plus de place");
        }
    }
    public bool AddItemInInventory(ItemManager item, int nombre)
    {
        int valeurDeRetour;

        for (int i = 0; i < cases.Length; i++)
        {
            valeurDeRetour = cases[i].RequestAddItem(item, nombre);

            if (valeurDeRetour == 1)
            {
                Debug.Log("ajout d'un item");
                return true;
            }
            if(valeurDeRetour > 1)
            {
                Debug.Log("divise le stack");
                nombre = valeurDeRetour - 10;
            }

        }
        Debug.Log("plus de place");
        return false;
    }

    public void DragOnMeRightClic(InventoryCase inventoryCase)
    {
        dragPoint = inventoryCase;
        Debug.Log("drag on me in InventoryGrid");
        //faire en sorte que 1/2 du stack est drag
    }
    public void DropOnMeRightClic(InventoryCase inventoryCase)
    {
        dropPoint = inventoryCase;

        Debug.Log(dragPoint == null);
        Debug.Log(dropPoint == null);
        Debug.Log(dragPoint == dropPoint);


        if (dragPoint == null | dropPoint == null | dragPoint == dropPoint)
        {
            dragPoint = null;
            dropPoint = null;
            Debug.Log("cancel right clic because");
            return;
        }


        if(dropPoint.myItem == null)
        {
            ItemManager drop;
            ItemManager drag;
            int nombreDrag;
            int nombreDrop;
            drag = dragPoint.myItem;
            drop = dropPoint.myItem;
            nombreDrag = dragPoint.MyItemNumber;
            nombreDrop = Mathf.FloorToInt(nombreDrag / 2);
            nombreDrag -= nombreDrop;

            dragPoint.RequestItemErase();
            dropPoint.RequestItemErase();

            if (nombreDrag != 0)
            {
                dropPoint.RequestAddItem(drag, nombreDrag);
            }
            if (nombreDrop != 0)
            {
                dragPoint.RequestAddItem(drag, nombreDrop);
            }
            dragPoint.ToggleOffFollowMouse();
            dragPoint = null;
            dropPoint = null;
            return;
        }
        if (dragPoint.myItem.name != dropPoint.myItem.name)
        {
            dragPoint = null;
            dropPoint = null;
            Debug.Log("can't do anythings");
            return;
        }
        if (dragPoint.myItem.name == dropPoint.myItem.name)
        {
            Debug.Log("same");
            ItemManager drop;
            ItemManager drag;
            int nombreDrag;
            int nombreDrop;
            drag = dragPoint.myItem;
            drop = dropPoint.myItem;
            nombreDrag = dragPoint.MyItemNumber;
            nombreDrop = dropPoint.MyItemNumber;

            int ram;

            ram = Mathf.FloorToInt(nombreDrag / 2);
            nombreDrop += ram;
            if(nombreDrop > drop.stackNumber)
            {
                ram = nombreDrop - drop.stackNumber;
                nombreDrop = drop.stackNumber;
            }
            nombreDrag -= ram;          

            dragPoint.RequestItemErase();
            dropPoint.RequestItemErase();
            if (nombreDrag != 0)
            {
                dropPoint.RequestAddItem(drag, nombreDrag);
            }
            if (nombreDrop != 0)
            {
                dragPoint.RequestAddItem(drag, nombreDrop);
            }
            dragPoint.ToggleOffFollowMouse();
            dragPoint = null;
            dropPoint = null;
            return;
        }
        
    }

    //script de drag and drop d'item
    public void DragOnMeLeftClic(InventoryCase inventoryCase)
    {
        dragPoint = inventoryCase;
        dragPoint.ToggleOnFollowMouse();
    }
    public void DropOnMeLeftClic(InventoryCase invetoryCase)
    {
        dropPoint = invetoryCase;

        if(dragPoint == null | dropPoint == null | dragPoint == dropPoint)
        {
            if (dragPoint != null)
            {
                dragPoint.ToggleOffFollowMouse();
            }
            dragPoint = null;
            dropPoint = null;
            return;
        }
        
        else
        {
            if(dropPoint.myItem == null)
            {
                ItemSwamp();
                return;
            }
            //drapPoint name n'existe pas a se moment t
            //trouver pourquoi;
            Debug.Log(dragPoint.myItem.name);
            if(dragPoint.myItem.name != dropPoint.myItem.name)
            {
                ItemSwamp();
                return;
            }
            else
            {
                //stack l'item a l'autre stack
                ItemManager drag;
                drag = dragPoint.myItem; 
                int totalNombreItem;
                totalNombreItem = dropPoint.MyItemNumber + dragPoint.MyItemNumber;

                dragPoint.RequestItemErase();
                dropPoint.RequestItemErase();

                if(totalNombreItem <= drag.stackNumber)
                {
                    dropPoint.RequestAddItem(drag, totalNombreItem);
                }
                else
                {
                    dropPoint.RequestAddItem(drag, drag.stackNumber);
                    dragPoint.RequestAddItem(drag, totalNombreItem - drag.stackNumber);
                }

            }


            return;
        }
    }
    public void ItemSwamp()
    {
        ItemManager drop;
        ItemManager drag;
        int nombreDrop;
        int nombreDrag;
        drop = dropPoint.myItem;
        drag = dragPoint.myItem;
        nombreDrop = dropPoint.MyItemNumber;
        nombreDrag = dragPoint.MyItemNumber;

        dragPoint.RequestItemErase();
        dropPoint.RequestItemErase();

        if (nombreDrop != 0)
        {
            dragPoint.RequestAddItem(drag, nombreDrop);
        }
        if (nombreDrag != 0)
        {
            dropPoint.RequestAddItem(drag, nombreDrag);
        }
        dragPoint = null;
        dropPoint = null;
        return;
    }

}
