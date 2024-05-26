
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerMovement.Instance.score += 100;
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
        }
    }
}