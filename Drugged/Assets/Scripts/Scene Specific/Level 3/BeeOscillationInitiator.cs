using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeOscillationInitiator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bee bee = collision.gameObject.GetComponent<Bee>();
        if (bee)
        {
            bee.StartOscillating();
        }
    }
}
