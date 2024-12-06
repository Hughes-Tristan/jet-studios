using UnityEngine;

public class Laser : MonoBehaviour
{
    public LaserManager laserManager; // Reference to the LaserManager

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie")) // Check if a zombie collides
        {
            laserManager.LaserDestroyed(); // Notify LaserManager
            Destroy(gameObject);          // Destroy the laser
        }
    }
}
