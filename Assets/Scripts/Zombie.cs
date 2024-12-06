using UnityEngine;

public class Zombie : MonoBehaviour
{
    public static event System.Action<GameObject> OnZombieKilled;

    private void OnDestroy()
    {
        // Notify listeners when this zombie is destroyed
        OnZombieKilled?.Invoke(gameObject);
    }
}
