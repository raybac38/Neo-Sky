using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenement9 : MonoBehaviour
{

    public GameObject[] AllMap;

    public string MapToShow = "Mus�e de la Reddition";
    
    private void Start()
    {

        SetMapToShow("Mus�e de la Reddition");
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
