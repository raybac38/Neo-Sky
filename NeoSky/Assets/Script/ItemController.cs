using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public string ItemTag;
    public int largeurItem;
    public int hauteurItem;
    public Vector2 positionActuel;
    public bool rotation; // true si tournée a 90°
    public GameObject tuile;
    public GameObject[] listeObjetc;
    public GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateNewItem(string ItemTag_, int largeur_, int hauteur_, Vector2 position_, bool rotation_, GameObject playerUI_)
    {
        ItemTag = ItemTag_;
        largeurItem = largeur_;
        hauteurItem = hauteur_;
        positionActuel = position_;
        rotation = rotation_;
        playerUI = playerUI_;
        transform.SetParent(playerUI_.transform);
        transform.localPosition = Vector3.zero;

        transform.localScale = new Vector3(1, 1, 1);
        CreateAffichage();
    }

    private void CreateAffichage()
    {
        listeObjetc = new GameObject[largeurItem * hauteurItem];
        int nb = 0;
        for (int i = 0; i < largeurItem; i++)
        {
            for (int k = 0; k < hauteurItem; k++)
            {
                listeObjetc[nb] = Instantiate(tuile);
                listeObjetc[nb].transform.localPosition = new Vector2(-i - 240, -k - 250);                
                listeObjetc[nb].transform.SetParent(this.transform);
                nb++;
            }
        }
        transform.localPosition = Vector3.zero;
    }
}
