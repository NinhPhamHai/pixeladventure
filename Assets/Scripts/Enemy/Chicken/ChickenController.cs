using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : BaseEnemy
{
    public int hp;
    public int speed;
    private Animator animator;

    public Enemy_SO data;

    public Transform player;

    public float distanceActive;

    private bool isHit;

    private bool isDestroy = false;

    

    void Awake()
    {
        hp = data.hp;
        speed = data.speed;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame



    void Update()
    {
        if (player)
        {
            if (Vector3.Distance(player.position, transform.position) <= distanceActive)
            {
                isActive = true;
            }
            else
            {
            }
        }
        else
        {
            //do nothing
        }
        animator.SetBool("Run", isActive);
        animator.SetBool("Hit", isHit);
    }

    public void resetHit()
    {
        isHit = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot")&&!isDestroy)
        {
            isHit = true;
            if (hp >= 1)
            {
                hp--;
                if (hp == 0)
                {
                    isDestroy = true;
                    animator.SetBool("Destroy", isDestroy);
                }
            }
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
