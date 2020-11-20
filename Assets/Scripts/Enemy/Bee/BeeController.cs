using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{

    public int hp;
    public int speed;
    private Animator animator;

    private int bulletAvaible = 3;

    public Enemy_SO data;

    public Vector2 move;

    private bool isAttack = false;

    private float startTime;

    private bool isActive = false;

    Coroutine currentInvervalAttackCoroutine;

    private int count = 0;

    public GameObject Bullet;

    public BulletPosition bulletPosition;


    //cách thứ 2: quản lý nhiều coroutine: ít khi dùng ( nên hạn chế dùng)
    Dictionary<string, Coroutine> coroutineMap = new Dictionary<string, Coroutine>();
    void Start()
    {
        speed = data.speed;
        animator = GetComponent<Animator>();

        //cách thứ nhất: nên dùng cách này
        if (currentInvervalAttackCoroutine != null)
        {
            StopCoroutine(currentInvervalAttackCoroutine);
            currentInvervalAttackCoroutine = null;
        }
        currentInvervalAttackCoroutine = StartCoroutine(IntervalAttack());
    }

    void StartCustomCoroutine(string coroutineName)
    {
        if (coroutineMap.ContainsKey(coroutineName))
        {
            StopCoroutine(coroutineMap[coroutineName]);
            coroutineMap.Remove(coroutineName);
        }

        coroutineMap[coroutineName] = StartCoroutine(coroutineName);
    }

    void Update()
    {
        if (count != 3)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 16f);
            if (hit && hit.collider.CompareTag("Player") && count <= 3)
            {
                StartCoroutine(active());
            }
            if (isActive)
            {
                float xDistance = MaskDudeController.instance.transform.position.x - transform.position.x;
                Debug.Log("in atk" + xDistance);
                //khoảng cách giữa nhân vật và con quái: nếu nhân vật ở bên trái thì hiệu trên là số âm
                //nếu nhân vật ở bên phải thì hiệu trên là số dương
                if (Mathf.Abs(xDistance) < 0.1f)
                {
                    Debug.Log("in atk" + xDistance); // không vào
                                                     //cái return ở đây có bug, về fix
                                                     //attack
                    animator.SetBool("Attack", true);
                    return;
                }
                //chỗ này di chuyển
                transform.Translate(Vector3.right * ((xDistance > 0) ? 1f : -1f) * Time.deltaTime * speed);
            }
            animator.SetBool("Attack", false);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            StartCoroutine(death());
        }
    }

    IEnumerator active()
    {
        yield return new WaitForSeconds(1.5f);
        isActive = true;
    }

    public void fire()
    {
        if (count != 3)
        {
            count++;
            GameObject arrow = Instantiate(Bullet);
            arrow.transform.position = bulletPosition.gameObject.transform.position;
        }
    }

    IEnumerator IntervalAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //TODO: Do something
        }
    }

    IEnumerator IntervalSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //TODO: Do something
        }
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
