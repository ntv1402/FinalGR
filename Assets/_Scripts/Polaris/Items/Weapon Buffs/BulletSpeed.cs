using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    [CreateAssetMenu(menuName = "PowerUps/BulletSpeed")]
    public class BulletSpeedBuff : PowerUps
    {
        public float amount;

        public override void Apply(GameObject Player)
        {
            Player.GetComponentInChildren<Shooting>().force.value += amount;
        }
    }
}
