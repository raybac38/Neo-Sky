using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Sun : MonoBehaviour
{
    //sun du Game

    private void Start()
    {
        GetComponent<Light>().enabled = false;
    }
    void Update()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        
    }

    
    void OnSceneUnloaded(Scene scene)
    {
        if(scene.name == "Menu")
        {
            GetComponent<Light>().enabled = true;
        }
    }

}
