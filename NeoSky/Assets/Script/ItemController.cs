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
    public GameObject[] listeObjetc;
    public GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.localScale = new Vector3(1, 1, 1);
        CreateAffichage();
    }

    private void CreateAffichage()
    {
        listeObjetc = new GameObject[largeurItem * hauteurItem];
        int nb = 0;
        RectTransform SetHauteurLargeur;
        for (int i = 0; i < largeurItem; i++)
        {
            for (int k = 0; k < hauteurItem; k++)
            {
                listeObjetc[nb] = new GameObject("image");
                SetHauteurLargeur = listeObjetc[nb].AddComponent<RectTransform>();
                listeObjetc[nb].AddComponent<CanvasRenderer>();
                listeObjetc[nb].AddComponent<RawImage>();
                SetHauteurLargeur.sizeDelta = new Vector2(1, 1);
                SetHauteurLargeur.localPosition = new Vector2(-i, -k);
                
                listeObjetc[nb].transform.SetParent(this.transform);
                nb++;
            }
        }
        transform.localScale = new Vector3(30, 30, 30);
        transform.position = new Vector3(774, 474, 0);

    }


}
