using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform camTransform = null;
    [SerializeField] private Vector2 size = new Vector2(), parallaxEffect = new Vector2();

    private Vector2 startPos = new Vector2();
    private Vector3 lastPos = new Vector2();

    private void Start()
    {
        startPos = transform.position;
        lastPos = startPos;
    }

    private void LateUpdate()
    {
        Vector2 deltaPos = lastPos - transform.position;
        transform.position = new Vector3(lastPos.x + deltaPos.x * parallaxEffect.x,
            lastPos.y + deltaPos.y * parallaxEffect.y, transform.position.z);

        if (camTransform.position.x > transform.position.x + size.x) transform.position = transform.position + new Vector3(size.x, 0, 0);
        else if (camTransform.position.x < transform.position.x - size.x) transform.position = transform.position - new Vector3(size.x, 0, 0);
        if (camTransform.position.y > transform.position.y + size.y) transform.position = transform.position + new Vector3(0, size.y, 0);
        else if (camTransform.position.y < transform.position.y - size.y) transform.position = transform.position - new Vector3(0, size.y, 0);

        lastPos = transform.position;
    }
}
