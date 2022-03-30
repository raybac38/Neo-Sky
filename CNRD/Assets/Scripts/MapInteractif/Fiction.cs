using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiction : MonoBehaviour
{
    public GameObject FictionRuban;
    // Start is called before the first frame update
    void Start()
    {
        FictionRuban.SetActive(true);
    }

    private void OnDisable()
    {
        FictionRuban.SetActive(false);
    }
}
