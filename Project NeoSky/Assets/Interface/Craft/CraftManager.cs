using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftManager : MonoBehaviour
{
    //script pour le craft personel
    public TextMeshProUGUI itemCraftName;
    private int taille;
    private Item[] items;
    private ItemCraft itemCraft;
    public List<CraftCase> craftCases = new List<CraftCase>();
    public List<GrilleInventaire> grilleInventaires = new List<GrilleInventaire>();

    public bool craftingStation = false;

    private void Start()
    {
        itemCraftName.text = " ";
        itemCraft = null;
        items = new Item[craftCases.Count];
        RefreshDisplayCraft();
    }
    /// <summary>
    /// change la recette de craft
    /// </summary>
    /// <param name="craft"></param>
    public void ChangeItemToCrat(ItemCraft craft)
    {
        itemCraft = craft;
        if(itemCraft != null)
        {
            itemCraftName.text = itemCraft.nameOfItem;

        }
        else
        {
            itemCraftName.text = " ";
        }
        for (int i = 0; i < items.GetLength(0); i++)
        {
            if (items[i] == null)
            {
                //pas d'item
            }
            else
            {
                //systeme pour envoyer les items dans l'inventaire
                for (int j = 0; j < grilleInventaires.Count; j++)
                {

                    items[i] = grilleInventaires[j].RequestAddItem(items[i]);
                    if (items[i] == null)
                    {

                    }
                    else if (items[i].number == 0)
                    {
                        break;
                    }
                }
                if (items[i].number != 0)
                {
                    Debug.Log("item en trop");
                }
                items[i] = null;
            }
        }
        RefreshDisplayCraft();
    }

    //fini  ^^
    public Item AddItem(CraftCase craftCase, Item item)
    {
        if (itemCraft == null)
        {
            return item;
        }
        //ajout d'un item dans une craftCase
        int index = craftCases.IndexOf(craftCase);
        //recupere l'index de la case drop

        if (items[index] == null)
        {
            //savoir si l'item que l'on ajoute a un type / un nom qui correspond ^^
            string TypeAndName = itemCraft.ressources[index];
            if (TypeAndName == item.data.itemName | TypeAndName == item.data.type)
            {
                //le nom ou le type correspond ^^ super
                Item item1 = ScriptableObject.CreateInstance<Item>();
                item1.number = item.number;
                item1.data = item.data;
                item1.isRotate = item.isRotate;
                item1.ID = Random.Range(0, 10000000);
                if (item1.number > itemCraft.quantiter[index])
                {
                    int difference = item1.number - itemCraft.quantiter[index];
                    item1.number -= difference;
                    item.number = difference;
                }
                else
                {
                    item.number = 0;

                }
                items[index] = item1;
                RefreshDisplayCraft();
                return item;
            }
        }
        else if (items[index].data == item.data)
        {
            items[index].number += item.number;
            if (items[index].number > itemCraft.quantiter[index])
            {
                int difference = items[index].number - itemCraft.quantiter[index];
                items[index].number -= difference;
                item.number = difference;
                RefreshDisplayCraft();
                return item;
            }
            else
            {
                item.number = 0;
                RefreshDisplayCraft();
                return item;
            }

            //stack les deux items 
        }
        RefreshDisplayCraft();
        return item;
    }

    public void CraftManagerClose()
    {
        ChangeItemToCrat(null);
    }
    public void DumpOneCraftCase(CraftCase craft)
    {
        int index = craftCases.IndexOf(craft);
        Item item1 = ScriptableObject.CreateInstance<Item>();
        if (items[index] == null || items[index].number == 0)
        {
            return;
        }
        item1.data = items[index].data;
        item1.number = items[index].number;
        item1.isRotate = items[index].isRotate;
        item1.ID = Random.Range(0, 10000000);
        
        for (int i = 0; i < grilleInventaires.Count; i++)
        {

            item1 = grilleInventaires[i].RequestAddItem(item1);
            if (item1 == null || item1.number == 0)
            {

                items[index].number = 0;
                RefreshDisplayCraft();
                return;
            }
            
        }
        items[index].number = 0;
        RefreshDisplayCraft();
        return;

    }

    public ItemCraft pistoletPrefab;
    public void PrefabPistoletCraft()
    {
        ChangeItemToCrat(pistoletPrefab);
    }

    public void RequestCraftItem()
    {
        if (itemCraft == null) return;
        for (int i = 0; i < items.GetLength(0); i++)
        {
            if (items[i] == null & itemCraft.ressources[i] != null) return;
            if (itemCraft.quantiter[i] != items[i].number)
            {
                return;
            }
        }
        //tout les items son en nombre suffisant
        for (int i = 0; i < craftCases.Count; i++)
        {
            craftCases[i].freeze = true;
            //on lock tout ca ^^

            //mettre la "fonte des matériaux"            
        }
        StartCoroutine(MaterialsConsumption());

    }
    IEnumerator MaterialsConsumption()
    {
        float time = itemCraft.craftingTime;
        float timer = 0f;
        while (time > timer)
        {
            timer += Time.deltaTime;
            //calcule les materieux qu'il reste
            float pourcentage = 0f;
            if (time != 0)
            {
                pourcentage = (time - timer) / time;
            }
            for (int i = 0; i < items.GetLength(0); i++)
            {
                items[i].number = Mathf.RoundToInt(itemCraft.quantiter[i] * pourcentage);
            }
            RefreshDisplayCraft();
            yield return null;
        }
        for (int i = 0; i < craftCases.Count; i++)
        {
            craftCases[i].freeze = false;
        }
        RefreshDisplayCraft();

        yield return null;
    }

    public void RefreshDisplayCraft()
    {
        if (itemCraft == null)
        {
            for (int i = 0; i < craftCases.Count; i++)
            {
                craftCases[i].inscription.text = " ";
                craftCases[i].progressionBar.transform.localScale = new Vector3(0, 1, 1);
                craftCases[i].image.color = Color.white;

            }
            return;
        }
        for (int i = 0; i < craftCases.Count; i++)
        {
            if (items[i] != null && items[i].number != 0)
            {
                craftCases[i].inscription.text = items[i].data.itemName.ToString() + "\n" + items[i].number.ToString()
                    + " / " + itemCraft.quantiter[i].ToString();
                craftCases[i].image.color = items[i].data.itemColor;
                craftCases[i].progressionBar.transform.localScale = new Vector3((float)items[i].number/ (float)itemCraft.quantiter[i], 1, 1);
            }
            else if (itemCraft.ressources[i] != null)
            {
                craftCases[i].image.color = Color.white;
                craftCases[i].inscription.text = itemCraft.ressources[i].ToString();
                craftCases[i].progressionBar.transform.localScale = new Vector3(0, 1, 1);

            }
            else
            {
                craftCases[i].inscription.text = " ";
                craftCases[i].progressionBar.transform.localScale = new Vector3(0, 1, 1);

            }
        }
    }
}
