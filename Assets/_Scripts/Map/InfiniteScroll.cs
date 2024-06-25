using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class InfiniteScroll : MonoBehaviour
    {
        public float scrollSpeed = 2.0f;
        private float startPosition;
        private float loopWidth;

        void Start()
        {
            startPosition = transform.position.x;
            // Calculate the width of your looping element
            loopWidth = GetComponent<SpriteRenderer>().bounds.size.x /2;
        }

        void Update()
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
            if (transform.position.x < startPosition - loopWidth)
            {
                transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
            }
        }
    }
}
