using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointAction : MonoBehaviour
{
    public float thressholdOffset = 0.1f;
    public Transform pointA, pointB;

    public float speed;
    public bool fromAToB = false;
    public SpriteRenderer charactorSprite;

    

    public bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(speed * Time.deltaTime * (fromAToB ? 1f : -1f), 0);
        charactorSprite.flipX = fromAToB;
        transform.Translate(move);
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
        }
    }
}
