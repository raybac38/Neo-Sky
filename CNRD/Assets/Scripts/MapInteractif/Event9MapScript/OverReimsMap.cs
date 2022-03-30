using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverReimsMap : MonoBehaviour
{
    // Start is called before the first frame update
    public Evenement9 evenement;
    private void OnMouseOver()
    {
        evenement.SetMapToShow("Musée de la Reddition");
    }
}
