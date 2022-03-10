using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenement9 : MonoBehaviour
{
    public int CurrentIntercalaire = 0;
    public GameObject[] IntercalairePartie;

    public GameObject[] AllMap;

    public string MapToShow = "Musée de la Reddition";
    
    private void Start()
    {
        //initialisation des données des intercalaires
        for (int i = 0; i < IntercalairePartie.Length; i++)
        {
            IntercalairePartie[i].SetActive(false);
        }
        IntercalairePartie[0].SetActive(true);
        SetMapToShow("Musée de la Reddition");
    }

    public void RequestNumber(int numero)
    {
        if (numero > IntercalairePartie.Length | CurrentIntercalaire == numero)
        {
            return;
        }
        IntercalaireChange();
        IntercalairePartie[numero].SetActive(true);

    }

    public void IntercalaireChange()
    {
        for (int i = 0; i < IntercalairePartie.Length; i++)
        {
            IntercalairePartie[i].SetActive(false);
        }
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
