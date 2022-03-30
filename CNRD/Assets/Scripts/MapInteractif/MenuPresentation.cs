using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPresentation : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CarteSatelite2022;
    public GameObject TesteCarteVirge;

    public string typeCarte;

    public CarteChange carteChange;

    public int numeroEvenement;
    public bool isEvenement;

    public Animator PartiDroiteEcran;
    public Animator ButtonRetour;
    public Animator ChangementDeCarte;
    public Animator maison;


    public GameObject[] buttons;
    public GameObject[] evenementArray;


    private void Awake()
    {
        isEvenement = false;
        PartiDroiteEcran.SetBool("isEvenement", false);
        ButtonRetour.SetBool("isShow", false);
        ChangementDeCarte.SetBool("isShow", true);
        maison.SetBool("Open", true);
        ShowButtons(true);
        numeroEvenement = 0;
    }
    public void RefreshData()
    {
        CarteSatelite2022 = carteChange.CarteSatelite2022;
        TesteCarteVirge = carteChange.TesteCarteVirge;
        typeCarte = carteChange.typeCarte;
    }

    IEnumerator TexteShow(int numeroEvent)
    {
        yield return new WaitForSeconds(1f);

    }
    public void Evenement1()
    {
        LancementEvenement();
        numeroEvenement = 1;
        LanchEvent(0);

    }
    public void Evenement2()
    {
        LancementEvenement();
        numeroEvenement = 2;
        LanchEvent(1);

    }
    public void Evenement3()
    {
        LancementEvenement();
        numeroEvenement = 3;
        LanchEvent(2);

    }
    public void Evenement4()
    {
        LancementEvenement();
        numeroEvenement = 4;
        LanchEvent(3);

    }
    public void Evenement5()
    {
        LancementEvenement();
        numeroEvenement = 5;
        LanchEvent(4);

    }
    public void Evenement6()
    {
        LancementEvenement();
        numeroEvenement = 6; 
        LanchEvent(5);

    }
    public void Evenement7()
    {
        LancementEvenement();
        numeroEvenement = 7;
        LanchEvent(6);

    }
    public void Evenement8()
    {
        LancementEvenement();
        numeroEvenement = 8;
        LanchEvent(7);
    }
    public void Evenement9()
    {
        LancementEvenement();
        numeroEvenement = 9;
        LanchEvent(8); 
    }

    public void LancementEvenement()
    {
        PartiDroiteEcran.SetBool("isEvenement", true);
        ButtonRetour.SetBool("isShow", true);
        ChangementDeCarte.SetBool("isShow", false);
        maison.SetBool("Open", false);

        isEvenement = true;
        ShowButtons(false);
    }

    public void CloseEvenement()
    {
        PartiDroiteEcran.SetBool("isEvenement", false);
        ButtonRetour.SetBool("isShow", false);
        ChangementDeCarte.SetBool("isShow", true);
        maison.SetBool("Open", true);

        isEvenement = false;
        numeroEvenement = 0;
        ShowButtons(true);
    }


    public void ShowButtons(bool isShow)
    {
        if (isShow)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
        }
        if (!isShow)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void LanchEvent(int indice)
    {
        for (int i = 0; i < evenementArray.Length; i++)
        {
            evenementArray[i].SetActive(false);
        }
        evenementArray[indice].SetActive(true);
        
    }
}
