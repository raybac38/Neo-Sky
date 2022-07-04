using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class CaseInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RawImage rawImage;
    public Vector2Int index;
    public GrilleInventaire grilleInventaire;
    public bool caseUse = false;
    public TextMeshProUGUI textMesh;


    public Vector3 position;
    private void Start()
    {
        if(rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
            rawImage.color = new Color(230f, 230f, 230f, 0.6f);
        }
    }
    private void OnBeginDrag()
    {

    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!caseUse)
        {

            rawImage.color = new Color(230f, 230f, 230f, 1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!caseUse)
        {
            rawImage.color = new Color(230f, 230f, 230f, 0.6f);

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (caseUse)
        {
            position = transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (caseUse)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (caseUse)
        {
            transform.position = position;
        }

    }

    
}
