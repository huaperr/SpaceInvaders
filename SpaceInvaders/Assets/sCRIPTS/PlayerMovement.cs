using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public float speed = 10.0f;
    public float maxTilt = 0.3f;

    public int score = 0;
    public TMP_Text numScore;

    private float screenWidthWorldUnits;
    private float playerWidthWorldUnits;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0.0f;

    bool powerUp;

    private AudioSource audioSource;
    public AudioClip bulletSound;

    private bool rapidFireActive = false;
    private float rapidFireEndTime;

    void Start()
    {
        Instance = this;

        Camera mainCamera = Camera.main;
        screenWidthWorldUnits = mainCamera.orthographicSize * mainCamera.aspect;
        playerWidthWorldUnits = GetComponent<Renderer>().bounds.size.x / 2;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float tilt = Input.acceleration.x;

        float move = tilt * speed * Time.deltaTime;

        transform.Translate(move, 0, 0);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenWidthWorldUnits + playerWidthWorldUnits, screenWidthWorldUnits - playerWidthWorldUnits);
        transform.position = pos;
        
        
        if(powerUp)
        {
            DoubleShoot();
        }
        else { Shoot(); }

        numScore.text = score.ToString();
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * projectileSpeed;

        audioSource.PlayOneShot(bulletSound);
    }

    void DoubleShoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject projectile2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * projectileSpeed;

        audioSource.PlayOneShot(bulletSound);
    }

    public void ActivateRapidFire(float duration)
    {
        rapidFireActive = true;
        rapidFireEndTime = Time.time + duration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "PowerUp") 
        {
            Destroy(collision.collider.gameObject);
            powerUp = true;
        }
    }
}
