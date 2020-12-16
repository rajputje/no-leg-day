using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfMap : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(1 << collision.gameObject.layer == playerLayer)
        {
            IKillable killable = collision.gameObject.GetComponent<IKillable>();
            if (killable == null)
                collision.transform.root.gameObject.GetComponent<IKillable>();
            if(killable != null)
            {
                killable.Die(CauseOfDeath.FellOutOfMap, default, collision.collider);
                AudioManager.Instance.PlaySound(AudioClipNames.LoudSquish);
            }
        }
    }
}
