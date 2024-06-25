using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/LifeSteal")]
public class LifeSteal : PowerUps
{
    public float amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponentInChildren<Shooting>().lifesteal.value += amount;
    }
}

