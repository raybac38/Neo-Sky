using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CeintureInventory : MonoBehaviour
{
    public GameObject borneInf;
    public GameObject borneSup;
    public int[,] itemNumber;

    public float xScale;
    public float yScale;
    public Vector2 scale;

    public Vector2Int dimmensionDuDammier = new Vector2Int(9, 3);

    public List<Item> nomItem = new List<Item>();

    public GameObject itemObject;
    public GameObject inventory;
    public GameObject prefabImage;

    public int[] ligne1 = new int[9];
    public int[] ligne2 = new int[9];
    public int[] ligne3 = new int[9];

    
    private void Update()
    {
        refershData();
    }

    private void refershData()
    {
        for (int i = 0; i < 9; i++)
        {
            ligne1[i] = itemNumber[i, 0];
            ligne1[i] = itemNumber[i, 1];
            ligne1[i] = itemNumber[i, 2];
        }
    }
    private void Awake()
    {
        itemNumber = new int[dimmensionDuDammier.x, dimmensionDuDammier.y];
        //itemNumber prend la valeur de -1 quand il n'y a pas d'item
        int nombre = dimmensionDuDammier.x * dimmensionDuDammier.y;
        int y = 0;
        int x = 0;
        Debug.Log(nombre);
        for (int w = 0; w < nombre; w++)
        {
            if(x == 9)
            {
                x = 0;
                y++;
            }
            x++;
            itemNumber[x - 1, y] = -1;
            Debug.Log("x et y " + x.ToString() + y.ToString());
        }
        Debug.Log(itemNumber[2, 2]);
        refershData();
        InventoryScaleUpdater();
    }
    public void InventoryScaleUpdater()
    {
        xScale = Mathf.Abs(borneInf.transform.position.x - borneSup.transform.position.x);
        yScale = Mathf.Abs(borneInf.transform.position.y - borneSup.transform.position.y);
        scale = new Vector2(xScale, yScale);
    }

    public bool RequestAddItem(int nombre, ItemManager itemManager)
    {
        int hauteur = itemManager.itemSize.y;
        int largeur = itemManager.itemSize.x;

        if(hauteur < 1)
        {
            Debug.LogError("trop petit");
        }
        if (largeur < 1)
        {
            Debug.LogError("trop petit");
        }
        //rechercher tout les endroits disponible pour l'item. on prend comme point de reference, le bord en haut a gauche.
        bool canPlace = false;
        Vector2Int positionPlace = new Vector2Int(0, 0);


        Debug.Log("check hauteur de " + (dimmensionDuDammier.y - (hauteur -1 )).ToString());
        Debug.Log("check largeur de " + (dimmensionDuDammier.x - (largeur -1)).ToString());
        for (int i = 0; i < dimmensionDuDammier.y - (hauteur -1); i++)
        {
            
            // recherche pour tout y
            for (int j = 0; j < dimmensionDuDammier.x - (largeur -1); j++)
            {
                //recherche pour tout les x
                Debug.Log(new Vector2(j, i).ToString());
                if(itemNumber[j,i] == -1)
                {
                    canPlace = true;
                    //si la place est libre
                    //recherche si les places aux alantour 
                    for (int o = 0; o < hauteur; o++) //pout tout les y que contient l'item
                    {
                        for (int k = 0; k < largeur; k++) //pour tout les x que contient l'item
                        {
                            if(itemNumber[j + k, i + o] != -1)
                            {
                                canPlace = false; //si il y a UNE place impossible, skip
                            }
                        }
                    }
                    if(canPlace == true)
                    {
                        positionPlace = new Vector2Int(j, i);
                        Debug.Log("item peu rentrer dans l'inventaire");
                        PlaceItem(positionPlace, nombre, itemManager);
                        return true;
                    }
                }
            }
        }
        if(canPlace == false)
        {
            Debug.Log("pas de place dans l'inventaire");
            return false;
        }
        else
        {
            Debug.LogError("item ni placée, ni full");
            return false;
            
        }
    }
    
    
    // fonctionnement de l'inventaire :
    //
    //chanque item est stocker dans la liste : l'item en question est stocker grace a son script ItemManager
    //Son indice permet de savoir ou se situe sur la grille qui est l'array itemNumber

    public void PlaceItem(Vector2Int position, int nombreItem, ItemManager itemManager)
    {
        Debug.Log("item placer aux coordonner " + position.ToString());
        int hauteur = itemManager.itemSize.y;
        int largeur = itemManager.itemSize.x;
        // met dans itemManager le nb des items

        GameObject game = Instantiate(itemObject);
        game.transform.SetParent(inventory.transform);
        Item item = game.AddComponent<Item>();
            
        nomItem.Add(item);
        int indice = nomItem.Count - 1;
        for (int i = position.y; i < hauteur; i++)
        {
            for (int k = position.x; k < largeur; k++)
            {
                if(itemNumber[k,i] == -1)
                {
                    Debug.Log("remplacement d'un truc vraie");
                }
                else
                {
                    Debug.Log("remplacement d'un truc faux");
                }
                itemNumber[k, i] = indice;
                
            }
        }
        Debug.Log(indice);
        nomItem[indice].quantiter = nombreItem;
        Debug.Log("ici");
        Debug.Log(" ajout de" + itemManager.ToString() + "en " + nomItem[indice].quantiter.ToString());



    }
    public void CreateGameObjectItem(ItemManager itemManager)
    {
    }





}


