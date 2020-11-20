using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameLionController : MonoBehaviour
{
    public Transform APoint, BPoint;

    private SpriteRenderer charactorSprite;

    private float thressholdOffset = 0.1f;

    private float speed = 4;
    private bool fromAToB = false;

    private Animator animator;

    private GameObject toggle;

    private Coroutine currentCorroutine;

    private int hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        charactorSprite = GetComponent<SpriteRenderer>();
        toggle = this.gameObject.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
    }

    private bool isAttack;

    private bool isRun = true;

    private bool isHit;

    private bool isIdle;

    private bool isDestroy;


    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fromAToB ? Vector2.right : Vector2.left, 5f);
        //Debug.DrawRay(transform.position, (fromAToB ? Vector2.right : Vector2.left) * 10f, Color.red);

        if (!isDestroy)
        {
            if (!isHit)
            {
                if (!isAttack)
                {
                    if (!isRun)
                    {

                    }
                    else
                    {
                        isAttack = false;
                        isHit = false;
                        isIdle = false;
                        toggle.SetActive(false);
                    }
                }
                else
                {
                    isHit = false;
                    isRun = false;
                    isIdle = false;
                    toggle.SetActive(true);
                }
            }
            else
            {
                isRun = false;
                isIdle = false;
                isAttack = false;
            }
        }
        else
        {
            isHit = false;
            isRun = false;
            isIdle = false;
            isAttack = false;
        }

        if (hit && hit.collider.CompareTag("Player"))
        {
            attack();
        }
        else
        {
            if(!isHit)
            {
                run();
            }
        }

        animator.SetBool("Attack", isAttack);
        animator.SetBool("Run", isRun);
        animator.SetBool("Hit", isHit);
        animator.SetBool("Destroy", isDestroy);
        animator.SetBool("Idle", isIdle);
    }

    private void run()
    {
        isRun = true;
        float xADistance = transform.position.x - APoint.position.x;
        float xBDistance = transform.position.x - BPoint.position.x;
        if (!fromAToB && Mathf.Abs(transform.position.x - APoint.position.x) <= thressholdOffset)
        {
            //transform.Rotate(0, 180, 0);
            fromAToB = true;
        }

        if (fromAToB && Mathf.Abs(transform.position.x - BPoint.position.x) <= thressholdOffset)
        {
            //transform.Rotate(0, 0, 0);
            fromAToB = false;
        }
        charactorSprite.flipX = fromAToB;
        Vector2 move = new Vector2(speed * Time.deltaTime * (fromAToB ? 1f : -1f), 0);
        transform.Translate(move);
        toggle.SetActive(false);
    }

    private void attack()
    {
        isAttack = true;
        toggle.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            isHit = true;
            if (hp > 0)
            {
                hp--;
                if (hp == 0)
                {
                    isDestroy = true;
                }
            }
        }
    }

    public void endHit()
    {
        isIdle = false;
        isHit = false;
        isAttack = false;
        isRun = true;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
