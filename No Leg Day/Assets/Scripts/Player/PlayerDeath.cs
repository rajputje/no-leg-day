using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDeath : MonoBehaviour, IKillable
{
    private Player player;
    [SerializeField] private ParticleSystem spikeBloodPS_Prefab = null;
    [SerializeField] private ParticleSystem spikeWheelBloodPS_Prefab = null;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Die(CauseOfDeath causeOfDeath, Vector3 damagePosition, Collider2D collider)
    {
        if (!IsDead)
        {
            IsDead = true;
            DisableFists(GetComponent<Player>().RFistMovements, player.LFistMovements);
        }
        switch (causeOfDeath)
        {
            case CauseOfDeath.Spikes:
                DieFromSpikes(damagePosition, collider);
                break;
            case CauseOfDeath.SpikeWheel:
                DieFromSpikeWheel(damagePosition, collider);
                break;
            case CauseOfDeath.FellOutOfMap:
                DieFromSpikeWheel(collider.transform.position, collider);
                break;
        }
    }

    private void DieFromSpikes(Vector3 damagePosition, Collider2D collider)
    {
        ParticleSystem ps = Instantiate(spikeBloodPS_Prefab, collider.gameObject.transform);
        ps.transform.position = damagePosition;
        ps.Play();
    }

    private void DieFromSpikeWheel(Vector3 damagePosition, Collider2D collider)
    {
        ParticleSystem ps = Instantiate(spikeWheelBloodPS_Prefab);
        ps.transform.position = damagePosition;
        ps.Play();
        collider.gameObject.SetActive(false);
    }

    private void DisableFists(NewFistMovements rFistMovement_Script, NewFistMovements lFistMovement_Script)
    {
        rFistMovement_Script.Disable();
        lFistMovement_Script.Disable();
    }
}
