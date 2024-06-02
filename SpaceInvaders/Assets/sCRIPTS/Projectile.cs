
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject powerUpPrefab; // Prefab del power-up que queremos instanciar
    public float powerUpDropChance = 0.1f; // 10% de probabilidad de soltar un power-up

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerMovement.Instance.score += 100;
            Destroy(collision.gameObject);
            Destroy(gameObject);

            TrySpawnPowerUp(collision.transform.position);
        }
    }

    void TrySpawnPowerUp(Vector2 position)
    {
        float randomChance = Random.value;
        if (randomChance < powerUpDropChance)
        {
            Instantiate(powerUpPrefab, position, Quaternion.identity);
        }
    }
}