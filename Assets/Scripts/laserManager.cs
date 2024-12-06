using UnityEngine;
using TMPro;

public class LaserManager : MonoBehaviour
{
    public int totalLasers = 5; // Total number of lasers
    private int intactLasers;  // Number of intact lasers

    public TMP_Text laserCounterText; // Reference to the HUD Text

    void Start()
    {
        intactLasers = totalLasers; // Initialize intact lasers
        UpdateLaserCounter();      // Update the HUD
    }

    public void LaserDestroyed()
    {
        if (intactLasers > 0)
        {
            intactLasers--;        // Decrease intact lasers
            UpdateLaserCounter();  // Update the HUD
        }
    }

    private void UpdateLaserCounter()
    {
        laserCounterText.text = $"Lasers: {intactLasers}/{totalLasers}";
    }
}
