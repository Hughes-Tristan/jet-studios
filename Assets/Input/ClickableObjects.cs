using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{

    public MovetoMouse movementAction;

    // Start is called before the first frame update
    void OnMouseDown()
    {
        if(movementAction != null)
        {
            movementAction.setTarget(transform.position);
        }
    }
}
