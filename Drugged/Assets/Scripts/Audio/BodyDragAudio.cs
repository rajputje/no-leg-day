using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDragAudio : MonoBehaviour
{
    [SerializeField] private float dragVelocityThreshold = 0;
    [SerializeField] private float dropVelocityThreshold = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.sqrMagnitude > dropVelocityThreshold * dropVelocityThreshold)
        {
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.BigImpact);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.relativeVelocity.sqrMagnitude > dragVelocityThreshold * dragVelocityThreshold)
        {
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.Drag);
        }
        else
        {
            AudioManager.Instance.StopSound(AudioClipNames.Drag);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        AudioManager.Instance.StopSound(AudioClipNames.Drag);
    }
}
