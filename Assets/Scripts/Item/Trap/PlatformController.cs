using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform BPoint;
    private Animator animator;

    private bool isOn;

    private bool playerTouched;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTouched)
        {
            isOn = true;
        }
        animator.SetBool("On", isOn);
    }

    void FixedUpdate()
    {
        if (isOn&&transform.position.x < BPoint.position.x)
        {
            Vector2 move = new Vector2(3 * Time.deltaTime * 1, 0);
            transform.Translate(move);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            playerTouched = true;
        }
    }

}
