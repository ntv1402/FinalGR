using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class Library : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(AudioManager.Instance.PlayMusicFade("Library", 2f));

        }

    }
}
