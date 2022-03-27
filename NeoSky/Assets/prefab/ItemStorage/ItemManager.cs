using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemManager")]
public class ItemManager : ScriptableObject
{
    new public string name = "New Item";
    public Texture icon = null;
    public GameObject handItem = null;

    public bool item = false;
    
    public Vector2Int itemSize;
    public Vector2Int pixelSize;
    public Sprite[,] itemPicture;
    public GameObject[] prefabRessource = null;
    public float masseVolumique = 0f;
    public float capaciterThermique = 0f;
    public float conductiviterThermique = 0f;
    public float pointDeFusion = 0f;
}
