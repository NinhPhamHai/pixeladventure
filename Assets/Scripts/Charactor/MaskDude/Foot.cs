using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : BasePhysicObject
{

    private MaskDudeController mask;
    void Awake()
    {
        mask = this.transform.parent.GetComponent<MaskDudeController>();
    }
    private
    void OnCollisionEnter2D(Collision2D other)
    {

        switch (other.gameObject.tag)
        {
            case "Grass":
                {
                    mask.onGrassCollisionEnter();
                    break;
                }
            case "Enemy":
                {
                    mask.enemyJump();
                    break;
                }
            default:
                break;
        }
    }
}
