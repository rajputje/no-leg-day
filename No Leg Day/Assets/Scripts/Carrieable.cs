using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrieable : MonoBehaviour
{
    private bool isBeingCarried = false;
    private bool IsColliding => collisionCount > 0;
    private int collisionCount = 0;

    public void Carry(FistController joystick, Rigidbody2D torsoRb, Transform forcePoint, float torsoForce)
    {
        isBeingCarried = true;
        gameObject.layer = 8;
        StartCoroutine(AddForceToTorso(joystick, torsoRb, forcePoint, torsoForce));
    }

    public void Uncarry()
    {
        isBeingCarried = false;
        gameObject.layer = 9;
    }

    private IEnumerator AddForceToTorso(FistController joystick, Rigidbody2D torsoRb, Transform forcePoint, float torsoForce)
    {
        while (isBeingCarried)
        {
            if (IsColliding)
            {
                torsoRb.AddForceAtPosition(torsoForce * -joystick.Direction, forcePoint.position);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
    }
}
