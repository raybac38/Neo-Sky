using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CraftCase : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public DragAndDropManager dragAndDropManager;
    public CraftManager craft;
    public GameObject progressionBar;
    public TextMeshProUGUI inscription;
    public RawImage image;
    public bool freeze;
    public void OnDrop(PointerEventData eventData)
    {
        if(!freeze) dragAndDropManager.DropCraftCase(this);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!freeze)
        {
            craft.DumpOneCraftCase(this);
        }
    }


}
