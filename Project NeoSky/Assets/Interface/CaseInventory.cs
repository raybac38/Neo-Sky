using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class CaseInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public RawImage rawImage;
    public Vector2Int index;
    public GrilleInventaire grilleInventaire;
    public bool caseUse = false;
    public TextMeshProUGUI textMesh;

    public DragAndDropManager dragAndDropManager;
   
    private GameObject ghostItem;
    public Vector3 position;
    private void Start()
    {
        if(rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
            
            rawImage.color = new Color(230f, 230f, 230f, 0.6f);
        }
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



            //creation du ghostItem ^^
            Vector2Int dimention;
            Item item = grilleInventaire.itemIndex[grilleInventaire.positionItem[Mathf.RoundToInt(index.x), Mathf.RoundToInt(index.y)]];
            dimention = item.data.dimention; //gg pour cette ligne de code sans fin xD
            position = transform.position;
            ghostItem = Instantiate(new GameObject("ghost"));
            ghostItem.transform.SetParent(grilleInventaire.transform);
            ghostItem.AddComponent<RawImage>().color = item.data.itemColor;
            ghostItem.transform.SetAsLastSibling();
            ghostItem.GetComponent<RectTransform>().sizeDelta = dimention;
            ghostItem.transform.localScale = new Vector3(50f, 50f, 1);
            GameObject number = Instantiate(new GameObject("number"), ghostItem.transform);
            TextMeshProUGUI textMesh = number.AddComponent<TextMeshProUGUI>();
            textMesh.text = item.number.ToString();
            number.GetComponent<RectTransform>().sizeDelta = Vector2Int.one;
            number.GetComponent<RectTransform>().pivot = Vector2Int.zero;
            number.GetComponent<RectTransform>().localPosition = new Vector3(dimention.x, dimention.y, 0) / -2;
            ghostItem.layer = 3;
            CanvasGroup canvas = ghostItem.AddComponent<CanvasGroup>();
            canvas.blocksRaycasts = false;
            textMesh.fontSize = 0.7f;
            textMesh.color = Color.white;
            number.layer = 3;

            ///deuxieme partie
            ///
            dragAndDropManager.DeclareDragPoint(item, grilleInventaire);



        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (caseUse)
        {
            ghostItem.transform.position = Input.mousePosition;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ghostItem != null)
        {
            Destroy(ghostItem);
        }
        

    }
    
    public void OnDrop(PointerEventData eventData)
    {
        dragAndDropManager.DeclareDropPoint(grilleInventaire, this);        
    }

}
