using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class HitEffect : MonoBehaviour
{

    private void Audioplay()
    {
        AudioManager.Instance.PlaySound2D("BulletHit");
    }

    private void Bye()
    {
        Destroy(gameObject);
    }
}

