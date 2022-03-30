using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwap : MonoBehaviour
{
    public GameObject mapLiberation;
    public GameObject mapMarcheMort;

    private void Start()
    {
        ShowMapLiberation();
    }
    public void ShowMapMarche()
    {
        MapChange();
        mapMarcheMort.SetActive(true);
    }
    public void ShowMapLiberation()
    {
        MapChange();
        mapLiberation.SetActive(true);
    }
    private void MapChange()
    {
        mapLiberation.SetActive(false);
        mapMarcheMort.SetActive(false);

    }
}
