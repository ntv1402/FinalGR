using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class MainMenu : MonoBehaviour
    {
        public void Start()
        {
            StartCoroutine(AudioManager.Instance.PlayMusicFade("MainMenu", 1.5f));
        }
        public void StartGame()
        {
            GameManager.instance.PlayerFuckingDied();
        }

        public void Quit()
        {
            //turn off software
            Application.Quit();
        }
        #region SFXs
        public void SFXsHover()
        {
            //play hover sound
            AudioManager.Instance.PlaySound2D("ButtonHover");
        }

        public void SFXsClicked()
        {
            AudioManager.Instance.PlaySound2D("ButtonPressed");
        }

        #endregion
    }
}
