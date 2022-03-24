using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    Behaviour[] componentsToDisable;
    private void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false; //desactiver tout les behaviour qui ne sont gere que par le client
            }
        }
    }
}
