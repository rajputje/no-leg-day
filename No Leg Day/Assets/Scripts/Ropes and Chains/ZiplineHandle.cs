using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineHandle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D WheelRb = null;

    [SerializeField] private LayerMask playerLayer = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == playerLayer)
        {
            WheelRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            this.enabled = false;
        }
    }
}
