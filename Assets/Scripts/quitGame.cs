using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // Logs a message in the editor for testing purposes
        Debug.Log("Quit button clicked!");

        // Reset the Level2Unlocked status to 0 before quitting
        PlayerPrefs.SetInt("Level2Unlocked", 0);
        PlayerPrefs.Save();  // Save the PlayerPrefs data to ensure the reset is persistent


        // Exits the application
        Application.Quit();
    }
}
