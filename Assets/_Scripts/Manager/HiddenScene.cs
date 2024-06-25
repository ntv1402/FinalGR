using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bardent
{
    public class HiddenScene : MonoBehaviour
    {
        public void WalkSect1()
        {
            SceneManager.LoadScene(10);
        }

        public void WalkSect2() 
        { 
            SceneManager.LoadScene(11); 
        }

        public void WalkSect3()
        {
            SceneManager.LoadScene(12);
        }

        public void EnemySect2()
        {
            SceneManager.LoadScene(13);
        }

        public void QuittoMain()
        {
            SceneManager.LoadScene(0);
        }
    }
}
