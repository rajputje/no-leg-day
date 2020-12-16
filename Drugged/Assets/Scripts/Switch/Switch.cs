using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject lever = null;
    [SerializeField] private UnityEvent offEvent = null;
    [SerializeField] private UnityEvent onEvent = null;
    private bool isOff = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == lever)
        {
            if (Vector2.SignedAngle(transform.up, collision.transform.up) < 0)
            {
                if (!isOff)
                {
                    offEvent.Invoke();
                    isOff = true;
                }
            }
            else
            {
                if (isOff)
                {
                    onEvent.Invoke();
                    isOff = false;
                }
            }
        }
    }

}
