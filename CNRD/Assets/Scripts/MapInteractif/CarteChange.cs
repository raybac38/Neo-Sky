using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarteChange : MonoBehaviour
{
    public GameObject RubanChoixCarte;
    public bool visible = false;
    public string typeCarte;

    public Animation RubanAnimation;
    public Animator RubanAnimator;

    public GameObject CarteSatelite2022;
    public GameObject TesteCarteVirge;
    public GameObject EntreDeuxGuerre;


    private void Awake()
    {
        MapChange();
        CarteSatelite2022.SetActive(true);
        visible = false;
        RubanAnimator.SetBool("See", false);
        
    }
    public void ToggleRubanChoixCarte()
    {
        visible = !visible;
        RubanAnimator.SetBool("See", !RubanAnimator.GetBool("See"));

    }
    public void closeRubanChoixCarte()
    {
        visible = false;
        RubanAnimator.SetBool("See", false);

    }

    public void SelectSatelite2022Map()
    {
        if(typeCarte == "Satelite_2022")
        {
            closeRubanChoixCarte();
            return;
        }
        MapChange();
        typeCarte = "Satelite_2022";
        CarteSatelite2022.SetActive(true);
        closeRubanChoixCarte();

    }
    public void SelectEntreDeuxGuerre()
    {
        if (typeCarte == "EntreDeuxGuerre")
        {
            closeRubanChoixCarte();
            return;
        }
        MapChange();
        typeCarte = "EntreDeuxGuerre";
        EntreDeuxGuerre.SetActive(true);
        closeRubanChoixCarte();

    }
    public void SelectTesteCarteVirge()
    {
        if (typeCarte == "TesteCarteVirge")
        {
            closeRubanChoixCarte();
            return;
        }
        MapChange();
        typeCarte = "TesteCarteVirge";
        TesteCarteVirge.SetActive(true);
        closeRubanChoixCarte();

    }
    public void MapChange()
    {
        CarteSatelite2022.SetActive(false);
        TesteCarteVirge.SetActive(false);
        EntreDeuxGuerre.SetActive(false);
    }

}
