using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public Vector3 mouvement;
    public Vector3 deplacement;
    public Vector3 angleInput;
    public Vector3 angle;
    public Vector3 Velocity;
    public Rigidbody rb;
    public bool isGrappin = false;
    public bool canMove;
    public bool cursorLock = true;
    public bool isRunning = false;
    public bool canRotate;
    public bool isGrounded = true;
    public float jumpForce = 50f;
    public float sensibiliter = 1.5f;
    public float speed = 8f;
    private float addForceForce = 45;
    public float capMoveHead = 90;
    public float visioCap = 85;
    public GameObject Camera; //faire manuellement car trop chiant
    public GameObject niveauMarche;
    public Grapin grappin;
    public Inventory inventory;
    public LayerMask fixePoint;
    public CameraManager cameraManager;


    public GameObject ItemTestePrefab;
    public GameObject playerInventory;
    public GameObject ItemTeste;
    

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        canRotate = true;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        
        rb = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0, 0, 0);
        angle = new Vector3(0, 0, 0);
        Camera.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            inventory.LeftClic();
        }
        isGrappin = grappin.isGrappin;
        if (canRotate)
        {
            RotationManager();
        }
        if (canMove)
        {
            InputMouvement(angle, speed);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.InventoryRequest();

        }
        if (Input.GetKey(KeyCode.E))
        {
            inventory.InteractionRequest();
            //interaction faite
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ItemTeste = Instantiate(ItemTestePrefab);
            ItemTeste.GetComponent<ItemController>().CreateNewItem("caca", 9, 21, new Vector2(0, 0), false, playerInventory);

        }
    }
    private void LateUpdate()
    {
        Velocity = rb.velocity;
    }
    void InputMouvement(Vector3 angle, float speed)
    {
        deplacement = new Vector3(0f, 0f, 0f);
        if (Input.GetKey(KeyCode.Z))
        {
            deplacement.z++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            deplacement.z--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            deplacement.x++;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            deplacement.x--;
        }
        deplacement = deplacement.normalized;

        deplacement = deplacement * speed * Time.deltaTime;  //calcule du vecteur de deplacement
        RunManager(); //permet au joureur de courir (oubligatoirement sur terre)

        if (isGrounded)
        {
            CollisionCheck(); //pour faire les deplacement limiter envers les murs

            rb.MovePosition(transform.position + deplacement); //la marchche
            if (Input.GetKey(KeyCode.Space))
            {
                JumpManager(); //le saut quand le personnage touche le sol
            }
        }
        else if (isGrappin)
        {
            deplacement = new Vector3(deplacement.x * Mathf.Cos(angle.y) + (deplacement.z * Mathf.Sin(angle.y)), 0,
                           deplacement.x * Mathf.Sin(-angle.y) + deplacement.z * Mathf.Cos(angle.y)); 
            //ajouter la force de deplacement dans le sens du regard
            rb.AddForce(deplacement * addForceForce); //la force ==> en grappin ou en l'air
        }
    }
    void RotationManager()
    {
        rotationUpdate(); // avoire la MAJ du client
        cameraManager.CameraUpdater(angleInput.x);
        transform.Rotate(0, angleInput.y, 0);
        angle = new Vector3(cameraManager.transform.eulerAngles.x, transform.eulerAngles.y, 0) * Mathf.Deg2Rad;

    }
    void rotationUpdate()
    {
        angleInput = new Vector3(Input.GetAxis("Mouse Y") * -1 * sensibiliter, Input.GetAxis("Mouse X") * sensibiliter, 0);
    }

    void RunManager()
    {
        //gere entierement la course
        if (Input.GetKey(KeyCode.LeftShift) & !isRunning & isGrounded)
        {
            speed *= 2;
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) & isRunning)
        {
            speed /= 2;
            isRunning = false;
        }
    }
    void JumpManager()
    {
        //script pour gere le saut
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
    

    /// <summary>
    /// permte de lock la souris ou de l'unlock
    /// </summary>
    /// <param name="etat">true = lock, false = unlock</param>
    public void ToggleCursorMode(bool etat)
    {
        if (etat)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLock = false;
            canRotate = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            cursorLock = true;
            canRotate = false;
        }
    }
    
    private void CollisionCheck()
    {
        RaycastHit hit;


        float distance;
        Debug.DrawRay(niveauMarche.transform.position, new Vector3(deplacement.x * Mathf.Cos(angle.y) + (deplacement.z * Mathf.Sin(angle.y)), 0,
                           deplacement.x * Mathf.Sin(-angle.y) + deplacement.z * Mathf.Cos(angle.y)) * 20, Color.red, 10f);

        if (Physics.SphereCast(niveauMarche.transform.position,0.5f,  new Vector3(deplacement.x * Mathf.Cos(angle.y) + (deplacement.z * Mathf.Sin(angle.y)), 0,
                           deplacement.x * Mathf.Sin(-angle.y) + deplacement.z * Mathf.Cos(angle.y)) * 20, out hit, fixePoint))
        {
            distance = Vector3.Distance(transform.position, hit.point);
            

            if (distance < 0.8f)
            {
                deplacement = Vector3.zero;
 
            }
        }
    }
}

