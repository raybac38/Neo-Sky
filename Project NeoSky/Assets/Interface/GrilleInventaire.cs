using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrilleInventaire : MonoBehaviour
{
    public string nomGrille;
    //a attacher se script sur le fond
    public bool keepInventory = false;
    public GameObject origine;
    public GameObject prefabBackground;
    public GameObject prefabItemForground;
    public float UIscale = 25;
    public Vector2Int taille;
    [SerializeField]
    public CaseInventory[,] caseArrray;
    [SerializeField]
    public int[,] positionItem;
    [SerializeField]
    List<Item> itemIndex = new List<Item>();
    public void Awake()
    {
        itemIndex.Clear();
        if (taille.x < 1 | taille.y < 1)
        {
            Debug.LogError("taille d'une grille d'inventaire invalide");
            taille = Vector2Int.one * 3;

        }
        caseArrray = new CaseInventory[taille.x, taille.y];
        positionItem = new int[taille.x, taille.y];
        for (int i = 0; i < positionItem.GetLength(0); i++)
        {
            for (int j = 0; j < positionItem.GetLength(1); j++)
            {
                positionItem[i, j] = -1;
            }
        }
        for (int x = 0; x < taille.x; x++)
        {
            for (int y = 0; y < taille.y; y++)
            {
                GameObject obj = Instantiate(prefabBackground);
                obj.transform.SetParent(origine.transform);
                obj.transform.localScale = Vector3.one * UIscale;
                obj.AddComponent(typeof(CaseInventory));
                CaseInventory caseInventory = obj.GetComponent<CaseInventory>();
                caseArrray[x, y] = caseInventory;
                caseInventory.index = new Vector2Int(x, y);
                caseInventory.grilleInventaire = this;
                obj.transform.localPosition = new Vector3(x * UIscale, UIscale * y, 0);

            }
        }
    }
    ///public bool RequestAddItem(Vector2Int tailleItem)
    ///{
    ///    if(tailleItem.x > taille.x | tailleItem.y > taille.y)
    ///    {
    ///        return false;
    ///        //item plus grand que l'inventaire xD
    ///    }
    ///    bool canPlace = true;
    ///
    ///    for (int x = 0; x < positionItem.GetLength(0) - (tailleItem.x - 1); x++)
    ///    {
    ///        for (int y = 0; y < positionItem.GetLength(1) - (tailleItem.y - 1); y++)
    ///        {
    ///            for (int i = 0; i < tailleItem.x; i++)
    ///            {
    ///                for (int j = 0; j < tailleItem.y; j++)
    ///                {
    ///                    if(positionItem[x+i,y+j] != null)
    ///                    {
    ///                        canPlace = false;
    ///                        break;
    ///                    }
    ///                    
    ///                }
    ///                if (!canPlace)
    ///                {
    ///                    break;
    ///                }
    ///            }
    ///            if (canPlace == true)
    ///            {
    ///                return true;
    ///            }
    ///
    ///        }
    ///    }
    ///    return false;
    ///}


    public void RequestAddItem(Item item)
    {
        
        item = FindStackablePlace(item);
        if(item == null)
        {
            Debug.Log("item stack qq part");
            //item stack qq part
        }
        else
        {
            item = FindNewPlace(item);
            if(item == null)
            {
                Debug.Log("nouveau stack de creer");
                //nouveau stack de creer
            }
            else
            {

            }
        }
        RefreshCaseInventoryDisplay();

    }
    
    /// <summary>
    /// permet de stacker automatiquement un item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public Item FindStackablePlace(Item item)
    {
        if (item.number == 0)
        {
            Debug.Log("annulation du stack, pas assez d'item");
            return item;
        }
        if (itemIndex.Count == 0)
        {
            Debug.Log("annulation du stack, pas d'endroit possible a stack");
            return item;
        }
        Debug.Log("recherche de stack possible");
        for (int i = 0; i < itemIndex.Count; i++)
        {
            if (itemIndex[i].data.itemName == item.data.itemName)
            {
                if (itemIndex[i].number < item.data.maxItem)
                {
                    Debug.Log("place trouver, re stack en cours");
                    //y'a de la place, youpi ^^
                    itemIndex[i].number += item.number;
                    item.number = 0;
                    if (itemIndex[i].number > item.data.maxItem)
                    {
                        int difference = itemIndex[i].number - item.data.maxItem;
                        item.number = difference;
                        itemIndex[i].number -= difference;
                    }
                }
            }
            if (item.number == 0) return null;
        }
        return item;
    }
    public Item FindNewPlace(Item item)
    {
        bool canPlace = true;
        for (int x = 0; x < positionItem.GetLength(0) - (item.data.dimention.x - 1); x++)
        {
            for (int y = 0; y < positionItem.GetLength(1) - (item.data.dimention.y - 1); y++)
            {
                canPlace = true;
                for (int i = 0; i < item.data.dimention.x; i++)
                {
                    for (int j = 0; j < item.data.dimention.y; j++)
                    {

                        if (positionItem[x + i, y + j] != -1)
                        {
                            canPlace = false;
                            break;
                        }



                    }
                    if (!canPlace) break;
                }
                if (canPlace == true)
                {
                    //placer l'item
                    int nouvelleIndex = itemIndex.Count;

                    Item item1 = ScriptableObject.CreateInstance<Item>();
                    item1.number = item.number;
                    item1.data = item.data;
                    if (item1.number > item1.data.maxItem)
                    {
                        int difference = item1.number - item1.data.maxItem;
                        item1.number -= difference;
                        item.number = difference;
                    }
                    else
                    {
                        item.number = 0;
                    }
                    itemIndex.Add(item1);

                    for (int i = 0; i < item.data.dimention.x; i++)
                    {
                        for (int j = 0; j < item.data.dimention.y; j++)
                        {
                            positionItem[x + i, y + j] = nouvelleIndex;
                        }
                    }

                    if (item.number == 0)
                    {
                        //si il n'y a plus d'item, sert a rien de chercher de la place
                        Debug.Log("stack de cree");
                        return null;
                    }
                }
            }
        }
        Debug.Log("plus de place");
        return item;
    }

    public void RefreshCaseInventoryDisplay()
    {
        Debug.Log("refresh effectuer");
        for (int i = 0; i < positionItem.GetLength(0); i++)
        {
            for (int j = 0; j < positionItem.GetLength(1); j++)
            {
                if(positionItem[i,j] == -1)
                {
                    caseArrray[i, j].caseUse = false;
                }
                else
                {
                    int index = positionItem[i, j];
                    caseArrray[i, j].caseUse = true;
                    caseArrray[i, j].rawImage.color = itemIndex[index].data.itemColor;
                }
            }
        }
        //mise en place des nombres pour savoir le compte des items

        //rendre le systeme plus optimiser pour eviter les coups de lags ^^

        bool texteMit = false;
        for (int i = 0; i < itemIndex.Count; i++)
        {
            texteMit = false;
            for (int x = 0; x < positionItem.GetLength(0); x++)
            {
                for (int y = 0; y < positionItem.GetLength(1); y++)
                {
                    if(i == positionItem[x, y])
                    {
                        //firt occurance
                        TextMeshProUGUI text;
                        GameObject obj;

                        if(caseArrray[x,y].textMesh == null)
                        {
                            obj = Instantiate(new GameObject("numero (clone)"), caseArrray[x, y].transform);
                            text = obj.AddComponent<TextMeshProUGUI>();
                            caseArrray[x, y].textMesh = text;
                        }
                        else
                        {
                            text = caseArrray[x, y].textMesh;
                        }
                        text.text = itemIndex[i].number.ToString();
                        text.fontSize = 0.7f;
                        text.rectTransform.sizeDelta = new Vector2(1, 1);
                        texteMit = true;
                        break;
                    }                    
                }
                if (texteMit) break;
            }
        }

    }

    public Item woodPrefab;
    public Item ironPrefab;
    public Item item2;
    public void AddWood()
    {
        item2 = ScriptableObject.CreateInstance<Item>();
        item2.number = woodPrefab.number;
        item2.data = woodPrefab.data;
        RequestAddItem(item2);
    }
    public void AddIron()
    {
        item2 = ScriptableObject.CreateInstance<Item>();

        item2.number = ironPrefab.number;
        item2.data = ironPrefab.data;
        RequestAddItem(item2);
    }

}


