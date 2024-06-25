using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Bardent
{
    public class LightFlicker : MonoBehaviour
    {
        [SerializeField] float firstVariable;
        [SerializeField] float secondVariable;
        [SerializeField] float secondsBetweenFlickers;
        [SerializeField] float intensity1;
        [SerializeField] float intensity2;

        Light2D myLight;

        // Smoothing variables
        [SerializeField] float smoothTime = 0.1f;
        private float currentOuterRadiusVelocity;
        private float currentIntensityVelocity;

        private float targetOuterRadius;
        private float targetIntensity;

        void Start()
        {
            myLight = GetComponent<Light2D>();
            StartCoroutine(LightFlick());
        }

        IEnumerator LightFlick()
        {
            while (true)
            {
                yield return new WaitForSeconds(secondsBetweenFlickers);

                // Set random target values
                targetOuterRadius = Random.Range(firstVariable, secondVariable);
                targetIntensity = Random.Range(intensity1, intensity2);
            }
        }

        void Update()
        {
            // Smoothly transition to the new target values
            myLight.pointLightOuterRadius = Mathf.SmoothDamp(myLight.pointLightOuterRadius, targetOuterRadius, ref currentOuterRadiusVelocity, smoothTime);
            myLight.intensity = Mathf.SmoothDamp(myLight.intensity, targetIntensity, ref currentIntensityVelocity, smoothTime);
        }
    }
}