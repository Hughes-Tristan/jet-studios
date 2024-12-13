//Author: Evita Kanaan
//Purpose: check if level 2 is unlocked 

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUnlockManager : MonoBehaviour
{
    public Button level2Button; // Reference to the Level 2 button

    void Start()
    {
        // Check if Level 2 is unlocked
        if (PlayerPrefs.GetInt("Level2Unlocked", 0) == 1)
        {
            level2Button.interactable = true; // Enable the button
            level2Button.onClick.AddListener(LoadLevel2); // Add the click event listener
        }
        else
        {
            level2Button.interactable = false; // Keep the button disabled
        }
    }

    // This method will load Level 2 when the button is clicked
    void LoadLevel2()
    {
        Debug.Log("Level 2 Button Clicked!");
        SceneManager.LoadScene("Final_Level_2"); 
    }
}
