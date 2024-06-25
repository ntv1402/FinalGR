using Bardent;
using System.Collections;
using UnityEngine;

namespace SupanthaPaul
{
    public class PlayerController : MonoBehaviour
	{
        #region Fields
        public FloatVariable speed;
        [Header("Jumping")]
		public FloatVariable jumpForce;
		[SerializeField] private float fallMultiplier;
		[SerializeField] private Transform groundCheck;
		[SerializeField] private float groundCheckRadius;
		[SerializeField] private LayerMask whatIsGround;
		public FloatVariable extraJumpCount;

		[SerializeField] private GameObject jumpEffect;
        [SerializeField] private GameObject doublejumpEffect;
        [SerializeField] private GameObject landEffect;
        [HideInInspector] public float jumpTimer = 0f;
        [HideInInspector] public float jumpCooldown = 0.4f;
        [HideInInspector] private float jumpBufferTime = 0.2f;
		[HideInInspector] private float jumpBufferCounter;


        [Header("Dashing")]
		[SerializeField] private float dashSpeed = 30f;
		[Tooltip("Amount of time (in seconds) the player will be in the dashing speed")]
		[SerializeField] private float startDashTime = 0.1f;
		[Tooltip("Time (in seconds) between dashes")]
		[SerializeField] private float dashCooldown = 0.2f;
		[SerializeField] private GameObject dashEffect;

		// Access needed for handling animation in Player script and other uses
		[HideInInspector] public bool isGrounded;
        [HideInInspector] public bool wasGrounded = false;
        [HideInInspector] public float moveInput;
		[HideInInspector] public bool canMove = true;
		[HideInInspector] public bool isDashing = false;
		[HideInInspector] public bool actuallyWallGrabbing = false;
		[HideInInspector] public bool isBackwardMoving = false;
        // controls whether this instance is currently playable or not
        [HideInInspector] public bool isCurrentlyPlayable = false;
        [HideInInspector] public bool m_facingRight = true;
        [HideInInspector] public bool isshooting = false;
        [HideInInspector] public bool isknockedBack = false;

        [Header("Wall grab & jump")]
		[Tooltip("Right offset of the wall detection sphere")]
		public Vector2 grabRightOffset = new Vector2(0.16f, 0f);
		public Vector2 grabLeftOffset = new Vector2(-0.16f, 0f);
		public float grabCheckRadius = 0.24f;
		public Vector2 wallJumpForce = new Vector2(10.5f, 18f);
		public Vector2 wallClimbForce = new Vector2(4f, 14f);

		private Rigidbody2D m_rb;
		private ParticleSystem m_dustParticle;
		private readonly float m_groundedRememberTime = 0.25f;
		private float m_groundedRemember = 0f;
		private int m_extraJumps;
		private float m_extraJumpForce;
		private float m_dashTime;
		private bool m_hasDashedInAir = false;
		private bool m_onWall = false;
		private bool m_onRightWall = false;
		private bool m_onLeftWall = false;

		private float m_dashCooldown;
        private Vector3 mousePos;
        private Camera mainCam;


        #endregion

