using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bee bee = collision.GetComponent<Bee>();
        if (bee)
            bee.StopOscillating();
    }
}
