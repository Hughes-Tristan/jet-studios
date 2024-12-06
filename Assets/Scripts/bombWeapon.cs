using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    public float explosionRadius = 5f;  // Radius of the bomb's effect
    public float explosionDelay = 2f;   // Time before the bomb explodes
    public GameObject explosionEffect;  // Optional explosion visual effect (can be a prefab)

    // Add an initialization method for the bomb
    public void InitializeBomb()
    {
        // Here you can add bomb-specific initialization if needed
        // For example, you can set explosion delay, radius, or other bomb properties
        Debug.Log("Bomb initialized with radius: " + explosionRadius + " and delay: " + explosionDelay);
    }

    private void Start()
    {
        // Start a delayed explosion after placing the bomb
        Invoke(nameof(Explode), explosionDelay);
    }

    private void Explode()
    {
        // Display explosion effect (if any)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destroy all zombies within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Zombie"))
            {
                // this part checks to see if the zombie is a giant or not to account for boss challenge kills
                ZombieMovement zombieObject = collider.GetComponent<ZombieMovement>();
                if (zombieObject != null && zombieObject.isGiant)
                {
                    GameManager.Instance.bossKillCount();
                }
                Destroy(collider.gameObject);  // Destroy the zombie
            }
        }

        // Destroy the bomb itself after the explosion
        Destroy(gameObject);
    }
}
