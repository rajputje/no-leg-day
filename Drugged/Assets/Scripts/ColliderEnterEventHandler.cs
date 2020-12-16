using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEnterEventHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent OnColliderEnter = null;
    [SerializeField] private LayerMask colliderLayer = 0;
    [SerializeField] private bool invokeOnce = true;
    private bool canInvoke = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canInvoke && 1 << collision.gameObject.layer == colliderLayer)
        {
            if (invokeOnce)
                canInvoke = false;
            OnColliderEnter.Invoke();
        }
    }
}
