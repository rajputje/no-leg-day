using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer = 0;
    private bool wasTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasTouched && 1 << collision.gameObject.layer == playerLayer)
        {
            wasTouched = true;
            CoinManager.Instance.CoinBalance += 1;
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.CoinCollected);
            gameObject.SetActive(false);
        }
    }
}
