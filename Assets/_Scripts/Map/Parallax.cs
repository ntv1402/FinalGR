using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class Parallax : MonoBehaviour
    {
        public Transform cam;
        public float relativeMove = .3f;

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(cam.position.x * relativeMove, cam.position.y * relativeMove);

        }
    }
}
