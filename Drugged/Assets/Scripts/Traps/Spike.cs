using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [Tooltip("Multiplied with velocity on every fixed update")]
    [SerializeField] private float velocityMultiplier = 0;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        IKillable killable = collision.transform.root.gameObject.GetComponent<IKillable>();
        if (killable != null)
        {
            GetComponent<EdgeCollider2D>().isTrigger = true;
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.SmallSquish);
            killable.Die(CauseOfDeath.Spikes, collision.GetContact(0).point, collision.collider);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x * velocityMultiplier,
                collision.attachedRigidbody.velocity.y * velocityMultiplier);
    }
}
