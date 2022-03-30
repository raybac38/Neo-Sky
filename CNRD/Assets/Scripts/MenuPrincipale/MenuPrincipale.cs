using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuPrincipale : MonoBehaviour
{
    public Animator source;
    public Animator credit;
   
    public GameObject credits;
    public GameObject sources;

    public Button[] buttonMenuPrincipale;
    public int State;


    // 1 = menu principale, 2 = source, et 3 = crédits

    void Start()
    {
        State = 1;
        sources.SetActive(false);
        credits.SetActive(false);
        StateChange();
    }
    public void Source()
    {
        State = 2;
        StateChange();

    }
    public void Credits()
    {
        State = 3;
        StateChange();

    }
    public void LauncheCarteInteractive()
    {
        //mettre un fondu en noire
        SceneManager.LoadScene("CarteInteractif");
    }
    public void RequestQuit()
    {
        Application.Quit();
    }

    public void StateChange()
    {
        if(State != 1)
        {
            for (int i = 0; i < buttonMenuPrincipale.Length; i++)
            {
                buttonMenuPrincipale[i].enabled = false;
                //desactivation des bouttons pour ne pas recliquer desus

            }
        }
        else
        {
            for (int i = 0; i < buttonMenuPrincipale.Length; i++)
            {
                buttonMenuPrincipale[i].enabled = true;
                //reactivation des bouttons ainsi que la fermeture de la fenetre
            }
            StartCoroutine(closeWindows());
            Debug.Log("apres la coroutine");
        }
        if(State == 2)
        {
            //dans les sources
            sources.SetActive(true);
            source.SetBool("Open", true);
        }if(State == 3)
        {
            //dans les credits
            credits.SetActive(true);
            credit.SetBool("Open", true);
        }
    }
    public void retour()
    {
        State = 1;
        StateChange();
    }
    IEnumerator closeWindows()
    {
     
        credit.SetBool("Open", false);
        Debug.Log("open = false");
        source.SetBool("Open", false);
        yield return new WaitForSeconds(1.05f);

        credits.SetActive(false);
        Debug.Log("false");
        sources.SetActive(false);

    }
    
    
}
