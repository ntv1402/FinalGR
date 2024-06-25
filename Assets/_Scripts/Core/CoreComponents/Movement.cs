using System.Collections;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB { get; private set; }

        public int FacingDirection { get; private set; }

        public bool CanSetVelocity { get; set; }

        public Vector2 CurrentVelocity { get; private set; }

        private Vector2 workspace;

        private bool isKnockedBack = false;

        protected override void Awake()
        {
            base.Awake();

            RB = GetComponentInParent<Rigidbody2D>();

            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = RB.velocity;
        }

        #region Set Functions

        public void SetVelocityZero()
        {
            workspace = Vector2.zero;        
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            if (!isKnockedBack)
            {
                workspace = direction * velocity;
                SetFinalVelocity();
            }
        }

        public void SetVelocityX(float velocity)
        {
            if (!isKnockedBack)
            {
                workspace.Set(velocity, CurrentVelocity.y);
                SetFinalVelocity();
            }
        }

        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                RB.velocity = workspace;
                CurrentVelocity = workspace;
            }        
        }

        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public void Flip()
        {
            FacingDirection *= -1;
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        #endregion

        public void ApplyKnockback(Vector2 direction, float knockbackForce, float knockbackDuration)
        {
            RB.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            // Start a coroutine to reset the knockback after a duration
            if (!isKnockedBack)
            {
                isKnockedBack = true;
                StartCoroutine(ResetKnockback(knockbackDuration));
            }
        }

        private IEnumerator ResetKnockback(float duration)
        {
            yield return new WaitForSeconds(duration);
            isKnockedBack = false;
            SetVelocityZero();
        }
    }
}
