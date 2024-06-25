using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bardent.Combat.Damage;
using Bardent.CoreSystem;

namespace Bardent.Projectiles
{
    public class BulletScript : MonoBehaviour
    {
        private Vector3 mousePos;
        private Camera mainCam;
        private Rigidbody2D rb;
        private float force;
        private float bulletTime;
        private float damageAmount;
        private EnemyHealth enemyHealth;
        public GameObject hitEffect;
        private float hitEffectScale;
        private float lifesteal;
        private PlayerHealthManager playerHealth;
        public Vector3 direction;

        public void InitializeBullet(float force, float bulletTime, float damageAmount, float hitEffectScale, float lifeSteal)
        {
            this.force = force;
            this.bulletTime = bulletTime;
            this.damageAmount = damageAmount;
            this.hitEffectScale = hitEffectScale;
            this.lifesteal = lifeSteal;
        }

        // Start is called before the first frame update
        void Start()
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();

            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }

        // Update is called once per frame
        void Update()
        {
            Destroy(gameObject, bulletTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the collision is with an object tagged as "Enemy"
            if (other.CompareTag("Enemy"))
            {
                // Try to get the EnemyHealth component from the collided object
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

                // Check if the enemyHealth component is not null before calling TakeDamage
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                    // Check if the lifesteal chance is successful
                    if (Random.value < 0.5f)
                    {
                        // Apply lifesteal only when the chance succeeds
                        playerHealth.health.value += lifesteal;
                    }
                }


                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                Movement movement = other.GetComponentInChildren<Movement>();
                if (movement != null)
                {
                    movement.ApplyKnockback(knockbackDirection, 7f, 0.2f);//fix drections
                }


                Destroy(gameObject);
            }
            else if (other.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            //instantantiate hit effect
            GameObject newHit = Instantiate(hitEffect, transform.position, Quaternion.identity);
            newHit.transform.localScale = Vector3.one * hitEffectScale;
        }
    }
    
}
