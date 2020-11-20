using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    // Start is called before the first frame update

    public float activeTime;

    private bool isActive;

    private Rigidbody2D _rigidbody;

    private bool canJump = true;

    private CircleCollider2D circle;


    public float force;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.y > 0)
        {
            circle.isTrigger = true;
        }
    }


    IEnumerator jump()
    {
        yield return new WaitForSeconds(activeTime);
        if (canJump)
        {
            Vector2 dir = new Vector2(0, 1);
            _rigidbody.AddForce(dir * force);
            canJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            canJump = true;
            StartCoroutine(jump());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_rigidbody.velocity.y < 0 && other.gameObject.CompareTag("Line"))
        {
            circle.isTrigger = false;
            canJump = true;
            StartCoroutine(jump());
        }
    }
}
