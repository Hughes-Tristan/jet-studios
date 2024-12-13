//Author: Evita Kanaan
//Purpose: SFX Manager (coin sound when customers' order received)

using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance; // Singleton for easy access
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure there's only one instance of SFXManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMoneySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}