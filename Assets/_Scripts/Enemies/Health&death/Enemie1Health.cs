using Bardent.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class EnemyHealth : MonoBehaviour
    {
        private Entity enemy;
        public D_Entity entitydata;
        public float enemyMaxHealth;
        public float enemyHealth;
        private Animator anim;
        private SpriteRenderer _sr;
        private Material _material;


        private void Start()
        {
            enemy = GetComponent<Entity>();
            anim = GetComponent<Animator>();
            enemyMaxHealth = entitydata.maxHealth;
            enemyHealth = enemyMaxHealth;
            _sr = GetComponent<SpriteRenderer>();
            InitFlash();
        }



        private void Die()
        {
            enemy.EnemyDead();
        }

        public void TakeDamage(float amount)
        {
            if (enemyHealth > 0)
            {
                enemyHealth -= amount;

                //set trigger damage

                if (enemyHealth <= 0)
                {
                    enemyHealth = 0;
                    if (enemy != null)
                    { Die(); }
                    GameManager.instance.EnemyDefeated();
                }
                else
                {
                    StartCoroutine(Flasher());
                    anim.SetTrigger("damaged");
                }
            }
        }

        private void InitFlash()
        {
            _material = _sr.material;
        }


        private IEnumerator Flasher()
        {
            Debug.Log("flasher");
            _material.SetInt("_Hit", 1);

            yield return new WaitForSeconds(0.2f);

            _material.SetInt("_Hit", 0);
        }
    }
}

