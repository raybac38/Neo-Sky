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
    }

    public void InventoryScaleUpdater()
    {
        xScale = Mathf.Abs(borneInf.transform.position.x - borneSup.transform.position.x);
        yScale = Mathf.Abs(borneInf.transform.position.y - borneSup.transform.position.y);
    }

    public RequestAddItem(int largeur, int hauteur, int nombre)
    {
        //avancer sur le scripts pour les items

    }
}
