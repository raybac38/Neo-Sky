using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public CraftingSlots[] craftingSlots;
    public bool canCraft;
    public float cooldownTimer = 5f;

    public ItemManager itemManagerCraft;

    public ItemManager teste;
    /// <summary>
    /// Detecte si tout les items nessessaires sont présent
    /// </summary>
    /// <returns>bool value</returns>
    public bool UpdateCanCraftStatu()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if(craftingSlots[i].requiredMyItem != null)
            {
                if (craftingSlots[i].full == true)
                {
                    canCraft = true;
                }
                else
                {
                    canCraft = false;
                    return false;
                }
            }
            else
            {
                canCraft = true;
            }
        }
        canCraft = true;
        return true;
    }
    public void TesteCraftItem()
    {
        itemManagerCraft = teste;
        RefreshCraftingSlots(itemManagerCraft);
    }
    /// <summary>
    /// boutton de craft
    /// </summary>
    public void RequestCraft()
    {
        if(canCraft == true)
        {
            SuppresCraftingItem();
            StartCoroutine(Cooldown());
        }
        else
        {
            //rien, peu pas craft du con
        }
    }
    /// <summary>
    /// temps de repos entre 2 craft
    /// </summary>
    /// <returns>rien</returns>
    IEnumerator Cooldown()
    {
        canCraft = false;
        yield return new WaitForSeconds(cooldownTimer);
        canCraft = true;

    }
    /// <summary>
    /// supprime les items dans les slots car le craft s'effectue
    /// </summary>
    public void SuppresCraftingItem()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].myItemNumber = 0;
        }
    }
    public void RequetsItemToCraft(ItemManager itemManageur)
    {
        if(itemManagerCraft == itemManageur)
        {
            itemManagerCraft = null;
            //enlever l'item a craft si c'est le meme (une sorte de deselection)
        }
        if (itemManageur.craftableItem == true)
        {
            itemManagerCraft = itemManageur;
        }

    }
    public void RefreshCraftingSlots(ItemManager item)
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].ChangeRequieredItem(item.elementForCraft[i], item.nombreElementForCraft[i]);
        }
    }
    public void DeselectionCraft()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            
            craftingSlots[i].ChangeRequieredItem(null, 0);
        }
    }
    
}
