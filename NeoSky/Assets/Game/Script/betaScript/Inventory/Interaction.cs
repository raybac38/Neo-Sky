using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Interaction : MonoBehaviour
{
    public TextMeshProUGUI texte;
    public GameObject cameraPivot;
    public InventoryAccess inventoryAccess;

    private Interactable interactable;
    private string objectName;
    private RaycastHit hit;

    private void Update()
    {
        if(Physics.Raycast(cameraPivot.transform.position, cameraPivot.transform.forward, out hit))
        {
            if (hit.transform.TryGetComponent<Interactable>(out interactable))
            {
                objectName = interactable.nom;
            }
            else
            {
                objectName = null;
            }
        }
        UpdateTexte();
        if (Input.GetKey(KeyCode.E))
        {
            Use();
        }
    }

    private void UpdateTexte()
    {
        if(objectName == null)
        {
            texte.text = null;
        }
        else
        {
            texte.text = "use" + objectName;
        }
    }
    private void Use()
    {
        if(objectName == null)
        {
            return;
        }
        if(objectName == "CraftingStation")
        {
            inventoryAccess.OpenInventoryCraft(hit.transform.GetComponent<CraftingStationControler>());
        }
    }
}
