using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPlayerEnter = null;
    [SerializeField] private LayerMask playerLayer = 0;
    private bool invoked;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene arg0, LoadSceneMode arg1)
    {
        invoked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invoked && 1 << collision.gameObject.layer == playerLayer)
        {
            invoked = true;
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.LevelFinished);
            OnPlayerEnter.Invoke();
        }
    }

}
