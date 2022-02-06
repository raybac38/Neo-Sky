using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public float harvestRange;
    public int hotBarState = 0;
    public Text hotBarIndicator;
    public Grapin grapin;
    public Transform objectDevantMoi;
    public bool canHarvest = true;
    public void LeftClic()
    {
        if(hotBarState == 1)
        {
            Harvest();
        }
    }
    public void Harvest()
    {
        objectDevantMoi = grapin.requestSphereCast(harvestRange);
        if(objectDevantMoi != null & canHarvest)
        {
            StartCoroutine(HarvesteTimer());
            
        }
    }
    void Update()
    {
        UpdateHotBar();
        
    }
    public void UpdateHotBar()
    {
        int mouseDelta = (int)Input.mouseScrollDelta.y;
        if(mouseDelta != 0)
        {
            if(hotBarState + mouseDelta > 9)
            {
                hotBarState += mouseDelta - 10;
            }else if(hotBarState + mouseDelta < 0)
            {
                hotBarState += mouseDelta + 10;
            }
            else
            {
                hotBarState += mouseDelta;
            }
            hotBarIndicator.text = hotBarState.ToString();
        }

        
    }
    IEnumerator HarvesteTimer()
    {
        canHarvest = false;
        yield return new WaitForSeconds(2);
        objectDevantMoi = null;
        canHarvest = true;
    }
    private void RequestAddObject()
    {
        Debug.Log("can i add some object ?");

    }
}
