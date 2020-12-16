using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VolumeButtonFader : MonoBehaviour
{
    private bool isFadedIn = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleFade()
    {
        isFadedIn = !isFadedIn;
        if (isFadedIn)
            animator.Play("FadeIn");
        else
            animator.Play("FadeOut");
    }
}
