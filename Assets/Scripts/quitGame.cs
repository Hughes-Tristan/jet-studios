using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        // Logs a message in the editor for testing purposes
        Debug.Log("Quit button clicked!");

        // Exits the application
        Application.Quit();
    }
}
