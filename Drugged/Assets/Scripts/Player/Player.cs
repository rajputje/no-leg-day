using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private NewFistMovements lFistMovement_Script = null;
    [SerializeField] private NewFistMovements rFistMovement_Script = null;

    public NewFistMovements LFistMovements => lFistMovement_Script;
    public NewFistMovements RFistMovements => rFistMovement_Script;

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.relativeVelocity.sqrMagnitude > 0.5f)
    //    {
    //        Debug.Log("Sound 2:" + collision.relativeVelocity.sqrMagnitude);
    //        AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.Drag);
    //    }
    //    else
    //    {
    //        AudioManager.Instance.StopSound(AudioClipNames.Drag);
    //    }
    //}
}