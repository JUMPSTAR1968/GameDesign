using UnityEngine;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject bombPrefab;
    public float moveSpeed = 5f; // Speed of the plane

    private Vector2 moveInput;

    // --- SPAWNING LOGIC ---
    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            if (bombPrefab != null)
            {
                Instantiate(bombPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // --- MOVEMENT LOGIC (Added this so WASD works!) ---
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        // Move the plane every frame based on WASD input
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}