using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemManager")]
public class ItemManager : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public GameObject prefabRessource = null;
    public GameObject handItem = null;
}
