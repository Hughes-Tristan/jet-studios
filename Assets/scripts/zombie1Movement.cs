using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component that's attached to enemy
    }

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void MoveZombie()
    {
        // Basic movement to the right
        Vector2 movement = new Vector2(moveSpeed, 0);
        rb.velocity = movement; // Apply the movement to the Rigidbody2D
    }
}
