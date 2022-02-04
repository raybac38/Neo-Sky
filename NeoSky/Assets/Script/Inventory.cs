using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int[] data;
    
    void CreateInventory(int largeur, int hauteur)
    {
        if (largeur == 0 | hauteur == 0)
        {
            Debug.LogError("dimmension inventaire erronée");
            return;
        }
        for (int i = 0; i < largeur * hauteur; i++)
        {
            
        }
    }
}
