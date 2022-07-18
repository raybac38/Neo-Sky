using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Net : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject game;
    public AnimationChargement chargement;
    public GameObject player;
    public NetworkManager network;

    public bool dejaVu = false;
    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        //quand un scene est load
        if(scene.name == "Game")
        {
            if (dejaVu)
            {
                return;
            }
            else
            {


                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
                
                network.StartHost(); //modifier ca dans le futur si l'on veut le porter a du MMO
                
                RefreshChunkView chunkView = player.GetComponentInChildren<RefreshChunkView>();
                StartCoroutine(ChargementDuMonde(chunkView));
                dejaVu = true;
                //on charge le jeux ^^
                //lancer la coroutine pour le chargement des chunks
            }

        }
    }
    


    IEnumerator ChargementDuMonde(RefreshChunkView refreshChunk)
    {
        refreshChunk.load = true;
        int nbrequie = 40;
        while(refreshChunk.chunkLoad.Count < nbrequie)
        {
            chargement.LoadBarProgress(0.6f + (refreshChunk.chunkLoad.Count / 100));
            yield return null;
        }
        Debug.Log("chargement de monde fini");
        SceneManager.UnloadSceneAsync("Menu");
    }
   
}
