using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShoot : MonoBehaviour
{
    public float duration = 10f;
    public float descentSpeed = 1f; // Velocidad de descenso

    private void Update()
    {
        transform.Translate(Vector2.down * descentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision.collider));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ActivateRapidFire(duration);
        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
