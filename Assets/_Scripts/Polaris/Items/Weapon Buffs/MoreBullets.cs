using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    [CreateAssetMenu(menuName = "PowerUps/MoreBullets")]
    public class MoreBullets : PowerUps
    {
        public int amount;
        public override void Apply(GameObject Player)
        {
            Player.GetComponentInChildren<Shooting>().bulletAmount.intValue += amount;
        }
    }
}
