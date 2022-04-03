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
    }
    public void DropOnMe(InventoryCase invetoryCase)
    {
        dropPoint = invetoryCase;

        if(dragPoint == null | dropPoint == null | dragPoint == dropPoint)
        {
            dragPoint = null;
            dropPoint = null;
            return;
        }
        else
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
            dragPoint = null;
            dropPoint = null;

            return;
        }
    }

}
