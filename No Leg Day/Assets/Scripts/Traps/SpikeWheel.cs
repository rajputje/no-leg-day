using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWheel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IKillable killable = collision.gameObject.GetComponent<IKillable>();
        if (killable == null)
            killable = collision.transform.root.gameObject.GetComponent<IKillable>();
        if (killable != null)
        {
            AudioManager.Instance.PlaySound(AudioClipNames.LoudSquish);
            killable.Die(CauseOfDeath.SpikeWheel, collision.GetContact(0).point, collision.collider);
        }
    }
}
