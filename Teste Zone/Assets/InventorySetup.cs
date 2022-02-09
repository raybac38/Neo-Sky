using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySetup : MonoBehaviour
{
    public GameObject contour;
    public Vector2 offSet;
    public GameObject InventoryContoure;
    private int y;
    private int n = 0;
    private int longueur = 12;
    // Start is called before the first frame update
    private void Awake()
    {
        contour.SetActive(true);
        GenerationLigne();
        for (int i = 0; i < 5; i++)
        {
            GenerationTroue();
        }
        GenerationLigne();
        for (int i = 0; i < 10; i++)
        {
            GenerationTroue();
        }
        GenerationLigne();
        for (int i = 0; i < 3; i++)
        {
            GenerationTroue();
        }
        GenerationLigne();


        Transform[] transformTotal = InventoryContoure.GetComponentsInChildren<Transform>();
        assigniationLigne(transformTotal);
        assigniationLigneVide(transformTotal, 5);
        assigniationLigne(transformTotal);
        assigniationLigneVide(transformTotal, 11);
        assigniationLigne(transformTotal);


        
        contour.SetActive(false);
    }

    private void GenerationLigne()
    {
        for (int i = 0; i < longueur + 2; i++)
        {
            Instantiate(contour, InventoryContoure.transform);
        }
    }
    private void GenerationTroue()
    {
        Instantiate(contour, InventoryContoure.transform);
    }

    void assigniationLigne(Transform[] trTot)
    {
        for (int k = 0; k < longueur + 2; k++)
        {
            trTot[n].localPosition = new Vector3(offSet.x + k, offSet.y + y, 0);
        }
        y++;
    }
    void assigniationLigneVide(Transform[] trTot, int nb)
    {
        for (int k = 0; k < nb; k++)
        {
            trTot[n].localPosition = new Vector3(offSet.x, offSet.y + y, 0);
            n++;
            trTot[n].localPosition = new Vector3(offSet.x + longueur + 1, offSet.y + y, 0);
            y++;
        }
        
    }
}
