using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/JumpForce")]
public class JumpForce : PowerUps
{
    public float amount;
    public override void Apply(GameObject Player)
    {
        Player.GetComponent<PlayerController>().jumpForce.value += amount;
    }
}

