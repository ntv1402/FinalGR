using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupanthaPaul;
using Bardent;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public FloatVariable health, maxHealth;
    public GameObject blackscreen;
    public bool canTakeDamage = true;
    private PlayerController m_controller;
    private Animator animator;
    private Rigidbody2D m_rb;
    private GameObject gun;
    private BoxCollider2D m_collider;
    private CameraCullingMask m_camcull;
    private bool dead = false;

    private SpriteRenderer[] _sr;
    private Material[] _materials;

    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        canTakeDamage = true;
        m_controller = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<Shooting>().gameObject;
        m_collider = GetComponent<BoxCollider2D>();
        m_camcull = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCullingMask>();

        _sr = GetComponentsInChildren<SpriteRenderer>();
        InitFlash();
    }

    private void InitFlash()
    {
        _materials = new Material[_sr.Length];
        for (int i = 0; i < _sr.Length; i++)
        {
            _materials[i] = _sr[i].material;
        }
    }
    private void Update()
    {
        if (m_controller.isDashing == true)
        {
            canTakeDamage = false;
            StartCoroutine(DamageFlash());
        }
        if (health.value >= maxHealth.value)
        {
            health.value = maxHealth.value;
        }
        
    }

    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {
            health.value -= amount;
            StartCoroutine(StunPlayer());
            if (health.value > 0)
            {
                AudioManager.Instance.PlaySound2D("Damaged");
            }
            if (health.value <= 0)
            {
                health.value = 0;
                //fucking dies
                Die(); // Call the method that handles the player's death
                m_collider.enabled = false;

                StartCoroutine(AudioManager.Instance.StopMusicFade(1f));
            }
            if ((dead == false) && (m_controller.isDashing == false))
            {
                CameraShake.Instance.ShakeCamera(1f, 0.2f);
                StartCoroutine(DamageFlash());
            }
        }
    }

    private void Die()
    {
        // Add code here to handle the player's death
        // For example, you can play an animation, show a game over screen, or respawn the player
        canTakeDamage = false;
        Destroy(gun);
        blackscreen.SetActive(true);
        m_camcull.DyingCull();
        
        dead = true;
        animator.SetBool("isDead", true);
        m_rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("Player died");
        // ...
        AudioManager.Instance.StopSound2D();
        AudioManager.Instance.PlaySound2D("PlayerDeath");
    }


    private IEnumerator StunPlayer()
    {
        m_controller.canMove = false;
        yield return new WaitForSeconds(0.2f);
        m_controller.canMove = true;
    }

    private IEnumerator DamageFlash()
    {
        Debug.Log("flash");
        for (int i = 0; i < _materials.Length; i++)
        { _materials[i].SetInt("_Hit", 1); }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < _materials.Length; i++)
        { _materials[i].SetInt("_Hit", 0); }
        canTakeDamage = true;
    }
}
