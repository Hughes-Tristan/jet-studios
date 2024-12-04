using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
