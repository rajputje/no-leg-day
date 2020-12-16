using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Performs an action on the user's touch. 
/// This gameobject starts listening for touch when a gameobject of the specified physics layer is within its trigger.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class InteractOnTouch : MonoBehaviour
{
    [SerializeField] protected Collider2D TouchCollider = null;
    [SerializeField] protected Camera Cam = null;
    [Tooltip("The layer this gameobject will interact with")]
    [SerializeField] protected LayerMask InteractionTriggeringLayer = 0;
    private IEnumerator inputCoroutine = null;
    protected bool IsTakingInput => inputCoroutine != null;
    private int interactionTriggeringObjectsInsideTrigger = 0;

    private bool IsTouchValid(Vector2 screenPosition)
    {
        Vector2 position = Cam.ScreenToWorldPoint(screenPosition);
        return TouchCollider.OverlapPoint(position);
    }

    private IEnumerator CheckForInput()
    {
        while (true)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && IsTouchValid(Input.mousePosition))
            {
                Interact();
            }
#else
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && IsTouchValid(touch.position))
                {
                    Interact();
                }
            }
#endif
            yield return null;
        }
    }

    protected abstract void Interact();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == InteractionTriggeringLayer)
        {
            interactionTriggeringObjectsInsideTrigger++;
            if (inputCoroutine == null)
            {
                inputCoroutine = CheckForInput();
                StartCoroutine(inputCoroutine);
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == InteractionTriggeringLayer)
            interactionTriggeringObjectsInsideTrigger--;
        if (inputCoroutine != null && interactionTriggeringObjectsInsideTrigger == 0)
        {
            StopCoroutine(inputCoroutine);
            inputCoroutine = null;
        }
    }
}
