using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAccess : MonoBehaviour
{
    public GameObject inventory;
    public CraftManager craftManager;

    private bool tabActive = false;
    // Update is called once per frame
    private void Start()
    {
        tabActive = false;
        CloseInventory();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) & !tabActive)
        {
            OpenInventoryTab();
            
        }else if (Input.GetKeyDown(KeyCode.Tab) & tabActive)
        {
            CloseInventory();
        }
    }
    private void OpenInventoryTab()
    {
        tabActive = true;
        Cursor.lockState = CursorLockMode.None;
        inventory.SetActive(true);
    }
    private void CloseInventory()
    {
        tabActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        inventory.SetActive(false);
    }
    public void OpenInventoryCraft(CraftingStationControler craftingStationControler)
    {
        inventory.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        tabActive = true;

        craftManager.OpenInCraftingStation(craftingStationControler);
    }
}
