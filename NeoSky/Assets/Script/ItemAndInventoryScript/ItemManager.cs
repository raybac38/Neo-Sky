using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemManager")]
public class ItemManager : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public GameObject prefabRessource = null;
    public GameObject handItem = null;

    public bool item = false;

    public Vector2 itemSize;
    public Sprite[,] itemPicture;
    
    public float masseVolumique = 0f;
    public float reststanceThermique = 0f;
    public float capaciterThermique = 0f;
}
