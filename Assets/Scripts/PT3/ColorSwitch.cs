using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    private MeshRenderer sphereRend;

    void Start()
    {
        sphereRend = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            MeshRenderer wallRend = collision.gameObject.GetComponent<MeshRenderer>();

            if (wallRend != null)
            {
                // 1. Store the current colors
                Color sphereColorBeforeHit = sphereRend.material.color;
                Color wallColorBeforeHit = wallRend.material.color;

                // 2. Swap them!
                sphereRend.material.color = wallColorBeforeHit;
                wallRend.material.color = sphereColorBeforeHit;

                Debug.Log("Traded colors with " + collision.gameObject.name);
            }
        }
    }
}