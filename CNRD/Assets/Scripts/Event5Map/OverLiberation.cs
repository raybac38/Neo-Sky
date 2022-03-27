using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverLiberation : MonoBehaviour
{
    public MapSwap map;
    private void OnMouseOver()
    {
        map.ShowMapLiberation();
    }
}
