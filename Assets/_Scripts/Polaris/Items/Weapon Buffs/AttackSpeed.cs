using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/AttackSpeed")]
public class AttackSpeed : PowerUps
{
    public float percentage;
    public override void Apply(GameObject Player)
    {
        Shooting shootingComponent = Player.GetComponentInChildren<Shooting>();
        shootingComponent.shootdelay.value -= shootingComponent.shootdelay.value * (percentage / 100f);
    }
}

