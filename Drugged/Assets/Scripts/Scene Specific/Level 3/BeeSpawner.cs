using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private GameObject bee = null;
    [SerializeField] private LayerMask playerLayer = 0;
    private bool isSpawning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isSpawning && 1 << collision.gameObject.layer == playerLayer)
        {
            isSpawning = true;
            InvokeRepeating("Spawn", 0, spawnInterval);
        }
    }

    private void Spawn()
    {
        Instantiate(bee, transform.position, Quaternion.identity, transform).SetActive(true);
    }
}
