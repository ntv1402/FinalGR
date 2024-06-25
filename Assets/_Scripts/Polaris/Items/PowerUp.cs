using Bardent;
using Nova;
using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SupanthaPaul
{
    public class PowerUp : MonoBehaviour
    {
        private bool pickupAllowed = false;

        public List<ItemDescription> availableItems;
        private ItemDescription item;

        private Collider2D collidingObject;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            int randomIndex = Random.Range(0, availableItems.Count);
            item = availableItems[randomIndex];

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = item.icon;


        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collidingObject = collision;
            if (collidingObject.CompareTag("Player"))
            {
                pickupAllowed = true;
            }
            //pop up ui button for interract
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            pickupAllowed = false;
        }

        private void Update()
        {
            if (pickupAllowed && InputSystem.Interact())
            {
                Destroy(gameObject);
                if (collidingObject != null)
                {

                    IngameUI.Instance.InitiateitemUI(item);

                    GameManager.instance.CreateGate(transform.position);

                    //add to inventory
                    InventoryManager.instance.AddItem(item);

                    ApplyPowerUpEffects(collidingObject.gameObject);
                }
            }
        }

        private void ApplyPowerUpEffects(GameObject target)
        {
            foreach (var powerupEffect in item.powerupEffects)
            {
                powerupEffect.Apply(target);
            }
        }
    }
}
