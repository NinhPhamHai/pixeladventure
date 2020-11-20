using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public bool isOn = false;
    private Animator animator;

    public static FireController instance;

    private Coroutine currentCoroutine;

    private Coroutine turnOffCoroutine;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        animator = GetComponent<Animator>();
        if (currentCoroutine != null)
        {
            StopCoroutine(fire());
            currentCoroutine = null;
        }
        else
        {
            currentCoroutine = StartCoroutine(fire());
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("On", isOn);
        if (!isOn)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(fire());
                currentCoroutine = null;
            }
            else
            {
                currentCoroutine = StartCoroutine(fire());
            }
        }
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(2.5f);
        isOn = true;
    }

    public void turnOff()
    {
        if(turnOffCoroutine!=null)
        {
            StopCoroutine(close());
            turnOffCoroutine=null;
        }else{
            turnOffCoroutine = StartCoroutine(close());
        }
    }

    IEnumerator close()
    {
        yield return new WaitForSeconds(2.5f);
        isOn = false;
    }


}
