using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;
    [SerializeField] private Collider2D attackHitbox;

    private void TriggerAttack()
    {
        attackHitbox.enabled = true;
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackHitbox.enabled = false;
        attackState.FinishAttack();
    }

    private void MonsterSound()
    {
        AudioManager.Instance.PlaySound2D("MonsterShoot");
    }

    private void ShootSound()
    {
        AudioManager.Instance.PlaySound2D("Crackle");
    }

    private void MonsterMeleeSound()
    {
        AudioManager.Instance.PlaySound2D("MonsterMelee");
    }

    private void PigDeathSound()
    {
        AudioManager.Instance.PlaySound2D("PigDeath");
    }

    private void GobDeathSound()
    {
        AudioManager.Instance.PlaySound2D("GobDeath");
    }

    private void DodgeSound()
    {
        AudioManager.Instance.PlaySound2D("Dash");
    }
}
