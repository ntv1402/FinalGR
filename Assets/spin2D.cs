using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class spin2D : MonoBehaviour
    {
        private void Update()
        {
            transform.DORotate(new Vector3(0, 0, 360), 300f, RotateMode.FastBeyond360);
        }
    }
}
