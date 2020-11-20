using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : BaseEnemy
{
    public int hp;
    public int speed;
    private Animator animator;

    public Enemy_SO data;

    private bool isHit = false;

    private bool isDestroy = false;

    private bool isAttack = false;

    private bool isIdle = false;

    public Transform player;

    public GameObject seed;

    public FirePosition firePosition;

    void Awake()
    {
        hp = data.hp;
        speed = data.speed;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 20f);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) *20f, Color.red);
        if (player)
        {
            if (hit && hit.collider.CompareTag("Player"))
            {
                isAttack = true;
                isIdle = false;
            }
            else
            {
                isAttack = false;
                isIdle = true;
            }

        }
        animator.SetBool("Idle", isIdle&&!isHit&&!isDestroy);
        animator.SetBool("Attack", isAttack&&!isHit&&!isDestroy);
        animator.SetBool("Hit", isHit);
        animator.SetBool("Destroy", isDestroy&&!isHit);
        //todo neen check 1s 1 laanf
    }

    public void attack()
    {
        GameObject arrow = Instantiate(seed);
        arrow.transform.position = firePosition.gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot")&&!isDestroy)
        {
            if (hp > 0)
            {
                isHit = true;
                hp--;
                if (hp == 0)
                {
                    isHit = false;
                    isDestroy = true;
                }
            }
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
