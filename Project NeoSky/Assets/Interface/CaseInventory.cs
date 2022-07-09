using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class CaseInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    /// <summary>
    /// rotatoin de l'item a faire plus tard.
    /// </summary>
    public RawImage rawImage;
    public Vector2Int index;
    public GrilleInventaire grilleInventaire;
    public bool caseUse = false;
    public TextMeshProUGUI textMesh;

    public DragAndDropManager dragAndDropManager;
   
    private GameObject ghostItem;
    public Vector3 position;

    public Item itemDrag;
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
            itemDrag = grilleInventaire.itemIndex[grilleInventaire.positionItem[Mathf.RoundToInt(index.x), Mathf.RoundToInt(index.y)]];
            CreateGhostItem();

            //creation du ghostItem ^^

            ///deuxieme partie
            ///
            dragAndDropManager.DeclareDragPoint(itemDrag, grilleInventaire, this);
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if(itemDrag == null)
        {
            return;
        }
        Vector2Int dimention = itemDrag.data.dimention;
        if (itemDrag.isRotate)
        {
            dimention = new Vector2Int(dimention.y, dimention.x);
        }
        if (caseUse)
        {
            ghostItem.transform.position = Input.mousePosition + (new Vector3(dimention.x / 2, dimention.y / -2, 0) * 50);
        }
    }
    public void RotateItem()
    {
        Debug.Log("rorate");
        Destroy(ghostItem);
        CreateGhostItem();
        Vector2Int dimention = itemDrag.data.dimention;
        if (itemDrag.isRotate)
        {
            dimention = new Vector2Int(dimention.y, dimention.x);
        }
        ghostItem.transform.position = Input.mousePosition + (new Vector3(dimention.x / 2, dimention.y / -2, 0) * 50);
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

    public void CreateGhostItem()
    {
        Vector2Int dimention = itemDrag.data.dimention;
        if (itemDrag.isRotate)
        {
            dimention = new Vector2Int(dimention.y, dimention.x);
        }

        position = transform.position;
        ghostItem = new GameObject("ghostItem");
        ghostItem.AddComponent<RawImage>().color = itemDrag.data.itemColor;
        ghostItem.GetComponent<RectTransform>().sizeDelta = dimention;
        ghostItem.transform.localScale = new Vector3(50f, 50f, 1);
        GameObject number = new GameObject("number");
        number.transform.SetParent(ghostItem.transform);


        TextMeshProUGUI textMesh = number.AddComponent<TextMeshProUGUI>();
        textMesh.text = itemDrag.number.ToString();

        RectTransform rect = number.GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
        rect.sizeDelta = Vector2Int.one;
        rect.pivot = Vector2Int.zero;
        rect.localPosition = new Vector3(dimention.x, dimention.y, 0) / -2;


        CanvasGroup canvas = ghostItem.AddComponent<CanvasGroup>();
        canvas.blocksRaycasts = false;
        textMesh.fontSize = 0.7f;
        textMesh.color = Color.white;

        ghostItem.transform.localScale = new Vector3(48f, 48f, 0f);
        /// partie qui bug
        number.transform.SetAsLastSibling();
        ghostItem.transform.SetAsLastSibling();
        ghostItem.transform.SetParent(grilleInventaire.transform);
    }

}
