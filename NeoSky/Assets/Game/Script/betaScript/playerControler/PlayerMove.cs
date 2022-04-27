using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //script pour le deplacement du joueur
    public Rigidbody rb;
    public bool ghostJump = false;
    private bool isGrappin = false;
    private Vector3 mouvement;
    private bool run;
    private float speed;
    private bool isGrounded = false;


    // zone des constantes
    private float forceMultiplier = 45f;
    private float defaultSpeed = 15f;
    private float jumpForce = 80f;
    private float ghostJumpCooldownTimer = 2f;

    private void Awake()
    {
        speed = defaultSpeed;
    }
    private void Update()
    {
        mouvement = Vector3.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            mouvement.z++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mouvement.z--;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            mouvement.x--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mouvement.x++;
        }

        RunManager();
        UpdateDeplacement();
        JumpManager();
    }
    public void JumpManager()
    {
        if (Input.GetKey(KeyCode.Space) & isGrounded & !ghostJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            ghostJump = true;
        }
    }

    IEnumerator GhostJumpCooldown()
    {
        yield return new WaitForSeconds(ghostJumpCooldownTimer);
        ghostJump = false;
    }

    private void RunManager()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) & isGrounded)
        {
            speed = 2 * defaultSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = defaultSpeed;
        }
        if (!isGrounded)
        {
            speed = defaultSpeed;
        }
    }
    private void UpdateDeplacement()
    {
        float angle = transform.eulerAngles.y;
        angle = Mathf.Deg2Rad * angle;
        mouvement = mouvement.normalized;
        mouvement *= speed * Time.deltaTime;

        rb.MovePosition(transform.position + new Vector3(mouvement.x * Mathf.Cos(angle) + (mouvement.z * Mathf.Sin(angle)), 0, mouvement.x * Mathf.Sin(-angle) + mouvement.z * Mathf.Cos(angle)));
    }
    /// <summary>
    /// met la variable isGrounded a la valeur de isOnFloor
    /// </summary>
    /// <param name="isOnFloor">est t'il au sol ?</param>
    public void GroundedState(bool isOnFloor)
    {
        isGrounded = isOnFloor;
    }

}
