using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string type; //wood / metal
    public int maxItem;
    public Vector2Int dimention;
    public Color itemColor;

}
