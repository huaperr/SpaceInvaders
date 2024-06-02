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
    public float duration = 10f;

    bool powerUp;

    private AudioSource audioSource;
    public AudioClip bulletSound;

    private bool rapidFireActive = false;
    private float rapidFireEndTime;

    void Start()
    {
        powerUp = false;
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

        if (Time.time > nextFireTime)
        {
            if (powerUp)
            {
                DoubleShoot();
            }
            else
            {
                Shoot();
            }
            nextFireTime = Time.time + fireRate;
        }

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
        // Primer proyectil
        GameObject projectile1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
        rb1.velocity = (Vector2.up + Vector2.left * 0.1f) * projectileSpeed;

        // Segundo proyectil
        GameObject projectile2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
        rb2.velocity = (Vector2.up + Vector2.right * 0.1f) * projectileSpeed;

        audioSource.PlayOneShot(bulletSound);
    }

    public void ActivateRapidFire(float duration)
    {
        StartCoroutine(RapidFireRoutine(duration));
    }

    private IEnumerator RapidFireRoutine(float duration)
    {
        powerUp = true;
        yield return new WaitForSeconds(duration);
        powerUp = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            ActivateRapidFire(duration);
        }
    }
}
