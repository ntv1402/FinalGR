using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class MeleeEnemy : MonoBehaviour
    {
        #region public variables
        public Transform rayCast;
        public LayerMask raycastMask;
        public float racastLength;
        public float attackDistance;
        public float moveSpeed;
        public float timer;
        #endregion

        #region private variables
        private RaycastHit2D hit;
        private GameObject target;
        private Animator anim;
        private float distance;
        private bool attackMode;
        private bool inRange;
        private bool cooling;
        private float inTimer;
        #endregion
        void Start()
        {
            inTimer = timer;
            anim = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {
            if (inRange)
            {
                hit = Physics2D.Raycast(rayCast.position, Vector2.left, racastLength, raycastMask);
                Raycastdebugger();
            }

            if(hit.collider != null)
            {
                EnemyLogic();
            }
            else if (hit.collider == null)
            {
                inRange = false;
            }
            if (inRange == false)
            {
                anim.SetBool("CanWalk", false);
                StopAttack();
            }
        
        }

        void EnemyLogic()
        {
            distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance <= attackDistance && cooling == false)
            {
                Attack();
            }
            else if (distance > attackDistance)
            {
                Move();
                StopAttack();
            }

            if (cooling)
            {
                anim.SetBool("Attack", false);
            }
        }

        private void OnTriggerEnter2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Player")
            {
                target = trig.gameObject;
                inRange = true;
            }
        }

        void Raycastdebugger()
        {
            if (distance > attackDistance)
            {
                Debug.DrawRay(rayCast.position, Vector2.left * racastLength, Color.red);
            }
            else if (distance < attackDistance)
            {
                Debug.DrawRay(rayCast.position, Vector2.left * racastLength, Color.green);
            }
        }

        void Move()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Melee"))
            {
                Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }

        void Attack()
        {
            timer = inTimer;
            attackMode = true;

            anim.SetBool("Attack", true);
            anim.SetBool("CanWalk", false);
        }

        void StopAttack()
        {
            cooling = false;
            attackMode = false;
            anim.SetBool("Attack", false);
        }
    }
}
