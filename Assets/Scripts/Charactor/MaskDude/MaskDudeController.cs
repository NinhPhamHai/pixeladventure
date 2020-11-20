using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDudeController : BasePhysicObject
{
    public Animator animator;
    public float speed = 7f;

    public float jumpForce = 10f;

    public SpriteRenderer sprite;

    public static MaskDudeController instance;

    private Vector2 move = Vector2.zero;

    private bool isJumping;

    private bool isDoubleJumping;

    private int clickJumpCount = 0;

    private BoxCollider2D boxCollider;

    private bool isHit = false;
    private bool isInviorable = false;
    public int point = 50;

    private bool enemyHited = false;

    private bool haveKey = false;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }



    protected override void caculatorVelocity()
    {
        //move.x = Input.GetAxis("Horizontal");//trả về 1 là sang phải, -1 là sang trái
        targetVelocity = move * speed;
        animator.SetBool("Run", !Mathf.Approximately(velocity.x, 0f) && isGround && !isHit);
        animator.SetBool("Stop", Mathf.Approximately(velocity.x + velocity.y, 0f) && isGround && !isHit);
        animator.SetBool("Jump", !Mathf.Approximately(velocity.y, 0f) && !isHit);
        animator.SetBool("DoubleJump", !Mathf.Approximately(velocity.y, 0f) && isDoubleJumping && !isHit);
        animator.SetBool("Hit", isHit && !enemyHited);
        if (!Mathf.Approximately(move.x, 0f))
        {
            sprite.flipX = move.x < 0;
        }
    }

    public void moveLeft()
    {
        move.x = -1;
    }

    public void moveRight()
    {
        move.x = 1;
    }

    public void idle()
    {
        move.x = 0;
    }

    public void jump()
    {
        clickJumpCount++;
        if (!isJumping)
        {
            velocity.y = jumpForce;
            isJumping = true;
        }
        if (isJumping && !isDoubleJumping && clickJumpCount == 2)
        {
            isDoubleJumping = true;
            clickJumpCount = 0;
            velocity.y += jumpForce * 0.8f;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Grass":
                {
                    isJumping = false;
                    isDoubleJumping = false;
                    clickJumpCount = 0;
                    break;
                }
            case "Enemy":
            case "Saw":
                {
                    if (!isInviorable && !enemyHited)
                    {
                        getDamage();
                        //velocity += new Vector2(-1,1)*16.0f; nhảy lên
                    }
                    break;
                }
            case "Fire":
                {
                    if(FireController.instance.isOn)
                    {
                        getDamage();
                    }
                    break;
                }
            default:
                break;
        }
    }

    public void getDamage()
    {
        if (!isInviorable && !enemyHited)
        {
            inviorable();
            velocity += new Vector2(-1,1)*16.0f;
        }
    }

    public void addPower(int point)
    {
        if (this.point + point < 100)
        {
            this.point += point;
        }
        else
        {
            this.point = 100;
        }
    }



    public void jumpByTrampoline(float force)
    {
        velocity.y += force;
    }

    public void onGrassCollisionEnter()
    {
        isJumping = false;
        isDoubleJumping = false;
        clickJumpCount = 0;
        enemyHited = false;
    }

    public void inviorable()
    {
        isHit = true;
        isInviorable = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        enemyHited = false;
    }

    public void resetInviorable()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        isHit = false;
        isInviorable = false;
        enemyHited = false;
    }

    public void enemyJump()
    {
        enemyHited = true;
        isJumping = true;
        velocity.y = jumpForce;
    }

    public void getKey()
    {
        haveKey = true;
    }

}
