using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        

        if (target == null)
        {
            return;
        }
        //cái này truyền vào giá trị, giới hạn nó trong khoảng max min
        //-10 xem ở trục z của camera ở unity
        Vector3 newPosition = new Vector3(Mathf.Clamp(target.position.x, 3.58f, 364.6f),
        Mathf.Clamp(target.position.y, 2.07f, 3.1f), -10);
        transform.position = newPosition;

    }
}
