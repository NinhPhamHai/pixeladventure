using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    public Animator animator;

    private bool canJump = true;

    public float force;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void jump()
    {
        MaskDudeController.instance.jumpByTrampoline(force);
    }

    public void resetJump()
    {
        animator.SetTrigger("Idle");
        canJump = true;
    }

    public void collisionFromBox(Collision2D collider)
    {
        if (canJump)
        {
            animator.SetTrigger("Jump");
            canJump = false;
        }
    }

    public void collisionExitFromBox(Collision2D collider)
    {

    }
}
