using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDoking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pieds;
    public GameObject shipDock;
    public GameObject floor;
    public GameObject ramps;
    public bool canInteract;

    private void Start()
    {
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator AnimationPlacement()
    {
        yield return new WaitForSeconds(1f);
        floor.SetActive(true);
        //lancer l'animation du sols
        yield return new WaitForSeconds(2f);
        pieds.SetActive(true);

        //lancer l'animation des pieds et de la ramps
        yield return new WaitForSeconds(2f);
        canInteract = true;
    }
    public void StartAnimationPlace()
    {
        pieds.SetActive(false);

        ramps.SetActive(false);
        floor.SetActive(false);
        shipDock.SetActive(true);
        StartCoroutine(AnimationPlacement());

    }
}
