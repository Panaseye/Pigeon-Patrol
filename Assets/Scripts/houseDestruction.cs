using UnityEngine;

public class houseDestruction : MonoBehaviour
{
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 1f;

    public void DestroyHouse()
    {
        foreach (Transform child in transform) // Loop through all children
        {
            // Add Rigidbody if not already present
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (!rb)
            {
                rb = child.gameObject.AddComponent<Rigidbody>();
                rb.mass = 5f; // Adjust mass if needed
            }

            // Apply explosion force
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
    }
}
