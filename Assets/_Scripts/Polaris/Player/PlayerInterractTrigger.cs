using HeneGames.DialogueSystem;
using Nova;
using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class PlayerInterractTrigger : MonoBehaviour
    {
        public UIBlock interact;
        private DialogueManager dManager;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            //compare tag
            if (collision.gameObject.CompareTag("Interactive"))
            {
                interact.gameObject.SetActive(true);
            }

            dManager = collision.GetComponentInChildren<DialogueManager>();
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (dManager != null)
            {
                if (InputSystem.Interact())
                {
                    interact.gameObject.SetActive(false);
                }
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Interactive"))
            {
                interact.gameObject.SetActive(false);
            }
        }
    }
}
