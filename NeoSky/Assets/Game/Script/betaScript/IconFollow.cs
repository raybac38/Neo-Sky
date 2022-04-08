using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconFollow : MonoBehaviour
{
    public Canvas canvas;
    public InventoryCase inventoryCase;

    private void Awake()
    {
        transform.localPosition = inventoryCase.transform.localPosition;
        this.enabled = false;
    }
    public void Update()
    {
        transform.position = Input.mousePosition;
        if (!Input.GetMouseButton(1) & !Input.GetMouseButton(0))
        {
            this.enabled = false;
        }
    }
    public void OnDisable()
    {
        transform.localPosition = inventoryCase.transform.localPosition;
    }
}
