using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movespeed = 5f;
    private bool isMoving = false;
    private Vector2 objectPosition;

    /*// Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        float move = movespeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, objectPosition, move);

        if (Vector2.Distance(transform.position, objectPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void setTarget(Vector2 target)
    {
        objectPosition = target;
        isMoving = true;
    }
}
