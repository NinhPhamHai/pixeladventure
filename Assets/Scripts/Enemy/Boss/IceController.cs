using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviour
{
    private SpriteRenderer sprite;
    public float speed;

    private float xTarget;

    private float yTarget;

    private bool fromAToB;

    private Animator animator;

    private bool isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        xTarget = MaskDudeController.instance.transform.position.x;
        yTarget = MaskDudeController.instance.transform.position.y;
        fromAToB = BossController.instance.fromAToB;
    }

    // Update is called once per frame
    void Update()
    {
        if (fromAToB)
        {
            sprite.flipX = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xTarget, yTarget, 0), 8 * Time.deltaTime);
        if (transform.position.x == xTarget && transform.position.y == yTarget)
        {
            isDestroy = true;
        }
        animator.SetBool("Destroy",isDestroy);
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            isDestroy = true;
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
