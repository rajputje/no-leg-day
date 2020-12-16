using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZiplineWheel : MonoBehaviour
{
    [SerializeField] private Transform StartPoint = null;
    [SerializeField] private Transform EndPoint = null;

    [SerializeField] private LayerMask PlayerLayer = 0;

    private Rigidbody2D rb;

    private float minX;
    private float maxX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void FixedUpdate()
    {
        minX = Mathf.Min(StartPoint.position.x, EndPoint.position.x);
        maxX = Mathf.Max(StartPoint.position.x, EndPoint.position.x) - 0.2f;
        if (rb.position.x > maxX)
        {
            rb.position = new Vector2(maxX, rb.position.y);
            rb.velocity = Vector2.zero;
        }
        else if (rb.position.x < minX)
        {
            rb.position = new Vector2(minX, rb.position.y);
            rb.velocity = Vector2.zero;
        }
        if(rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (1 << collision.gameObject.layer == PlayerLayer)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
