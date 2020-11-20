using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : BaseFruilt
{
   public int point;

   public Fruit_SO data;

   private Animator animator;
    void Start()
    {
        point = data.point;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsCollected",true);
            MaskDudeController.instance.addPower(point);
        }
    }

    public void destroyFruit()
    {
        Destroy(gameObject);
    }

}
