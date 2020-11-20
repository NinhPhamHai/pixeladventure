using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public int hp;
    public int speed;
    private Animator animator;

    public Enemy_SO data;

    public Transform pointA, pointB;

    public bool fromAToB = false;

    public SpriteRenderer charactorSprite;

    public float thressholdOffset = 0.1f;

    private bool isIdle1 = false;

    private bool haveSpike = false;
    void Start()
    {
        hp = data.hp;
        speed = data.speed;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isIdle1)
        {
            Vector2 move = new Vector2(speed * Time.deltaTime * (fromAToB ? 1f : -1f), 0);
            transform.Translate(move);
        }
        if (fromAToB && Mathf.Abs(transform.position.x - pointB.position.x) <= thressholdOffset)
        {
            fromAToB = false;
        }
        else if (!fromAToB && Mathf.Abs(transform.position.x - pointA.position.x) <= thressholdOffset)
        {
            fromAToB = true;
        }
        else
        {
            //do nothing
        }
        charactorSprite.flipX = fromAToB;
    }

    public void Idle1()
    {
        animator.SetTrigger("Idle1");
        isIdle1 = true;
        StartCoroutine(restart());
    }

    public void Idle2()
    {
        animator.ResetTrigger("SpikeIn");
        animator.SetTrigger("Idle2");
        haveSpike = false;
    }

    IEnumerator restart()
    {
        haveSpike = false;
        yield return new WaitForSeconds(2.5f);
        isIdle1 = false;
        animator.ResetTrigger("Idle1");
        animator.SetTrigger("SpikeIn");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Foot":
                {
                    if(haveSpike)
                    {
                        MaskDudeController.instance.getDamage();
                    }else{
                        animator.SetTrigger("Hit");
                        if(hp>0)
                        {
                            hp--;
                            if(hp==0)
                            {
                                animator.SetTrigger("Destroy");
                            }
                        }
                    }
                    break;
                }
            default: break;
        }
    }

    public void getDamage()
    {
        haveSpike = true;
        animator.ResetTrigger("Hit");
        animator.SetTrigger("SpikeOut");
        animator.ResetTrigger("Idle2");
    }


    public void destroyEnemy()
    {
        Destroy(gameObject);
    }


}
