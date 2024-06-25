using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PowerUps
{
    public float amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponent<PlayerController>().speed.value += amount;
    }
}

