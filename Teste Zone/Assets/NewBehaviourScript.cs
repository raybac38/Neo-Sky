using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler
{
    private bool follow = false;
    public InventoryManagment inventoryManagment;
    public GameObject BackGround;
    public Image Me;
    public Vector3 oldPosition;
    public Collider2D pasLibre;
    public bool canPlace = true;
    public bool selected = false;
    // Start is called before the first frame update
    private void Awake()
    {
        BackGround.SetActive(false);
    }
    void Start()
    {
    
    }
    
    private void OnTriggerEnter()
    {
        canPlace = false;
    }
    private void OnTriggerExit2D()
    {
        canPlace = true;
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        Debug.Log("desus");
        if (Input.GetMouseButtonDown(0))
        {
            Selected();
            Debug.Log("selectionner");
        }
    }
    void Update()
    {
        if (selected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Placement();
            }
        }
        if (follow)
        {
            transform.position = Input.mousePosition;
            BackGround.transform.localPosition = new Vector3(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y), 0);

        }
    }
    public void Selected()
    {
        if (selected)
        {
            return;
        }
        oldPosition = transform.localPosition;
        follow = true;
        BackGround.SetActive(true);
        selected = true;
    }
    
    public void Placement()
    {
        if (canPlace)
        {
            transform.localPosition = new Vector3(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y), 0);
        }
        else
        {
            transform.localPosition = oldPosition;
        }
        BackGround.SetActive(false);
        follow = false;
        selected = false;
    }

    // quand commence le drag
    public void OnPointerDown(PointerEventData eventData)
    {
        Selected();
    }
}
