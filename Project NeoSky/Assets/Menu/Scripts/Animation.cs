using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public List<Transform> pierres = new List<Transform>();
    public GameObject logoCasser;
    public GameObject logoEntier;
    List<Transform> animationAvenir = new List<Transform>();
    List<float> hauteur = new List<float>();
    public float cadance = 0.03f;
    public float speed = 1.5f;

    int nombreDebris = 0;
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

        yield return new WaitForSeconds(0.3f);
        logoEntier.SetActive(true);
        logoCasser.SetActive(false);
        StartCoroutine(LoadAnimation());
        yield return null;
    }

    /// <summary>
    /// animation de chargement ^^ 
    /// </summary>
    public Material loadMaterial;

    IEnumerator LoadAnimation()
    {
        float pourcentage = 100;
        while(pourcentage > 0)
        {
            
            pourcentage--;
            loadMaterial.SetFloat("Vector1_0711e0ba407146f985b924dce8e25d12", (pourcentage / 100 * 17.7f) - 10.3f);
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }
}

