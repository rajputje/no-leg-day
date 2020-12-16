using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticallyShiftable : MonoBehaviour
{
    [SerializeField] private float height = 0, speed = 0;
    [SerializeField] private bool isDown = false;
    private Vector3 upPos = new Vector2(), downPos = new Vector2();
    private IEnumerator currentCoroutine = null;

    private void Awake()
    {
        if (isDown)
        {
            upPos = transform.position + transform.up * height;
            downPos = transform.position;
        }
        else
        {
            upPos = transform.position;
            downPos = transform.position - transform.up * height;
        }
    }

    public void MoveUp()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = MoveTo(upPos);
        StartCoroutine(currentCoroutine);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            MoveDown();
        else if (Input.GetKeyDown(KeyCode.U))
        {
            MoveUp();
        }
    }

    private IEnumerator MoveTo(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void MoveDown()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = MoveTo(downPos);
        StartCoroutine(currentCoroutine);
    }
}
