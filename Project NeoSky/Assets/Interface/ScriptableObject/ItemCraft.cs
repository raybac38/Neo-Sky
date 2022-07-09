using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemCraft", order = 1)]

public class ItemCraft : ScriptableObject
{
    public string nameOfItem; //nom de l'item a craft
    public bool needCraftStation; //mettre true si il y a besion de qq chose pour le craft

    public List<string> ressources = new List<string>();
    //le mots marquer dans ressource sera recherche par l'ordinateur en temps que nom et type   
    

    
    public List<int> quantiter = new List<int>();

    public ItemData output;

    public float craftingTime;
}
