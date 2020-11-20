using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BasePhysicObject : MonoBehaviour
{
    [SerializeField]
    protected float miniumNomalY = 0.7f;

    [SerializeField]
    protected float miniumDistance = 0.0001f;

    [SerializeField]
    protected float gravityScale;
    protected Rigidbody2D _body;

    protected bool isGround = false;

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[25];

    protected Vector2 velocity;
    protected Vector2 targetVelocity;

    protected Vector2 groundNomal = Vector2.up;

    protected List<RaycastHit2D> listCollider = new List<RaycastHit2D>();

    protected float ofset = 0.01f;
    protected ContactFilter2D contactFilter;
    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        caculatorVelocity();
    }

    protected virtual void caculatorVelocity() { }

    private void FixedUpdate()
    {
        isGround = false;
        velocity += gravityScale * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        Vector2 groundDerection = new Vector2(groundNomal.y, -1 * groundNomal.x);
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move = groundDerection * deltaPosition.x;
        doMove(move, false);
        move = Vector2.up * deltaPosition.y;
        doMove(move, true);
    }

    protected void doMove(Vector2 move, bool ymovement)
    {
        float distance = move.magnitude;
        if (distance >= miniumDistance)
        {
            //Cast này tự truyền các contactFilter và hitBuffer
            int count = _body.Cast(move, contactFilter, hitBuffer, distance + ofset);
            listCollider.Clear();
            for (int i = 0; i < count; i++)
            {
                if (hitBuffer[i].collider.isTrigger)
                {
                    continue;
                }
                listCollider.Add(hitBuffer[i]);
            }
            Vector2 currentNomal;
            foreach (var collision in listCollider)
            {
                currentNomal = collision.normal;
                if (currentNomal.y >= miniumNomalY)
                {
                    isGround = true;
                    if (ymovement)
                    {

                        groundNomal = currentNomal;
                        currentNomal.x = 0;
                    }
                }

                float checkValue = Vector2.Dot(velocity, currentNomal);

                if (checkValue < 0)
                {
                    velocity = velocity - currentNomal * checkValue;
                }

                distance = Mathf.Min(distance, collision.distance - ofset);
            }
        }
        _body.position = _body.position + move.normalized * distance;
    }
}
