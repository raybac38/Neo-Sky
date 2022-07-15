using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Net : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject game;
    public AnimationChargement chargement;
    public GameObject player;
    public NetworkManager network;
    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        //quand un scene est load
        if(scene.name == "Game")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
            network.StartHost(); //modifier ca dans le futur si l'on veut le porter a du MMO
            //faire le spawn du joueur ^^
            RefreshChunkView chunkView = player.GetComponentInChildren<RefreshChunkView>();
            StartCoroutine(ChargementDuMonde(chunkView));

            //on charge le jeux ^^
            //lancer la coroutine pour le chargement des chunks

        }
    }



    IEnumerator ChargementDuMonde(RefreshChunkView refreshChunk)
    {
        int nbrequie = 40;
        while(refreshChunk.chunkLoad.Count < nbrequie)
        {
            chargement.LoadBarProgress(60 + refreshChunk.chunkLoad.Count);
            yield return null;
        }
        SceneManager.UnloadSceneAsync("Menu");
    }

}
