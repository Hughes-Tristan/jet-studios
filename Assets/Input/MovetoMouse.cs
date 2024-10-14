using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MovetoMouse : MonoBehaviour
{

    public float movespeed = 5f;
    private GameObject target;
    private bool canMove = false;
    private Vector3 objectPosition;

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {

            float move = movespeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, move);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                canMove = false;
            }


        }
    }

    public void MoveToTarget()
    {
        canMove = true;
    }

    private void OnMouseDown()
    {
        if(gameObject == target)
        {
            MoveToTarget();
        }
    }
}
