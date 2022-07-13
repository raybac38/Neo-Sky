using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Jouer : MonoBehaviour
{
    public bool show = false;
    public GameObject menuJouer;
    public GameObject menuPrincipale;

    private void Start()
    {
        //desactivation des deux bouton au debut pour rendre tout ca moins charger
        show = false;
        menuJouer.SetActive(false);
        menuPrincipale.SetActive(true);
    }
    public void menuSwap()
    {
        //on active les deux boutons ^^
        MenuJouerShow();
    }

    /// <summary>
    /// apparaitre / disparaitre le menu
    /// </summary>
    /// <param name="value">true => menu jouer apparait</param>
    private void MenuJouerShow()
    {
        show = !show;
        menuJouer.SetActive(show);
        menuPrincipale.SetActive(!show);
    }

    
}
