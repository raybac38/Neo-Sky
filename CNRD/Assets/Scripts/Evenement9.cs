using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evenement9 : MonoBehaviour
{
    public int CurrentIntercalaire = 0;
    public GameObject[] IntercalairePartie;
    private void Start()
    {
        //initialisation des données des intercalaires
        for (int i = 0; i < IntercalairePartie.Length; i++)
        {
            IntercalairePartie[i].SetActive(false);
        }
        IntercalairePartie[0].SetActive(true);
    }

    public void RequestNumber(int numero)
    {
        if(numero > IntercalairePartie.Length | CurrentIntercalaire == numero)
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


}
