using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadScreenController : MonoBehaviour
{
    // Start is called before the first frame update

    public static GamePadScreenController instance;



    public Dictionary<int,int> dictionary = new Dictionary<int, int>();

    void Start()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leftClick()
    {
        Debug.Log("Left click");
    }

}
