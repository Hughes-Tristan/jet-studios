using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int bossKills = 0;
    //public GameObject winScreen;

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

    public void bossKillCount()
    {
        bossKills++;
        if (bossKills >= 10)
        {
            setWinScreen();
        }
    }

    void setWinScreen()
    {
        Debug.Log("win screen loaded");
        SceneManager.LoadScene("WinScreen");
    }
}