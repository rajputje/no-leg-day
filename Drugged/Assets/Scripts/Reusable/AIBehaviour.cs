using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIBehaviour
{
    /// <summary>
    /// Is the aimer aiming towards the target
    /// </summary>
    /// <param name="aimingFrom">Position where the aimer is aiming from</param>
    /// <param name="aimDirection">Direction where the aimer is aiming</param>
    /// <param name="targetPosition">The target's position</param>
    /// <param name="acceptableDifferenceInAngles">The acceptable difference between their angles</param>
    /// <returns></returns>
    public static bool IsAimingAt(Vector2 aimingFrom, Vector2 aimDirection, Vector2 targetPosition, float acceptableDifferenceInAngles)
    {
        float desiredAngle = Vector2.SignedAngle(aimingFrom, targetPosition);
        float actualAngle = Vector2.SignedAngle(aimingFrom, aimDirection + aimingFrom);
        return actualAngle + acceptableDifferenceInAngles >= desiredAngle && actualAngle - acceptableDifferenceInAngles <= desiredAngle; 
    }
}
