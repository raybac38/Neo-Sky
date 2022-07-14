using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class AnimationChargement : MonoBehaviour
{
    public Jouer jouer;
    public List<Transform> pierres = new List<Transform>();
    public GameObject logoCasser;
    public GameObject logoEntier;
    List<Transform> animationAvenir = new List<Transform>();
    List<float> hauteur = new List<float>();
    public float cadance = 0.03f;
    public float speed = 1.5f;

    bool cut = false;
    int nombreDebris = 0;
    private void Update()
    {
        if(Input.anyKeyDown | Input.GetMouseButton(0) | Input.GetMouseButton(1))
        {
            cut = true;
        }
    }
    private void Awake()
    {
        logoEntier.SetActive(false);
        logoCasser.SetActive(true);
        foreach (Transform pierre in logoCasser.GetComponentsInChildren<Transform>())
        {
            if(pierre != logoCasser.transform)
            {
                pierres.Add(pierre);
                
                pierre.transform.localPosition -= new Vector3(0, 0, 27f);
            }
        }
        animationAvenir = pierres;
        //faire un sorte ...
        nombreDebris = pierres.Count;
        pierres.Sort((a, b) => a.transform.localPosition.z.CompareTo(b.transform.localPosition.z));
        animationAvenir = pierres;
    }

    private void Start()
    {
        lumiere.SetActive(true);

        StartCoroutine(LancementDesPierres());
    }
    IEnumerator LancementDesPierres()
    {
        yield return new WaitForSeconds(0.65f);
        int i = pierres.Count;
        while(i != 0)
        {
            i--;
            StartCoroutine(Launch());
            if (cut)
            {
                StartCoroutine(SwitchActive());
                break;
            }
            yield return new WaitForSeconds(cadance);
        }
    }
    
    IEnumerator Launch()
    {
        Transform transform = animationAvenir[animationAvenir.Count - 1];
        float objectif = transform.localPosition.z + 26.3f;
        animationAvenir.RemoveAt(animationAvenir.Count - 1);
        while(transform.localPosition.z < objectif - 0.3f)
        {
            transform.localPosition += Vector3.forward * speed * Time.deltaTime * Mathf.Abs(transform.localPosition.z - objectif);
            yield return null;
        }
        nombreDebris--;
        if(nombreDebris == 0)
        {
            Debug.Log("le dernier xD");
            StartCoroutine(SwitchActive());
        }
        yield return null;
    }

    IEnumerator SwitchActive()
    {
        loadMaterial.SetFloat("Vector1_0711e0ba407146f985b924dce8e25d12", (17.7f) - 10.3f);
        jouer.jouer.SetInteger("event", 2);
        jouer.principale.SetInteger("event", 2);
        jouer.titre.SetInteger("event", 2);

        yield return new WaitForSeconds(0.3f);
        logoEntier.SetActive(true);
        logoCasser.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// animation de chargement ^^ 
    /// </summary>
    public Material loadMaterial;


    public void LoadBarProgress(float pourcentage)
    {
        pourcentage = 1 - pourcentage;
        loadMaterial.SetFloat("Vector1_0711e0ba407146f985b924dce8e25d12", (pourcentage * 0.5f * 17.7f) - 10.3f);
    }

    public GameObject lumiere;
    public NetworkManager networkManager;
    public void LoadSceneAsHost()
    {
        StartCoroutine(LoadAsyncScene());
    }
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone)
        {
            
            LoadBarProgress(asyncOperation.progress);
            yield return null;
        }

        //faire spawn le joueur
        lumiere.SetActive(false);
        networkManager.StartHost();

        //charger le terrain autours de lui

        //dire adieux a se monde cruel xD
        //SceneManager.UnloadSceneAsync("Menu");
        
    }
}

