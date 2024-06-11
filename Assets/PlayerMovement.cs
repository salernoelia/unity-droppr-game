using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Adjusted speed for better control

    private Rigidbody2D rb;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set initial position in the lower quarter of the screen
        float lowerQuarterY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.25f, Camera.main.nearClipPlane)).y;
        rb.position = new Vector3(transform.position.x, lowerQuarterY, transform.position.z);
    }

    void Update()
    {
        // Capture horizontal input from Update to ensure responsive input handling
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Apply movement directly in FixedUpdate
        if (horizontalInput != 0)
        {
            // Calculate new position
            Vector2 newPosition = rb.position + new Vector2(horizontalInput * speed * Time.fixedDeltaTime, 0);
            rb.MovePosition(newPosition);
        }
    }
}
