using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Bardent
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private float shaketimer;
        private float startingIntensity;
        private float shaketimerTotal;
        // Start is called before the first frame update
        void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            Instance = this;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }

        public void ShakeCamera(float intensity, float duration)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            shaketimer = duration;
            startingIntensity = intensity;
            shaketimerTotal = duration;
        }

        private void Update()
        {
            if (shaketimer > 0)
            {
                shaketimer -= Time.deltaTime;
                if (shaketimer <= 0)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

                    Mathf.Lerp(startingIntensity, 0f, 1- (shaketimer / shaketimerTotal));
                }
            }
        }
    }
}
