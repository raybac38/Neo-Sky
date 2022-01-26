using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public Vector3 mouvement;
    public Vector3 deplacement;
    public Rigidbody rb;
    public bool canMove;
    public Vector3 angleInput;
    public float jumpForce = 50f;
    public Vector3 angle;
    public float sensibiliter = 1.5f;
    public float speed = 8f;
    public GameObject Camera; //faire manuellement car trop chiant
    public bool isRunning = false;
    private float addForceForce = 35f;
    // Start is called before the first frame update

    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0, 0, 0);
        angle = new Vector3(0, 0, 0);
        Camera.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RotationManager();
        if (canMove)
        {
            InputMouvement(angle, speed);
            Debug.Log("can move");
        }
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

        deplacement = deplacement * speed * Time.deltaTime;  //calcule du vecteur de deplacement
        RunManager(); //permet au joureur de courir (oubligatoirement sur terre)


        if (isGrounded())
        {
            transform.Translate(deplacement); //la marchche
            Debug.Log("deplacement fait");
            if (Input.GetKey(KeyCode.Space))
            {
                JumpManager(); //le saut quand le personnage touche le sol
            }
        }
        else
        {
            deplacement = new Vector3(deplacement.x * Mathf.Cos(angle.y) - deplacement.z * Mathf.Sin(angle.y), 0,
                           deplacement.x * Mathf.Sin(angle.y) + deplacement.z * Mathf.Cos(angle.y)); 
            //ajouter la force de deplacement dans le sens du regard
            rb.AddForce(deplacement * addForceForce); //la force ==> en grappin ou en l'air
        }
    }
    void RotationManager()
    {
        rotationUpdate(); // avoire la MAJ du client
        RotationSet(); //mettre la valeur de la rotation correct
    }
    void rotationUpdate()
    {
        angleInput = new Vector3(Input.GetAxis("Mouse Y") * -1 * sensibiliter, Input.GetAxis("Mouse X") * sensibiliter, 0);
    }
    void RotationSet()
    {
        Camera.transform.Rotate(angleInput.x, 0, 0);
        transform.Rotate(0, angleInput.y, 0);
        angle = new Vector3(Camera.transform.eulerAngles.x, transform.eulerAngles.y, 0) * Mathf.Deg2Rad; ;

    }
    void RunManager()
    {
        //gere entierement la course
        if (Input.GetKey(KeyCode.LeftShift) & !isRunning & isGrounded())
        {
            speed *= 2;
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) & isRunning | isRunning & !isGrounded())
        {
            speed /= 2;
            isRunning = false;
        }
    }
    void JumpManager()
    {
        //script pour gere le saut
        if (isGrounded())
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f); //check si le personnage est au sol ou pas
        // la taille du personnage, plus 1.1 pour faire resortir le verteur du personnage
    }
}

