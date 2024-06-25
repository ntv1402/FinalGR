using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class AnimationEvents : MonoBehaviour
    {
        public void PlayerfuckingDiessss()
        {
            GameManager.instance.PlayerFuckingDied();
        }

        public void PlayWalkSounds()
        {
            AudioManager.Instance.PlaySound2D("Walk");
        }

        public void PlayJumpSounds()
        {
            AudioManager.Instance.PlaySound2D("Jump");
        }

        public void PlayFallSound()
        {
            AudioManager.Instance.PlaySound2D("Fall");
        }
    }
}
