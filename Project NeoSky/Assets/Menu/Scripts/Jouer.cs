using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Jouer : MonoBehaviour
{
    public Animator titre;
    public Animator jouer;
    public Animator principale;

    public bool show = false;
    public GameObject menuJouer;
    public GameObject menuPrincipale;

    private void Start()
    {

        //desactivation des deux bouton au debut pour rendre tout ca moins charger
        show = false;
        menuPrincipale.SetActive(true);
        menuJouer.SetActive(true);
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

        if (show)
        {
            jouer.SetInteger("event", 1);
            principale.SetInteger("event", 1);
        }
        else
        {
            jouer.SetInteger("event", 0);
            principale.SetInteger("event", 0);
        }
    }

    
}
