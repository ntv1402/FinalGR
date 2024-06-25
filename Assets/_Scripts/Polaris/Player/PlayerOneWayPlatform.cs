using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SupanthaPaul;
public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    [SerializeField] private BoxCollider2D playerCollider, playerPhysicsCollider;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (InputSystem.Down() < 0f)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private IEnumerator DisableCollision()
    {
        if (currentOneWayPlatform != null)
        {
            BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

            if (platformCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, platformCollider);
                Physics2D.IgnoreCollision(playerPhysicsCollider, platformCollider);
                yield return new WaitForSeconds(0.5f);
                Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
                Physics2D.IgnoreCollision(playerPhysicsCollider, platformCollider, false);
            }
        }
    }

}
