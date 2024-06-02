using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShoot : MonoBehaviour
{
    public float duration = 10f;

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

        // Desactivar el sprite y el collider del power-up mientras está activo
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Esperar la duración del power-up
        yield return new WaitForSeconds(duration);

        // Destruir el power-up después de que se agote el tiempo
        Destroy(gameObject);
    }
}
