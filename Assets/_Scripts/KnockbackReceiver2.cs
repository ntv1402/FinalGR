using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class KnockbackReceiver2 : MonoBehaviour
    {
        public float knockbackForce = 0.5f; // Adjust the force as needed
        private Rigidbody2D rb;
        private PlayerController pcontrol;

        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            pcontrol = GetComponent<PlayerController>();
        }
        // Apply knockback force when called
        public void ApplyKnockback(Vector2 direction)
        {
            if (rb != null)
            {
                pcontrol.isknockedBack = true;
                // Apply the force in the specified direction
                rb.velocity = Vector2.zero; // Reset velocity to prevent accumulation
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                
            }
        }
    }
}
