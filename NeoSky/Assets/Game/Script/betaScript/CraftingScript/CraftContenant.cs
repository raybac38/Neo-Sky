using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftContenant : MonoBehaviour
{
    public List<button> playerCraft = new List<button>();
    public List<button> craftingStation = new List<button>();
    public IntercalaireDesCrafts IntercalaireDesCrafts;
    public CraftManager craftManager;
    private int status = 1;

    public GameObject buttonPrefab;

    public ItemManager donnut;
    private void Awake()
    {
        ShowChange(status);

    }
    public void Teste()
    {
        AddCraft(2, donnut);
    }
    /// <summary>
    /// permet d'ajouter une nouvelle recette de craft
    /// </summary>
    /// <param name="state"> 1 = player, 2 = craftingStation</param>
    /// <param name="item">l'item possible a craft</param>
    public void AddCraft(int state, ItemManager item)
    {
        if(state == 1)
        {
            //craft du joueur
            for (int i = 0; i < playerCraft.Count; i++)
            {
                if(playerCraft[i].myItem.name == item.name)
                {
                    return;
                }
            }
            GameObject ram = Instantiate(buttonPrefab, this.transform);
            button component = ram.GetComponent<button>();
            component.myItem = item;
            component.SetTexte();
            component.craftManager = craftManager;
            playerCraft.Add(component);
            ShowChange(status);
        }
        else
        {
            //craft du la station
            for (int i = 0; i < craftingStation.Count; i++)
            {
                if (craftingStation[i].myItem.name == item.name)
                {
                    return;
                }
            }
            GameObject ram = Instantiate(buttonPrefab, this.transform);
            button component = ram.GetComponent<button>();
            component.myItem = item;
            Debug.Log(component.myItem.name);
            component.craftManager = craftManager;
            component.SetTexte();

            craftingStation.Add(component);
            ShowChange(status);
        }
    }

    public void ShowChange(int state)
    {
        status = state;
        if(state == 1)
        {
            //montrer player
            for (int i = 0; i < playerCraft.Count; i++)
            {
                playerCraft[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < craftingStation.Count; i++)
            {
                craftingStation[i].gameObject.SetActive(false);
            }
        }
        else if(state == 2)
        {
            //montrer playerCraft
            for (int i = 0; i < playerCraft.Count; i++)
            {
                playerCraft[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < craftingStation.Count; i++)
            {
                craftingStation[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < playerCraft.Count; i++)
            {
                playerCraft[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < craftingStation.Count; i++)
            {
                craftingStation[i].gameObject.SetActive(false);
            }
        }
    }

}
