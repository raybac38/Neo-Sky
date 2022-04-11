using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuHome : MonoBehaviour
{
    public void RequestMenuHome()
    {
        SceneManager.LoadScene("MenuPrincipale");
    }
}
