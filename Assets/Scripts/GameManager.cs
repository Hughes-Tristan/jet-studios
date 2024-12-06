using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int bossKills = 0;
    //public GameObject winScreen;
    public TMP_Text zombieCounterText;
    private int zombieCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateZombieCounter();
    }

    public void bossKillCount()
    {
        bossKills++;
        increaseZombieCount(1);
        if (bossKills >= 5)
        {
            setWinScreen();
        }
    }

    void setWinScreen()
    {
        Debug.Log("win screen loaded");
        SceneManager.LoadScene("WinScreen");
    }

    public void increaseZombieCount(int amount)
    {
        zombieCount += amount; // Add the earned amount to money
        UpdateZombieCounter(); // Update the money display
    }

    private void UpdateZombieCounter()
    {
        zombieCounterText.text = $"Zombies: {zombieCount} / 5";
    }
}