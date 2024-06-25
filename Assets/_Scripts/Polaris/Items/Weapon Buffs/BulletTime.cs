using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    [CreateAssetMenu(menuName = "PowerUps/BulletTime")]
    public class BulletTimeBuff : PowerUps
    {
        public float amount;
        public override void Apply(GameObject Player)
        {
            Player.GetComponentInChildren<Shooting>().bulletTime.value += amount;
        }
    }
}
