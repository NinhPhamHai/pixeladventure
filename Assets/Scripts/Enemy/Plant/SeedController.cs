using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : BasePhysicObject
{
    public float speed;

    protected override void Start()
    {
        base.Start();
        gravityScale = 0;
    }

    protected override void caculatorVelocity()
    {
        targetVelocity = new Vector2(speed * -1f, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
