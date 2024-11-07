using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Controller Variables and References
    public float moveSpeed = 5;
    public SpriteRenderer itemHeldRender;
    private Vector3 playerPos;
    private WaveManager waveManager;
    private Items itemHeld;
    private bool isPlayerMoving = false;

    void Start()
    {
        playerPos = transform.position;
    }

    void Update()
    {
        
    }
}
