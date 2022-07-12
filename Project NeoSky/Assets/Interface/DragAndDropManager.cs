using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    public GrilleInventaire poche;
    public GrilleInventaire backpack;

    private Item itemDrag;
    private GrilleInventaire dragGrille;

    private bool defaultRotation;

    private GrilleInventaire dropGrille;
    private CaseInventory dropCase;
    private CaseInventory dragCase;

    

    /// <summary>
    /// declarer une action de drag
    /// </summary>
    /// <param name="item">l'item qui a etait drag</param>
    /// <param name="grille">quelle grille d'inventaire il appartient actuellement</param>
    public void DeclareDragPoint(Item item, GrilleInventaire grille, CaseInventory caseInventory)
    {
        dragCase = caseInventory;
        itemDrag = item;
        defaultRotation = itemDrag.isRotate;
        dragGrille = grille;
    }
    public void DeclareDropPoint(GrilleInventaire grille, CaseInventory caseInventore)
    {
        dropGrille = grille;
        dropCase = caseInventore;

        RequestDragAndDropAction();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //faire pivoter 
            if (dragCase != null)
            {
                itemDrag.isRotate = !itemDrag.isRotate;
                dragCase.RotateItem();
            }
        }
    }

    private void RequestDragAndDropAction()
    {
        if (itemDrag == null | dragGrille == null | dropGrille == null | dropCase == null)
        {
            itemDrag.isRotate = defaultRotation;
            itemDrag = null;
            dragGrille = null;
            dropGrille = null;
            dropCase = null;
            dragCase = null;

            return;
        }

        if (dragGrille == dropGrille)
        {
            Vector2Int drag = dragCase.index;
            Vector2Int drop = dropCase.index;

            if (dragGrille.positionItem[drag.x, drag.y] == dragGrille.positionItem[drop.x, drop.y])
            {
                itemDrag.isRotate = defaultRotation;

                itemDrag = null;
                dragGrille = null;
                dropGrille = null;
                dropCase = null;
                dragCase = null;
                return;
            }
        }
        //check si il y a de la place a l'endroit voulu
        Item item1 = ScriptableObject.CreateInstance<Item>();
        item1.number = itemDrag.number;
        item1.data = itemDrag.data;
        item1.isRotate = itemDrag.isRotate;
        item1.ID = Random.Range(0, 10000000);
        if (dropCase.grilleInventaire.positionItem[dropCase.index.x, dropCase.index.y] == -1) //la case drop est vide
        {
            Debug.Log("demande de deplacement");
            //l'item doit etre déplacer
            if (dropCase.grilleInventaire.PlaceItem(dropCase, item1))
            {
                dragGrille.DeleteItem(itemDrag);
                dropCase.grilleInventaire.RefreshCaseInventoryDisplay();
                dragGrille.RefreshCaseInventoryDisplay();
            }
            else
            {
                itemDrag.isRotate = defaultRotation;
            }
        } //la case drop a qq chose
        else if (dropCase.grilleInventaire.itemIndex[dropCase.grilleInventaire.positionItem[dropCase.index.x, dropCase.index.y]].data == item1.data)
        {
            Debug.Log("demande de stack");
            //peu stack les deux
            item1 = dropCase.grilleInventaire.StackItem(dropCase, item1);
            itemDrag.number = item1.number;
            if (item1.number == 0)
            {
                dragGrille.DeleteItem(itemDrag);
            }
            dropCase.grilleInventaire.RefreshCaseInventoryDisplay();
            dragGrille.RefreshCaseInventoryDisplay();

        }
        itemDrag = null;
        dragGrille = null;
        dropGrille = null;
        dropCase = null;
        dragCase = null;
    }
    //partie craft du code


    public CraftManager craftPersonelle;
    public CraftManager craftMulti;

    public void DropCraftCase(CraftCase craftCase)
    {
        if(itemDrag == null | dragCase == null | dragGrille == null | craftCase == null)
        {
            Debug.Log("et bein non");
            itemDrag = null;
            dragCase = null;
            dragGrille = null;
            return;
        }
        else
        {
            RequestDragAndDropCraft(craftCase);
            itemDrag = null;
            dragCase = null;
            dragGrille = null;
        }
    }
    public void RequestDragAndDropCraft(CraftCase craft)
    {
        Debug.Log(itemDrag.data.itemName);
        itemDrag = craftPersonelle.AddItem(craft, itemDrag);
        if(itemDrag.number == 0)
        {
            dragGrille.DeleteItem(itemDrag);
        }
        dragGrille.RefreshCaseInventoryDisplay();
    }

    public void RemoveItemFromCraft(CraftCase craft)
    {
        craftPersonelle.DumpOneCraftCase(craft);
        //enlever les items de la case de craft quand elle est cliquer desus
    }

}
