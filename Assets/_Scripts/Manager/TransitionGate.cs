using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG;
using DG.Tweening;

namespace SupanthaPaul
{
    public class TransitionGate : MonoBehaviour
    {
        private bool transitionAllowed;
        private Collider2D collidingObject;
        private Animator animator;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collidingObject = collision;
            if (collidingObject.CompareTag("Player"))
            {
                transitionAllowed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            transitionAllowed = false;
        }

        private void Start()
        {

            animator = GetComponent<Animator>();
            if (animator == null)
            {
                return;
            }
            if (animator != null)
            {
                transform.localScale = new Vector3(0, 0, 0);
                Vector3 _scaleTo = new Vector3(0.7f, 0.7f, 0.7f);
                transform.DOScale(_scaleTo, 1.5f)
                    .SetEase(Ease.InOutSine);
                AudioManager.Instance.PlayAmbient("Gate", 0.1f);

            }
        }

        private void OnDestroy()
        {
            AudioManager.Instance.StopAmbient();
        }

        private void Update()
        {
            if (transitionAllowed && InputSystem.Interact())
            {
                StartCoroutine(TransitionWithCooldown());
            }
        }

        private IEnumerator TransitionWithCooldown()
        {
            // Prevent further transitions during the cooldown.
            transitionAllowed = false;

            // Perform the transition.
            Transition();

            // Wait for the cooldown period.
            yield return new WaitForSeconds(3f);

            // Allow transitions again.
            transitionAllowed = true;
        }

        public void Transition()
        {
            GameManager.instance.NextLevel();
        }
    }
}
