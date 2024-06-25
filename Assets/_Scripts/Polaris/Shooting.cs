using Bardent;
using Bardent.Projectiles;
using DG.Tweening;
using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCam;
    private Vector3 initialRotation = new Vector3(0f, 0f, 0f);
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform gun;
    public Transform bulletSpawn;
    public bool canShoot;
    public bool isShooting = false;
    private float timer;
    public PlayerController playercontroller;

    public FloatVariable shootdelay;
    public FloatVariable bulletScale;
    public FloatVariable force;
    public FloatVariable bulletTime;
    public FloatVariable damageAmount;
    public FloatVariable lifesteal;
    public FloatVariable bulletAmount;
    public float startAngle = 30f, endAngle = -30f, initAngle = 0f;


    private bool gunfacingright;
    private Animator animator;

    
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (InputSystem.Shoot() && canShoot)
        {
            StopAllCoroutines();
            UpdateGunRotation();
            canShoot = false;
            Invoke(nameof(Shoot), 0.1f);
        }

        if (!canShoot)
        {
            UpdateGunRotation();
        }

        timer += Time.deltaTime;
        if (timer >= shootdelay.value)
        {
            canShoot = true;
            timer = 0;
        }

        if (InputSystem.shootRelease())
        {
            if (!canShoot)
            {
                StopAllCoroutines();
            }
            float duration = 0.2f;
            StartCoroutine(ResetGunRotationCoroutine(duration));
        }

    }

    void UpdateGunRotation()
    {
        if (Time.timeScale > 0)
        {
            Vector3 rotation = mousePos - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);

            if (playercontroller.m_facingRight)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            }

            float gunangle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
            if (mousePos.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunangle));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunangle));
            }
        }
    }

    void Shoot()
    {
        Vector3 rotation = bulletSpawn.position - mousePos;
        Vector3 direction = mousePos - bulletSpawn.position;

        if (bulletAmount.intValue == 1)
        {
            float offsetAngle = Random.Range(-3f, 3f); // Adjust the range as needed
            direction = Quaternion.Euler(0, 0, offsetAngle) * direction;

            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            GameObject newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            newBullet.transform.localScale = Vector3.one * bulletScale.value;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, rot);

            BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
            bulletScript.InitializeBullet(force.value, bulletTime.value, damageAmount.value, bulletScale.value, lifesteal.value);
            bulletScript.direction = direction;
        }
        else
        {
            direction = Quaternion.Euler(0, 0, startAngle) * direction;
            rotation = Quaternion.Euler(0, 0, startAngle) * rotation;
            float stepAngle = (endAngle - startAngle) / (bulletAmount.intValue);
            for (int i = 0; i < bulletAmount.intValue; i++)
            {
                float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                GameObject newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                newBullet.transform.localScale = Vector3.one * bulletScale.value;
                newBullet.transform.rotation = Quaternion.Euler(0, 0, rot);

                BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
                bulletScript.InitializeBullet(force.value, bulletTime.value, damageAmount.value, bulletScale.value, lifesteal.value);
                bulletScript.direction = direction;

                // Calculate the direction based on the current angle
                direction = Quaternion.Euler(0, 0, stepAngle) * direction;
                rotation = Quaternion.Euler(0, 0, stepAngle) * rotation;

             

                // Instantiate and configure the bulle


            }
        }

        animator.SetTrigger("Shoot");
        AudioManager.Instance.PlaySound2D("Gunshot");
    }


    private IEnumerator ResetGunRotationCoroutine(float duration)
    {
        Quaternion startrotation = transform.rotation;
        float elapsedTime = 0f;
        Quaternion endRotation;

        while (elapsedTime < 1f)
        {
            if (playercontroller.m_facingRight)
            {
                endRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                endRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            transform.rotation = Quaternion.Slerp(startrotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
