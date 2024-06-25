using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class EnemyHitbox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealthManager playerhealth = other.GetComponent<PlayerHealthManager>();
                KnockbackReceiver2 playerknockback = other.GetComponent<KnockbackReceiver2>();
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                playerhealth.TakeDamage(15f);
                playerknockback.ApplyKnockback(knockbackDirection);
            }
        }
    }
}
