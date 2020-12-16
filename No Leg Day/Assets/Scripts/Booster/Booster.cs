using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float upSpeed = 0;
    [SerializeField] private float resetForce = 0;
    [SerializeField] private float upDistance = 0;
    [SerializeField] private float downDistance = 0;

    private SliderJoint2D sliderJoint2D;
    private Rigidbody2D rb;

    private Vector2 initialPos;
    private bool isPushing = false;
    private bool IsDown => Vector2.Dot(transform.up, rb.position - initialPos) < 0;
    private float SquareDistanceFromInitialPos => Vector2.SqrMagnitude(rb.position - initialPos);
    private const float AcceptableDifferenceFromInitialPos = 0.01f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sliderJoint2D = GetComponent<SliderJoint2D>();
        sliderJoint2D.connectedAnchor = transform.position - transform.up * downDistance;
        initialPos = rb.position;
    }

    private void FixedUpdate()
    { 
        float sqMag = SquareDistanceFromInitialPos;
        if (!isPushing && sqMag >= downDistance * downDistance && IsDown)
        {
            StartCoroutine(Push());
        }
        else if(sqMag >= AcceptableDifferenceFromInitialPos)
        {
            ResetToInitialPos();
        }
    }

    private IEnumerator Push()
    {
        AudioManager.Instance.PlaySoundIfNotPlaying(AudioClipNames.BoosterUp);
        isPushing = true;
        while (IsDown || SquareDistanceFromInitialPos < upDistance * upDistance)
        {
            rb.velocity = upSpeed * transform.up;
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = Vector2.zero;
        isPushing = false;
    }

    private void ResetToInitialPos()
    {
        rb.AddForce((IsDown) ? resetForce * transform.up : resetForce * -transform.up);
    }
}
