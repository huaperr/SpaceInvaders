using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float maxTilt = 0.3f;

    private float screenWidthWorldUnits;
    private float playerWidthWorldUnits;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0.0f;

    void Start()
    {
        Camera mainCamera = Camera.main;
        screenWidthWorldUnits = mainCamera.orthographicSize * mainCamera.aspect;
        playerWidthWorldUnits = GetComponent<Renderer>().bounds.size.x / 2;
    }

    void Update()
    {
        float tilt = Input.acceleration.x;

        float move = tilt * speed * Time.deltaTime;

        transform.Translate(move, 0, 0);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenWidthWorldUnits + playerWidthWorldUnits, screenWidthWorldUnits - playerWidthWorldUnits);
        transform.position = pos;

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * projectileSpeed;
    }
}
