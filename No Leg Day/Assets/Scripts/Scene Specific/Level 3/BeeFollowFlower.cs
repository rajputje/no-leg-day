using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFollowFlower : MonoBehaviour
{
    [SerializeField] private Transform flower = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bee bee = collision.gameObject.GetComponent<Bee>();
        if (bee)
        {
            bee.Target = flower;
            bee.StopOscillating();
            bee.MoveToTarget();
        }
    }
}
