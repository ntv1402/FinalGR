using Bardent.Combat.Damage;
using Bardent.CoreSystem;
using Bardent.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class BulletDamage : MonoBehaviour //not monobehavior, or maybe create a damage script
    {
        private DamageReceiver damageReceiver;
        public FloatVariable bulletDamage;
        public DamageData data;

        private void Start()
        {
            //object reference
            data.SetAmount(bulletDamage.value);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            //TODO object reference
            damageReceiver.Damage(data);
        }
    }
}
