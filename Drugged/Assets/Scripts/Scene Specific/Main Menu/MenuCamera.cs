using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private float ySpeed = 0;
    [SerializeField] private float height = 0;
    private float yAngle = 0;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(yAngle) * height, transform.position.z);
        yAngle += ySpeed * Time.deltaTime;
    }
}
