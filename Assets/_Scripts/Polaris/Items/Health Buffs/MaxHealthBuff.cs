using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MaxHealthBuff")]
public class MaxHealthBuff : PowerUps
{
    public float amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponent<PlayerHealthManager>().health.value += amount;
        Player.GetComponent<PlayerHealthManager>().maxHealth.value += amount;
    }
}
