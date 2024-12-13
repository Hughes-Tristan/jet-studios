//Author: Evita Kanaan
//Purpose: back button that goes back to the main menu

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
