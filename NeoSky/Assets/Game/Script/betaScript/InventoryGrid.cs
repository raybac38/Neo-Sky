using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour

{
    public InventoryCase dragPoint;
    public InventoryCase dropPoint;

    public InventoryCase[] cases; //deux ligne de 4 case
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
                return true;
            }
            if(valeurDeRetour < 2)
            {
                nombre = valeurDeRetour - 10;
            }

        }
        return false;
    }



    //script de drag and drop d'item
    public void DragOnMe(InventoryCase inventoryCase)
    {
        dragPoint = inventoryCase;
        dragPoint.ToggleOnFollowMouse();
    }
    public void DropOnMe(InventoryCase invetoryCase)
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
            if(dragPoint.myItem.name != dragPoint.myItem.name)
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

                dropPoint.RequestAddItem(drag, nombreDrag);
                dragPoint.RequestAddItem(drop, nombreDrop);
                dragPoint.ToggleOffFollowMouse();
                dragPoint = null;
                dropPoint = null;
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

}
