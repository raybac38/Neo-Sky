using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    public GrilleInventaire poche;
    public GrilleInventaire backpack;

    private Item itemDrag;
    private GrilleInventaire dragGrille;


    private GrilleInventaire dropGrille;
    private CaseInventory dropCase;

    /// <summary>
    /// declarer une action de drag
    /// </summary>
    /// <param name="item">l'item qui a etait drag</param>
    /// <param name="grille">quelle grille d'inventaire il appartient actuellement</param>
    public void DeclareDragPoint(Item item, GrilleInventaire grille)
    {
        itemDrag = item;
        dragGrille = grille;
    }
    public void DeclareDropPoint(GrilleInventaire grille, CaseInventory caseInventore)
    {
        dropGrille = grille;
        dropCase = caseInventore;

        RequestDragAndDropAction();
    }
    private void RequestDragAndDropAction()
    {
        if(itemDrag == null | dragGrille == null | dropGrille == null | dropCase == null)
        {
            itemDrag = null;
            dragGrille = null;
            dropGrille = null;
            dropCase = null;
            return;
        }

        //check si il y a de la place a l'endroit voulu

        bool move = dropGrille.RequestPlaceItem(dropCase, itemDrag);
        if (move)
        {
            dragGrille.DeleteItem(itemDrag);
        }

    }

}
