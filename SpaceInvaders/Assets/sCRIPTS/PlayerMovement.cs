using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float maxTilt = 0.3f;

    private float screenWidthWorldUnits;
    private float playerWidthWorldUnits;

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
    }
}
