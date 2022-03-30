using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverBerlinMap : MonoBehaviour
{
    public Evenement9 evenement;
    private void OnMouseOver()
    {
        evenement.SetMapToShow("Musée Germano Russe Berlin Facade");
    }
}
