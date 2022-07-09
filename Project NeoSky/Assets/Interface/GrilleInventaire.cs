using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public DragAndDropManager dragAndDropManager;
    [SerializeField]
    public List<Item> itemIndex = new List<Item>();

    public void Awake()
    {
        itemIndex.Clear();
        if (taille.x < 1 | taille.y < 1)
        {
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
                caseInventory.dragAndDropManager = dragAndDropManager;
                caseInventory.grilleInventaire = this;
                obj.transform.localPosition = new Vector3(x * UIscale, UIscale * y, 0);

            }
        }
    }
    public Item RequestAddItem(Item item)
    {
        item = FindStackablePlace(item);
        if (item == null)
        {
            //item stack qq part
        }
        else if (item.number == 0)
        {
            return null;
        }
        else
        {
            item = FindNewPlace(item);
            if (item == null)
            {
                //nouveau stack de creer
            }
            //sinon plus de place
        }
        RefreshCaseInventoryDisplay();
        if (item == null)
        {
            return null;
        }
        else
        {
            return item;
        }

    }

    /// <summary>
    /// permet de stacker automatiquement un item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public Item FindStackablePlace(Item item)
    {
        if (item == null) return item;
        if (item.number == 0)
        {
            return item;
        }
        if (itemIndex.Count == 0)
        {
            return item;
        }
        for (int i = 0; i < itemIndex.Count; i++)
        {
            if (itemIndex[i].data.itemName == item.data.itemName)
            {
                if (itemIndex[i].number < item.data.maxItem)
                {
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
        Vector2Int dimention = item.data.dimention;
        if (item.isRotate)
        {
            dimention = new Vector2Int(dimention.y, dimention.x);
        }
        bool canPlace = true;
        for (int x = 0; x < positionItem.GetLength(0) - (dimention.x - 1); x++)
        {
            for (int y = 0; y < positionItem.GetLength(1) - (dimention.y - 1); y++)
            {
                canPlace = true;
                for (int i = 0; i < dimention.x; i++)
                {
                    for (int j = 0; j < dimention.y; j++)
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

                    Item item1 = ScriptableObject.CreateInstance<Item>();
                    item1.number = item.number;
                    item1.data = item.data;
                    itemIndex.Add(item1);
                    int nouvelleIndex = itemIndex.IndexOf(item1);
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

                    for (int i = 0; i < dimention.x; i++)
                    {
                        for (int j = 0; j < dimention.y; j++)
                        {

                            positionItem[x + i, y + j] = nouvelleIndex;
                        }
                    }

                    if (item.number == 0)
                    {
                        //si il n'y a plus d'item, sert a rien de chercher de la place
                        return null;
                    }
                }
            }
        }
        return item;
    }

    public void RefreshCaseInventoryDisplay()
    {
        for (int i = 0; i < positionItem.GetLength(0); i++)
        {
            for (int j = 0; j < positionItem.GetLength(1); j++)
            {
                if (positionItem[i, j] == -1)
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
            if (itemIndex[i] == null)
            {

            }
            else
            {
                texteMit = false;
                for (int x = 0; x < positionItem.GetLength(0); x++)
                {
                    for (int y = 0; y < positionItem.GetLength(1); y++)
                    {
                        if (i == positionItem[x, y])
                        {
                            //firt occurance
                            TextMeshProUGUI text;
                            GameObject obj;
                            if (caseArrray[x, y].textMesh == null)
                            {

                                obj = new GameObject("numero (clone)");
                                obj.transform.SetParent(caseArrray[x, y].gameObject.transform, false);
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

    }

    public Item woodPrefab;
    public Item ironPrefab;
    public Item item2;
    public void AddWood()
    {
        item2 = ScriptableObject.CreateInstance<Item>();
        item2.number = woodPrefab.number;
        item2.data = woodPrefab.data;
        item2.ID = Random.Range(0, 10000000);
        RequestAddItem(item2);
    }
    public void AddIron()
    {
        item2 = ScriptableObject.CreateInstance<Item>();

        item2.number = ironPrefab.number;
        item2.data = ironPrefab.data;
        item2.ID = Random.Range(0, 10000000);

        RequestAddItem(item2);
    }

    public bool RequestPlaceItem(CaseInventory caseInventory, Item item)
    {
        Vector2Int position = caseInventory.index;
        Vector2Int dimention = item.data.dimention;
        if (item.isRotate)
        {
            dimention = new Vector2Int(dimention.y, dimention.x);
        }
        if (taille.x < position.x + (dimention.x - 1) | taille.y < position.y - (dimention.y - 1) | position.y - (dimention.y - 1) < 0 | position.x + (dimention.x - 1) < 0)
        {
            Debug.Log("hors de l'array");
            Debug.Log(position.y + "posi y");

            Debug.Log(new Vector2(position.x + (dimention.x - 1), position.y - (dimention.y - 1)));
            return false;
            //hors de l'array (on vas eviter les erreur de out of range xD)

        }
        for (int i = 0; i < dimention.x; i++)
        {
            for (int j = 0; j < dimention.y; j++)
            {
                if (positionItem[position.x + i, position.y - j] != -1)
                {
                    Item item2 = itemIndex[positionItem[position.x + i, position.y - j]];
                    if (item2 != item)
                    {
                        Debug.Log("une case deja prise");
                        Debug.Log(new Vector2(position.x + i, position.y - j));
                        return false;
                    }
                }
            }
        }
        Debug.Log("place de libre");
        //placer l'item xD
        itemIndex.Add(item);
        int newIndex = itemIndex.IndexOf(item);
        Debug.Log(newIndex);
        for (int i = 0; i < dimention.x; i++)
        {
            for (int j = 0; j < dimention.y; j++)
            {
                AdaptiveScaleItem(dimention, new Vector2Int(i, j), position);

                positionItem[position.x + i, position.y - j] = newIndex;
            }
        }
        RefreshCaseInventoryDisplay();
        return true;
    }

    public void DeleteItem(Item item)
    {

        int index = 0;
        for (int i = 0; i < itemIndex.Count; i++)
        {
            if (item.ID == itemIndex[i].ID)
            {
                //meme ID
                index = i;
                break;
            }
        }
        Debug.Log(index);
        itemIndex.RemoveAt(index);
        for (int x = 0; x < positionItem.GetLength(0); x++)
        {
            for (int y = 0; y < positionItem.GetLength(1); y++)
            {
                if (positionItem[x, y] == index)
                {
                    positionItem[x, y] = -1;
                    caseArrray[x, y].rawImage.color = new Color(230f, 230f, 230f, 0.6f);
                    caseArrray[x, y].caseUse = false;

                    RectTransform rectTransform = caseArrray[x, y].GetComponent<RectTransform>();
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.localScale = Vector3.one * 50;
                    rectTransform.localPosition = new Vector3(caseArrray[x, y].index.x * 50, caseArrray[x, y].index.y * 50, 0);
                    TextMeshProUGUI textMeshProUGUI = caseArrray[x, y].gameObject.GetComponentInChildren<TextMeshProUGUI>();
                    if (textMeshProUGUI != null) Destroy(textMeshProUGUI.transform.gameObject);
                }
                else
                {
                    if (positionItem[x, y] > index)
                    {

                        positionItem[x, y]--;
                    }
                }
            }

        }

        RefreshCaseInventoryDisplay();
    }


    // permet de stack sans probleme

    public Item StackItem(CaseInventory caseInventoryDrop, Item item)
    {
        //verifier si c'est le bon type d'item

        Item item1 = itemIndex[positionItem[caseInventoryDrop.index.x, caseInventoryDrop.index.y]];
        if (item1.data != item.data)
        {
            return item;
        }

        //item = item que l'on veut stack
        //item1 = item qui se fait stack
        item1.number += item.number;
        int difference;
        if (item1.number > item1.data.maxItem)
        {
            difference = item1.number - item.data.maxItem;
            item1.number -= difference;
            item.number = difference;

        }
        else
        {
            item.number = 0;
        }
        RefreshCaseInventoryDisplay();
        return item;


    }

    public bool PlaceItem(CaseInventory caseInventoryDrop, Item item)
    {
        Vector2Int position = caseInventoryDrop.index;
        Vector2Int dimention = item.data.dimention;
        if (item.isRotate)
        {
            dimention = new Vector2Int(dimention.y, dimention.x);
        }
        for (int i = 0; i < dimention.x; i++)
        {
            for (int j = 0; j < dimention.y; j++)
            {
                if (positionItem[i + position.x, position.y - j] != -1)
                {
                    return false;
                }
            }
        }
        //il y a assez de place;
        itemIndex.Add(item);
        int newIndex = itemIndex.IndexOf(item);

        for (int i = 0; i < dimention.x; i++)
        {
            for (int j = 0; j < dimention.y; j++)
            {
                AdaptiveScaleItem(dimention, new Vector2Int(i, j), position);

                positionItem[position.x + i, position.y - j] = newIndex;

            }
        }
        RefreshCaseInventoryDisplay();

        return true;
    }

    private void AdaptiveScaleItem(Vector2Int dimention, Vector2Int positionRelative, Vector2Int position)
    {
        //attention, pour position relative le x vas en positif mais le y doit aller en nagétif
        Vector3 pivot = new Vector3(0f, 0f, 0);
        Vector3 scale = new Vector3(48, 48, 0);

        if (positionRelative.x == 0)
        {
            pivot += new Vector3(-0.5f, 0f, 0f);
        }
        if (positionRelative.x == (dimention.x - 1))
        {
            pivot += new Vector3(0.5f, 0f, 0f);
        }
        if (positionRelative.y == 0)
        {
            pivot += new Vector3(0f, 0.5f, 0f);
        }
        if (positionRelative.y == (dimention.y - 1))
        {
            pivot += new Vector3(0f, -0.5f, 0f);
        }
        if (pivot.y == 0 & pivot.x != 0)
        {
            scale = new Vector3(48, 50, 0);
        }
        if (pivot.y != 0 & pivot.x == 0)
        {
            scale = new Vector3(50, 48, 0);
        }
        if (pivot.x == 0 & pivot.y == 0)
        {
            scale = new Vector3(50, 50, 0);

        }

        RectTransform rectTransform = caseArrray[position.x + positionRelative.x, position.y - positionRelative.y].GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one * 50;
        rectTransform.pivot -= new Vector2(pivot.x, pivot.y);
        rectTransform.transform.position -= pivot * 50;
        rectTransform.localScale = scale;
        Debug.Log(positionRelative);
        Debug.Log(pivot);

    }

}


