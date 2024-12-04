using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour
{

    public float timeToDestroy = 10.0f;
    public float projectileSpeed = 5f;
    public float projectileDamage = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }

    // this function is another special event function
    // it is used to check if there is an enemy present in a row
    // if there is an enemy present than we need to the collision check for low enemy health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ZombieMovement enemy = collision.GetComponent<ZombieMovement>();
  
        if(enemy != null)
        {
            enemy.takeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
