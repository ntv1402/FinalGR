using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/ExtraJumps")]
public class ExtraJumps : PowerUps
{
    public int amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponent<PlayerController>().extraJumpCount.intValue += amount;
    }
}

