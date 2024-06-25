using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Heal")]
public class Heal : PowerUps
{
    public float amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponent<PlayerHealthManager>().health.value += amount;
    }
}
