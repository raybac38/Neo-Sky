using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intercalaire : MonoBehaviour
{
    public GameObject[] sousParties;
    public Image[] fondSousParties;
    public Button[] bouttonSousParties;

    public void Start()
    {
        CallPartie1();
    }
    public void CallPartie1()
    {
        clearOldPartie();
        sousParties[0].SetActive(true);
        fondSousParties[0].enabled = false;
        bouttonSousParties[0].enabled = false;

    }
    public void CallPartie2()
    {
        clearOldPartie();
        sousParties[1].SetActive(true);
        fondSousParties[1].enabled = false;
        bouttonSousParties[1].enabled = false;

    }
    public void CallPartie3()
    {
        clearOldPartie();
        sousParties[2].SetActive(true);
        fondSousParties[2].enabled = false;
        bouttonSousParties[2].enabled = false;

    }
    public void CallPartie4()
    {
        clearOldPartie();
        sousParties[3].SetActive(true);
        fondSousParties[3].enabled = false;
        bouttonSousParties[3].enabled = false;

    }
    public void CallPartie5()
    {
        clearOldPartie();
        sousParties[4].SetActive(true);
        fondSousParties[4].enabled = false;
        bouttonSousParties[4].enabled = false;

    }
    public void clearOldPartie()
    {
        for (int i = 0; i < sousParties.Length; i++)
        {
            sousParties[i].SetActive(false);
            fondSousParties[i].enabled = true;
            bouttonSousParties[i].enabled = true;

        }
    }
}
