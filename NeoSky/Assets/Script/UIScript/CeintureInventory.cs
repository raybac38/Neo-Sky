using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeintureInventory : MonoBehaviour
{
    public GameObject borneInf;
    public GameObject borneSup;
    public int[,] itemNumber;

    public float xScale;
    public float yScale;

    public Vector2Int dimmensionDuDammier = new Vector2Int(9, 3);

    public List<ItemManager> nomItem = new List<ItemManager>();

    private void Awake()
    {
        itemNumber = new int[dimmensionDuDammier.x, dimmensionDuDammier.y];
        //itemNumber prend la valeur de -1 quand il n'y a pas d'item
    }

    public void InventoryScaleUpdater()
    {
        xScale = Mathf.Abs(borneInf.transform.position.x - borneSup.transform.position.x);
        yScale = Mathf.Abs(borneInf.transform.position.y - borneSup.transform.position.y);
    }

    public RequestAddItem(int largeur, int hauteur, int nombre)
    {
        //rechercher tout les endroits disponible pour l'item. on prend comme point de reference, le bord en haut a gauche.
        for (int i = 0; i < dimmensionDuDammier.y - (hauteur - 1); i++)
        {
            // recherche pour tout y
            for (int j = 0; j < dimmensionDuDammier.x - (largeur - 1); j++)
            {
                //recherche pour tout les x
                if(itemNumber[j,i] == -1)
                {
                    //si la place est libre
                    //recherche si les places aux alantour 
                    for (int o = 0; o < hauteur; o++)
                    {
                        for (int k = 0; k < largeur; k++)
                        {

                        }
                    }
                }
            }
        }
    }
}
