using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverMarche : MonoBehaviour
{
    public MapSwap map;
    private void OnMouseOver()
    {
        map.ShowMapMarche();
    }
}
