using UnityEngine;

namespace Bardent.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        //private AttackDetails attackDetails;

        private float speed;
        private float travelDistance;
        private float xStartPos;

        [SerializeField]
        private float gravity;
        [SerializeField]
        private float damageRadius;

        private Rigidbody2D rb;

        private bool isGravityOn;
        private bool hasHitGround;

        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private LayerMask whatIsPlayer;
        [SerializeField]
        private Transform damagePosition;
        [SerializeField]
        private GameObject hitEffectEnemy;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            rb.gravityScale = 0.0f;
            rb.velocity = transform.right * speed;

            isGravityOn = false;

            xStartPos = transform.position.x;
        }

        private void Update()
        {
            if (!hasHitGround)
            {
                //attackDetails.position = transform.position;

                if (isGravityOn)
                {
                    float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!hasHitGround)
            {
                Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
                Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

                if (damageHit)
                {
                    PlayerHealthManager playerhealthman = damageHit.GetComponent<PlayerHealthManager>();
                    KnockbackReceiver2 playerknockback = damageHit.GetComponent<KnockbackReceiver2>();
                    Vector2 knockbackDirection = (damageHit.transform.position - transform.position).normalized;

                    playerhealthman.TakeDamage(15f);
                    playerknockback.ApplyKnockback(knockbackDirection);
                    //damageHit.transform.SendMessage("Damage", attackDetails);
                    Destroy(gameObject);
                }

                if (groundHit)
                {
                    hasHitGround = true;
                    rb.gravityScale = 0f;
                    rb.velocity = Vector2.zero;
                    Destroy(gameObject);
                }


                if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
                {
                    isGravityOn = true;
                    rb.gravityScale = gravity;
                }
            }        
        }

        public void FireProjectile(float speed, float travelDistance, float damage)
        {
            this.speed = speed;
            this.travelDistance = travelDistance;
        }

        private void OnDestroy()
        {
            Instantiate(hitEffectEnemy, transform.position, Quaternion.identity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        }
    }
}
