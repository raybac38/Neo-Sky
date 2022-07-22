using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{

    public CraftManager craftManager;
    public GrilleInventaire grilleInventaire;
   
    // Start is called before the first frame update
    void Start()
    {
        craftManager.gameObject.SetActive(false);
        grilleInventaire.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

        }
    }

    public void OpenInventoryTab()
    {
        grilleInventaire.gameObject.SetActive(true);
        craftManager.gameObject.SetActive(true);
    }
}
