using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class CameraCullingMask : MonoBehaviour
    {
        private Camera _camera;
        public LayerMask playerLayer;
        public PolygonCollider2D camerabound;
        private CinemachineVirtualCamera playercamera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            playercamera = GetComponentInChildren<CinemachineVirtualCamera>();
        }
        public void DyingCull()
        {
            _camera.cullingMask = playerLayer.value;
            //increase ortho size of playercamera
        }
    }
}
