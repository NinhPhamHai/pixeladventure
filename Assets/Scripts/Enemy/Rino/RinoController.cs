using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoController : MonoBehaviour
{
    public int hp;
    public int speed;
    private Animator animator;

    public Enemy_SO data;
    private bool isHit;

    private bool isRun;

    private bool hitWall;

    private bool isDestroy = false;

    private bool walking = true;

    private Vector2 move = Vector2.zero;

    public float thressholdOffset = 0.1f;
    public bool fromAToB = false;
    public SpriteRenderer charactorSprite;

    private Coroutine currentCoroutine;

    void Awake()
    {
        hp = data.hp;
        speed = data.speed;
        animator = GetComponent<Animator>();
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fromAToB ? Vector2.right : Vector2.left, 10f);
        if (hit && hit.collider.CompareTag("Player"))
        {
            isRun = true;
            speed = 9;
        }
        //Debug.DrawRay(transform.position, (fromAToB ? Vector2.right : Vector2.left) * 10f, Color.red);
        Vector2 move = new Vector2(speed * Time.deltaTime * (fromAToB ? 1f : -1f), 0);
        charactorSprite.flipX = fromAToB;
        transform.Translate(move);

        if (!isDestroy)
        {
            if (!isHit)
            {
                if (!hitWall)
                {

                }
                else
                {
                    walking = false;
                }
            }
            else
            {
                walking = false;
                hitWall = false;
            }
        }
        else
        {
            walking = false;
            isHit = false;
            hitWall = false;
        }
        animator.SetBool("Run", walking);
        animator.SetBool("HitWall", hitWall);
        animator.SetBool("Destroy", isDestroy);
        animator.SetBool("Hit", isHit);
    }

    public void resetHit()
    {
        isHit = false;
        walking = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                {
                    fromAToB = !fromAToB;
                    if (!isRun)
                    {

                    }
                    if (isRun)
                    {
                        hitWall = true;
                        if (currentCoroutine != null)
                        {
                            StopCoroutine(hitWallState());

                        }
                        currentCoroutine = StartCoroutine(hitWallState());
                    }
                    break;
                }
            case "Foot":
                {
                    if (!isDestroy)
                    {
                        isHit = true;
                        if (hp >= 1)
                        {
                            walking = false;
                            hitWall = false;
                            isRun = false;
                            hp--;
                            if (hp == 0)
                            {
                                isHit = false;
                                isRun = false;
                                walking = false;
                                isDestroy = true;
                            }
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }

    IEnumerator hitWallState()
    {
        speed = 0;
        walking = false;
        yield return new WaitForSeconds(1);
        hitWall = false;
        isRun = false;
        speed = 4;
        walking = true;
    }
}
