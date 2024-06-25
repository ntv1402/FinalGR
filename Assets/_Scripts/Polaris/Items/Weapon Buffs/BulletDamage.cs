using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    [CreateAssetMenu(menuName = "PowerUps/BulletDamage")]
    public class BulletDamageBuff : PowerUps
    {
        public float amount;
        public override void Apply(GameObject Player)
        {
            Player.GetComponentInChildren<Shooting>().damageAmount.value += amount;
        }
    }
}
