using DG.Tweening;
using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class LoadingScreen : MonoBehaviour
    {
        private UIBlock2D splashline;
        private Color color;
        public float alphavalue1 = 1f;
        public float alphavalue2 = 0.4f;
        public float smoothTime = 0.1f;
        public float secondsBetweenFlickers;
        private float currentAlphaVelocity;
        private float targetAlpha;
        public List<Sprite> linearts = new List<Sprite>();

        public TextBlock tips;
        public List<string> tipsList = new List<string>();
        private void Awake()
        {
            splashline = GetComponent<UIBlock2D>();

            splashline.SetImage(linearts[Random.Range(0, linearts.Count)]);

            color = Color.white;
            color.a = 0.4f;
            StartCoroutine(LightFlick());
            tips.Text = tipsList[Random.Range(0, tipsList.Count)];
        }

        IEnumerator LightFlick()
        {
            while (true)
            {
                yield return new WaitForSeconds(secondsBetweenFlickers);

                targetAlpha = Random.Range(alphavalue1, alphavalue2);
            }
        }

        private void Update()
        {
            DOTween.To(() => color.a, x => color.a = x, targetAlpha, smoothTime);

            splashline.Color = color;

        }
    }
}
