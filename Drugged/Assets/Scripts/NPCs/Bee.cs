using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour, IKillable
{
    [SerializeField] private float xSpeed = 0;
    [SerializeField] private float ySpeed = 0;
    [SerializeField] private float maxHeight = 0;

    public Transform Target = null;

    [SerializeField] private ParticleSystem bloodParticleSystem_Prefab = null;

    private float currentSinAngle = 0;
    private Rigidbody2D rb;
    private Vector3 difference;
    private IEnumerator currentCoroutine;

    public void Die(CauseOfDeath causeOfDeath, Vector3 damagePosition = default, Collider2D collider = null)
    {
        ParticleSystem ps = Instantiate(bloodParticleSystem_Prefab);
        ps.transform.position = damagePosition;
        Destroy(gameObject);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MoveToTarget();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void MoveToTarget()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = Move();
        StartCoroutine(currentCoroutine);
    }
    
    private IEnumerator Move()
    {
        while (true)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, Target.position, xSpeed));
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartOscillating()
    {
        StopCoroutine(currentCoroutine);
        currentCoroutine = Oscillate();
        StartCoroutine(currentCoroutine);
    }

    public void StopOscillating()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        StopCoroutine(currentCoroutine);
    }

    private IEnumerator Oscillate()
    {
        while (true)
        {
            rb.MovePosition((Vector3)rb.position + transform.right * xSpeed + transform.up * Mathf.Sin(currentSinAngle) * maxHeight - difference);
            difference = transform.up * Mathf.Sin(currentSinAngle) * maxHeight;
            currentSinAngle += ySpeed;
            yield return new WaitForFixedUpdate();
        }
    }

}
