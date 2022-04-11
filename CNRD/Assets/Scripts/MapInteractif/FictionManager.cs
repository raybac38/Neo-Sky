using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FictionManager : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator.SetInteger("State", -1);
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        if(animator.GetInteger("State") == -1)
        {
            animator.SetInteger("State", 0);
        }
    }
    private void OnMouseOver()
    {
        animator.SetInteger("State", 1);

    }
    private void OnMouseExit()
    {
        animator.SetInteger("State", 0);

    }
}
