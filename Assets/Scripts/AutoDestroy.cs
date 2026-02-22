using UnityEngine;

/// <summary>
/// Destroys the GameObject this script is attached to after a specified delay.
/// </summary>
public class AutoDestroy : MonoBehaviour
{
    [Tooltip("The delay in seconds before the GameObject is destroyed.")]
    public float destroyDelay = 1.0f;

    void Start()
    {
        // Schedule the destruction of the GameObject.
        Destroy(gameObject, destroyDelay);
    }
}
