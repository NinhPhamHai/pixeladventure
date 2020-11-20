using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxTrampoline : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            TrampolineController trampoline = transform.parent.GetComponent<TrampolineController>();
            trampoline.collisionFromBox(other);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            TrampolineController trampoline = transform.parent.GetComponent<TrampolineController>();
            trampoline.collisionExitFromBox(other);
        }
    }
}
