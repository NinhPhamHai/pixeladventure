using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LongClickListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool pointerDown;
    private float pointerDownTime;
    public float requiteHoldTime;
    public UnityEvent onLongClick;

    [SerializeField]
    private Image fillImage;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pointerDown)
        {
            pointerDownTime += Time.deltaTime;
            if(pointerDownTime>requiteHoldTime)
            {
                if(onLongClick!=null)
                {
                    onLongClick.Invoke();
                }
                reset();
            }
            fillImage.fillAmount = pointerDownTime/requiteHoldTime;
        }
    }

    private void reset()
    {
        pointerDown = false;
        pointerDownTime = 0;
        fillImage.fillAmount = pointerDownTime/requiteHoldTime;
    }
}
