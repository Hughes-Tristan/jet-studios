//Author: Evita Kanaan
//Purpose: speed boost power up button. pops up after 30 seconds, lasts for 15 sec

using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    public GameObject speedButtonAnimation;
    public Button powerUpButton;  // The button UI component
    public PlayerController playerController;  // Reference to the player controller script

    private float timeSinceLastPopup = 0f;  // Track how long it's been since the last power-up appeared
    private float popupInterval = 30f;  // Time interval for the button to reappear (30 seconds)
    private float speedBoostDuration = 15f;  // Duration for the speed boost (15 seconds)

    void Start()
    {
        powerUpButton.gameObject.SetActive(false);  // Initially hide the button
        speedButtonAnimation.gameObject.SetActive(false);
        powerUpButton.onClick.AddListener(OnPowerUpClick);  // Add listener for button click
    }

    void Update()
    {
        timeSinceLastPopup += Time.deltaTime;  // Increment the timer

        // Show the power-up button if it's been 30 seconds
        if (timeSinceLastPopup >= popupInterval)
        {
            speedButtonAnimation.gameObject.SetActive(true);
            powerUpButton.gameObject.SetActive(true);  // Make the button visible
        }
    }

    // Called when the button is clicked
    private void OnPowerUpClick()
    {
        speedButtonAnimation.gameObject.SetActive(false);
        powerUpButton.gameObject.SetActive(false);  // Hide the button after it�s clicked
        timeSinceLastPopup = 0f;  // Reset the timer for next power-up

        // Apply the speed boost to the player
        playerController.ApplySpeedBoost(2f, speedBoostDuration);  // 2x speed for 15 seconds
    }
}
