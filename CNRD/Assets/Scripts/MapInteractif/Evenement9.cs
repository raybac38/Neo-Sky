using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenement9 : MonoBehaviour
{

    public GameObject[] AllMap;

    public string MapToShow = "Musée de la Reddition";
    
    private void Start()
    {

        SetMapToShow("Musée de la Reddition");
    }

    public void SetMapToShow(string name){
        if (name == MapToShow)
        {
            return;
        }
        else
        {
            MapToShow = name;
            ChangeMapToShow();
        }
    }

    public void ChangeMapToShow()
    {
        for (int i = 0; i < AllMap.Length; i++)
        {
            if(AllMap[i].name == MapToShow)
            {
                AllMap[i].SetActive(true);
            }
            else
            {
                AllMap[i].SetActive(false);
            }
        }
    }

}
