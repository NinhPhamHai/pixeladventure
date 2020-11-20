using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private BoxCollider2D box;
    private bool isActive;
    private Animator animator;

    public Transform player;

    private int hp;

    private bool reachTarget;

    private Coroutine moveCoroutine;

    private Coroutine attackCoroutine;

    private SpriteRenderer sprite;

    private bool isRun;

    private bool isHit;

    private bool isDie;

    private bool isAttack;

    private bool isIdle;

    private bool farTarget;

    public GameObject ice;

    public ChildPosition childPosition;

    public bool fromAToB;

    public static BossController instance;

    private bool canFire = true;

    private float speed = 8;

    private Coroutine waitCoroutine;

    private bool isFirstType = true;

    private bool avoid = false;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        hp = 6;
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float xDistance = player.position.x - transform.position.x;
        if (Mathf.Abs(xDistance) <= 20f)
        {
            isActive = true;
        }
        if (isActive)
        {
            if (hp > 2 && !reachTarget)
            {
                isFirstType = true;
                moveToPlayerPosition();
            }
            if (hp < 2 && avoid && !isDie)
            {
                avoidAttack();
            }
            if (hp <= 2 && !farTarget && !isDie)
            {
                isFirstType = false;
                run();
            }
            if (hp <= 2 && farTarget && canFire && !isDie)
            {
                isFirstType = false;
                canFire = false;
                powAttack();
            }
        }

        if (!isDie)
        {
            if (!isHit)
            {
                if (!isRun)
                {
                    if (!isIdle)
                    {
                        if (!isAttack)
                        {

                        }
                        else
                        {
                            isIdle = false;
                            isRun = false;
                            isHit = false;
                        }
                    }
                    else
                    {
                        isAttack = false;
                        isRun = false;
                        isHit = false;
                    }
                }
                else
                {
                    isIdle = false;
                    isAttack = false;
                    isHit = false;
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
            isActive = false;
            reachTarget = false;
            isRun = false;
            isIdle = false;
            isHit = false;
            isAttack = false;
        }

        checkPosition();


        animator.SetBool("Run", isRun);
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Hit", isHit);
        animator.SetBool("Attack", isAttack);
        animator.SetBool("Die", isDie);
    }

    private void moveToPlayerPosition()
    {
        float xDistance = player.position.x - transform.position.x;
        transform.Translate(Vector3.right * ((xDistance > 0) ? 1f : -1f) * Time.deltaTime * speed);
        sprite.flipX = fromAToB;
        isRun = true;
        if (Mathf.Abs(xDistance) < 0.1f)
        {
            isIdle = true;
            reachTarget = true;
            if (moveCoroutine != null)
            {
                StopCoroutine(move());
                moveCoroutine = null;
            }
            moveCoroutine = StartCoroutine(move());
        }
    }

    private void checkPosition()
    {
        if (transform.position.x < player.transform.position.x)
        {
            fromAToB = true;
        }
        else
        {
            fromAToB = false;
        }
    }
    private void run()
    {
        sprite.flipX = !fromAToB;
        reachTarget = false;
        float xDistance = player.position.x - transform.position.x;
        float deltaX = 20f - Mathf.Abs(xDistance);
        if (Mathf.Abs(xDistance) < 20f)
        {
            isRun = true;
            speed = 10;
            if (player.position.x < transform.position.x)
            {
                transform.Translate(Vector3.right * 1 * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.right * -1 * Time.deltaTime * speed);
            }
        }
        if (Mathf.Abs(deltaX) < 0.1f)
        {
            isRun = false;
            farTarget = true;
            sprite.flipX = true;
        }
    }


    private void powAttack()
    {
        sprite.flipX = fromAToB;
        isHit = false;
        isIdle = false;
        isRun = false;
        isAttack = true;
    }

    IEnumerator attack()
    {
        isRun = false;
        isIdle = false;
        isAttack = true;
        canFire = true;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
        isAttack = true;
    }


    IEnumerator move()
    {
        yield return new WaitForSeconds(2f);
        reachTarget = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            isHit = true;
            if (hp >= 1)
            {
                hp--;
            }
            if (hp == 0)
            {
                isActive = false;
                isDie = true;
                box.isTrigger = true;
            }
            if (!isFirstType)
            {
                farTarget = false;
                canFire = false;
            }
        }
    }

    public void endHit()
    {
        isIdle = true;
        isHit = false;
    }

    public void fire()
    {
        GameObject arrow = Instantiate(ice);
        arrow.transform.position = childPosition.gameObject.transform.position;
    }

    public void endFire()
    {
        isIdle = true;
        isAttack = false;
        canFire = false;
        if (waitCoroutine != null)
        {
            StopCoroutine(waiting());
            waitCoroutine = null;
        }
        waitCoroutine = StartCoroutine(waiting());
    }

    private void avoidAttack()
    {
        avoid = true;
        float targetX;
        if (fromAToB)
        {
            targetX = player.transform.position.x + 5f;
        }
        else
        {
            targetX = player.transform.position.x - 5f;
        }
        float xDistance = targetX - player.transform.position.x;
        transform.Translate(Vector3.right * ((fromAToB) ? 1f : -1f) * Time.deltaTime * speed);
        sprite.flipX = fromAToB;
        isRun = true;
        if (Mathf.Abs(xDistance) < 0.1f)
        {
            isRun = false;
            isAttack = true;
            avoid = false;
            farTarget = true;
            canFire = true;
        }
    }
}