        void Start()
		{
			// create pools for particles
			PoolManager.instance.CreatePool(dashEffect, 2);
			PoolManager.instance.CreatePool(jumpEffect, 4);
			PoolManager.instance.CreatePool(landEffect, 2);
			PoolManager.instance.CreatePool(doublejumpEffect, 2);

            // if it's the player, make this instance currently playable
            if (transform.CompareTag("Player"))
				isCurrentlyPlayable = true;

			m_extraJumps = extraJumpCount.intValue;
			m_dashTime = startDashTime;
			m_dashCooldown = dashCooldown;
			m_extraJumpForce = jumpForce.value * 0.8f;

			m_rb = GetComponent<Rigidbody2D>();
			m_dustParticle = GetComponentInChildren<ParticleSystem>();
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

		private void FixedUpdate()
		{
			// check if grounded
			isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
			var position = transform.position;
			// check if on wall
			m_onWall = Physics2D.OverlapCircle((Vector2)position + grabRightOffset, grabCheckRadius, whatIsGround)
			          || Physics2D.OverlapCircle((Vector2)position + grabLeftOffset, grabCheckRadius, whatIsGround);
			m_onRightWall = Physics2D.OverlapCircle((Vector2)position + grabRightOffset, grabCheckRadius, whatIsGround);
			m_onLeftWall = Physics2D.OverlapCircle((Vector2)position + grabLeftOffset, grabCheckRadius, whatIsGround);



			
            // calculate mouse position


            // if this instance is currently playable
            if (isCurrentlyPlayable)
			{
				if (InputSystem.Shoot())
				{
                    isshooting = true;
                }

				if (isshooting)
				{
                    if (mousePos.x > transform.position.x && !m_facingRight)
                    {
						Flip();

                    }
                    else if (mousePos.x < transform.position.x && m_facingRight)
                    {
						Flip();
                    }
                }
// horizontal movement
				if (canMove && !isknockedBack)
					m_rb.velocity = new Vector2(moveInput * speed.value, m_rb.velocity.y);
				else if (!canMove && !isknockedBack)
					m_rb.velocity = new Vector2(0f, m_rb.velocity.y);
				// better jump physics
				if (m_rb.velocity.y < 0f)
				{
					m_rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
				}

				// Flipping
				if (!m_facingRight && moveInput > 0f && !isshooting )
					Flip();
				else if (m_facingRight && moveInput < 0f && !isshooting)
					Flip();

				// Dashing logic
				if (isDashing)
				{
					if (m_dashTime <= 0f)
					{
						isDashing = false;
						m_dashCooldown = dashCooldown;
						m_dashTime = startDashTime;
						m_rb.velocity = Vector2.zero;
					}
					else
					{
						m_dashTime -= Time.deltaTime;

						if (moveInput < 0)
						{
							m_rb.velocity = Vector2.left * dashSpeed;
						}
						else if (moveInput > 0)
						{
							m_rb.velocity = Vector2.right * dashSpeed;
						}
						else if (m_facingRight)
						{
							m_rb.velocity = Vector2.right * dashSpeed;
						}
						else if (!m_facingRight)
						{
							m_rb.velocity = Vector2.left * dashSpeed;
						}
					}
				}


				// enable/disable dust particles
				float playerVelocityMag = m_rb.velocity.sqrMagnitude;
				if (m_dustParticle.isPlaying && playerVelocityMag == 0f)
				{
					m_dustParticle.Stop();
				}
				else if (!m_dustParticle.isPlaying && playerVelocityMag > 0f)
				{
					m_dustParticle.Play();
				}

				if (((m_facingRight && moveInput < 0f) || (!m_facingRight && moveInput > 0f)) && isshooting && isGrounded)
				{
					isBackwardMoving = true;
				}
				else
				{
                    isBackwardMoving = false;
                }

				if (isknockedBack)
				{
					StartCoroutine(knockedback());
				}

            }
		}

		private void Update()
        #region horizontal input
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            if (InputSystem.Shoot())
            {
                isshooting = true;
            }
            else
            {
                isshooting = false;
            }

            // horizontal input
            moveInput = InputSystem.HorizontalRaw();

			if (isGrounded)
			{
				m_extraJumps = extraJumpCount.intValue;
            }

			// grounded remember offset (for more responsive jump)
			if (isGrounded)
			{
				m_groundedRemember = m_groundedRememberTime;
			}
			else
			{
                m_groundedRemember -= Time.deltaTime;
            }

			if (!isCurrentlyPlayable) return;

			// if not currently dashing and hasn't already dashed in air once
			if (!isDashing && !m_hasDashedInAir && m_dashCooldown <= 0f)
			{
				// dash input (left shift)
				if (InputSystem.Dash())
				{
					AudioManager.Instance.PlaySound2D("Dash");
					isDashing = true;
                    // dash effect
                    if (moveInput < 0)
                    {
                        PoolManager.instance.ReuseObject(dashEffect, groundCheck.position, Quaternion.identity); // Flip the dash effect for left direction
                    }
                    else
                    {
                        PoolManager.instance.ReuseObject(dashEffect, groundCheck.position, Quaternion.Euler(0, 180, 0)); // Keep the dash effect as is for right direction
                    }
                    // if player in air while dashing
                    if (!isGrounded)
					{
						m_hasDashedInAir = true;
					}
					// dash logic is in FixedUpdate
				}
			}
			m_dashCooldown -= Time.deltaTime;
			
			// if has dashed in air once but now grounded
			if (m_hasDashedInAir && isGrounded)
				m_hasDashedInAir = false;
            #endregion

            #region Jumping
            // Jumping
			if (InputSystem.Jump())
			{
                jumpBufferCounter = jumpBufferTime;
            }
			else
			{
                jumpBufferCounter -= Time.deltaTime;
            }

            if (InputSystem.Jump() && m_extraJumps > 0 && !isGrounded)	// extra jumping
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, m_extraJumpForce); ;
				m_extraJumps--;
				// jumpEffect
				PoolManager.instance.ReuseObject(doublejumpEffect, groundCheck.position, Quaternion.Euler(0,0,90));
				AudioManager.Instance.PlaySound2D("Jump");
            }
			else if(jumpBufferCounter > 0f && (isGrounded || m_groundedRemember > 0f))	// normal single jumping
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce.value);
				// jumpEffect
				PoolManager.instance.ReuseObject(jumpEffect, groundCheck.position, Quaternion.identity);
                jumpTimer = jumpCooldown;
                jumpBufferCounter = 0f;
                AudioManager.Instance.PlaySound2D("Jump");
            }
		
			
            #endregion
            if (InputSystem.JumpRelease() && m_rb.velocity.y > 0f)
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y * 0.75f);
				m_groundedRemember = 0f;
            }

			if (!wasGrounded && isGrounded)
			{
				PoolManager.instance.ReuseObject(landEffect, groundCheck.position, Quaternion.identity);
                // You can also play the landing sound effect here if desired
                // Example: AudioManager.PlaySound("LandingSound");
            }

			wasGrounded = isGrounded;

        }


		private IEnumerator knockedback()
		{
			yield return new WaitForSeconds(0.2f);
            isknockedBack = false;
        }
        public void Flip()
		{
			m_facingRight = !m_facingRight;
			transform.Rotate(0, 180, 0);
		}

		public void StandStill()
		{
			m_rb.velocity = new Vector2(0f, 0);
			isCurrentlyPlayable = false;    
        }


		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
			Gizmos.DrawWireSphere((Vector2)transform.position + grabRightOffset, grabCheckRadius);
			Gizmos.DrawWireSphere((Vector2)transform.position + grabLeftOffset, grabCheckRadius);
		}
	}

}
