using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    void Die(CauseOfDeath causeOfDeath, Vector3 damagePosition = new Vector3(), Collider2D collider = null);
}