using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class button : MonoBehaviour
{
    public ItemManager myItem;
    public CraftManager craftManager;
    public TextMeshProUGUI affichage;
    public void SetTexte()
    {
        if(myItem != null)
        {
            affichage.text = myItem.name;

        }
    }
    public void Selected()
    {
        craftManager.ChangeItemToCraft(myItem);
    }

}
