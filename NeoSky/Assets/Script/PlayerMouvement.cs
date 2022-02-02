using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public Vector3 mouvement;
    public Vector3 deplacement;
    public Rigidbody rb;
    public bool isGrappin = false;
    public bool canMove;
    public Vector3 angleInput;
    public float jumpForce = 50f;
    public Vector3 angle;
    public float sensibiliter = 1.5f;
    public float speed = 8f;
    public GameObject Camera; //faire manuellement car trop chiant
    public bool isRunning = false;
    private float addForceForce = 45;
    public float capMoveHead = 90;
    public float visioCap = 85;
    public Grapin grappin;
    public bool canRotate;
    public bool isGrounded = true;
    public Vector3 Velocity;



    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        canRotate = true;
        canMove = true;
        rb = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0, 0, 0);
        angle = new Vector3(0, 0, 0);
        Camera.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        isGrappin = grappin.isGrappin;
        if (canRotate)
        {
            RotationManager();
        }
        if (canMove)
        {
            InputMouvement(angle, speed);
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

        deplacement = deplacement * speed * Time.deltaTime;  //calcule du vecteur de deplacement
        RunManager(); //permet au joureur de courir (oubligatoirement sur terre)


        if (isGrounded)
        {
            transform.Translate(deplacement); //la marchche
            Debug.Log("deplacement fait");
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
        RotationSet(Camera.transform.localEulerAngles.x, angleInput.x); //mettre la valeur de la rotation correct
    }
    void rotationUpdate()
    {
        angleInput = new Vector3(Input.GetAxis("Mouse Y") * -1 * sensibiliter, Input.GetAxis("Mouse X") * sensibiliter, 0);
    }
    void RotationSet(float currentRotationX, float inputRotationX)
    {
        //systeme anti cou qui craque
        float newRotationX = currentRotationX + inputRotationX;
        if (((newRotationX >= visioCap * -1 & newRotationX <= visioCap) | (newRotationX <= 444 & newRotationX >= 360 - visioCap) & newRotationX != currentRotationX)) 
        {
            Camera.transform.localEulerAngles = new Vector3(newRotationX, 0, 0);
            Debug.Log("normal rotation");
        } 
        else if(275 > newRotationX & newRotationX > 180)
        {
            Camera.transform.localEulerAngles = new Vector3(-visioCap, 0, 0);
        }
        else if(visioCap < newRotationX & newRotationX <= 180)
        {
            Camera.transform.localEulerAngles = new Vector3(visioCap, 0, 0);
        }
        else
        {
            Debug.Log(newRotationX); //mouvement de camera qui ne sont pas pris en compte par le jeu (trop vite)
        }
        transform.Rotate(0, angleInput.y, 0);
        angle = new Vector3(Camera.transform.localEulerAngles.x, transform.eulerAngles.y, 0) * Mathf.Deg2Rad; ;
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
            rb.AddForce(new Vector3(rb.velocity.x, jumpForce, rb.velocity.z));
        }
    }


}

