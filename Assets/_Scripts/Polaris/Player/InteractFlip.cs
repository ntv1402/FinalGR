using Nova;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Bardent
{
    public class InteractFlip : MonoBehaviour
    {
        public UIBlock2D uiblock;
        public TextBlock textblock, E;
        private void OnEnable()
        {
            StartCoroutine(FadeIn());
        }
        void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        IEnumerator FadeIn()
        {
            uiblock.Border.Color.a = 0;
            textblock.TMP.alpha = 0;
            //increase uiblock.Border.Color.a from 0 to 255 in 0.1 seconds
            float duration = 0.2f;
            float elapsedTime = 0f;
            float startAlpha = 0f;
            float targetAlpha = 1f;

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
                uiblock.Border.Color.a = alpha;
                textblock.TMP.alpha = alpha;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            uiblock.Border.Color.a = (byte)targetAlpha;
            
        }
    }
}
