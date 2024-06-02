using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShoot : MonoBehaviour
{
    public float speed = 2f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootingInterval = 2f;
    public GameObject rapidFirePowerUpPrefab;
    public float powerUpDropChance = 0.2f; // 20% de probabilidad de soltar un power-up

    private float shootingTimer;

    void Start()
    {
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        MoveDown();
        HandleShooting();
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void HandleShooting()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject); // Destruye el proyectil
            Destroy(gameObject); // Destruye el enemigo
            DropPowerUp(); // Lanza el power-up
        }
    }

    void DropPowerUp()
    {
        if (Random.value <= powerUpDropChance)
        {
            Instantiate(rapidFirePowerUpPrefab, transform.position, Quaternion.identity);
        }
    }
}
