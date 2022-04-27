using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftInterface : MonoBehaviour
{
    public GameObject[] interfaceInteraction;
    public TextMeshProUGUI titre;

    public void RequestShowInterface(string name)
    {
        for (int i = 0; i < interfaceInteraction.Length; i++)
        {
            if(interfaceInteraction[i].name == name)
            {
                interfaceInteraction[i].SetActive(true);
                titre.text = name;
            }
            else
            {
                interfaceInteraction[i].SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < interfaceInteraction.Length; i++)
        {
            interfaceInteraction[i].SetActive(false);
        }
        titre.text = null;
    }
}
