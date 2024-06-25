using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class BossLevelThings : MonoBehaviour
    {

        public void KillPlayer()
        {
            PlayerHealthManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
            player.TakeDamage(5000f);
        }

        public void BossSFX()
        {
            AudioManager.Instance.PlaySound2D("Boss");
        }

        public void BossSFX2()
        {
            AudioManager.Instance.PlaySound2D("BossEnd");
        }
    }
}
