using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BulletSize")]
public class BulletSize : PowerUps
{
    public float amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponentInChildren<Shooting>().bulletScale.value += amount;
    }
}