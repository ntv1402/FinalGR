using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SupanthaPaul;

public class ScriptableObjectReset: MonoBehaviour
{
    public FloatVariable plHealth, plMaxHealth, extrajumpcount, jumpforce, bldamage, blscale, blspeed, bltime, lifesteal, shootdelay, plspeed, bulletAmount;
    public static ScriptableObjectReset instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetAll()
    {
        plHealth.value = 100;
        plMaxHealth.value = 100;
        extrajumpcount.intValue = 0;
        jumpforce.value = 20.3f;
        plspeed.value = 7f;


        bldamage.value = 15;
        blscale.value = 1;
        blspeed.value = 17f;
        bltime.value = 1f;
        lifesteal.value = 0;
        shootdelay.value = 0.7f;
        bulletAmount.intValue = 1;
    }

    public void Cheat()
    {
        bulletAmount.intValue += 2;
        blspeed.value += 7f;
        shootdelay.value -= 0.2f;
        plspeed.value += 3f;
    }


}

