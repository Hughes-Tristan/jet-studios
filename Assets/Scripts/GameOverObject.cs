
// Developer(s): Tristan Hughes
// Last Updated: 11-21-24
// Game Over Class

// the intended purpose of this class is to control the game over object.
// it also provides methods loading the game over screen and object death 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverObject : MonoBehaviour
{
    public float health = 6.0f;

    // this function loads the game over scene when it is destroyed
    void GameOver()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    // this function subtracts the damage amount from the health
    // if the health is less than or equal to 0 then ends the game
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameOver();
        }
    }
}
