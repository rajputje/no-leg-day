using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer = 0;
    [SerializeField] private TextMeshProUGUI checkPointTMP = null;
    [SerializeField] private float lerpSpeed = 0.05f;

    private bool hasEntered = false;

    public delegate void CheckPointReachedHandler(Vector3 position);
    public static CheckPointReachedHandler CheckPointReached;

    private void OnEnable()
    {
        GameManager.RestartingLevel += OnRestartingLevel;
    }

    private void OnDisable()
    {
        GameManager.RestartingLevel -= OnRestartingLevel;
    }

    private void OnRestartingLevel()
    {
        hasEntered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEntered && 1 << collision.gameObject.layer == playerLayer)
        {
            hasEntered = true;
            StartCoroutine(AnimateCheckpoint());
            CheckPointReached.Invoke(transform.position);
        }
    }

    private IEnumerator AnimateCheckpoint()
    {
        checkPointTMP.gameObject.SetActive(true);
        checkPointTMP.alpha = 0;
        while (checkPointTMP.alpha < 0.99f)
        {
            checkPointTMP.alpha = Mathf.Lerp(checkPointTMP.alpha, 1, lerpSpeed);
            yield return null;
        }
        while (checkPointTMP.alpha > 0.05f)
        {
            checkPointTMP.alpha = Mathf.Lerp(checkPointTMP.alpha, 0, lerpSpeed);
            yield return null;
        }
        checkPointTMP.gameObject.SetActive(false);
    }
}
