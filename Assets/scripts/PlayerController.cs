using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movespeed = 5f;
    private bool isMoving = false;
    private Vector2 objectPosition;
    private System.Action targetReached;

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
            targetReached?.Invoke();
            targetReached = null;
        }
    }

    public void MoveToTarget(Vector2 target, System.Action onArrival)
    {
        objectPosition = target;
        isMoving = true;
        targetReached = onArrival;
    }
}


