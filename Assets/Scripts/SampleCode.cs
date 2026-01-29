using UnityEngine;

public class SampleCode : MonoBehaviour
{
    // Add this line so you can drag your Bomb here in the Inspector
    public GameObject bombPrefab; 
    
    public GameObject moveObject;
    public float speed = 5;

    void Update()
    {
        // --- 1. BOMB SPAWNING LOGIC ---
        // Input.GetMouseButtonDown(0) is Left Click
        if (Input.GetMouseButtonDown(0))
        {
            // Spawn the bomb at the 'moveObject' (Player) position, not the Spawner's position
            Instantiate(bombPrefab, moveObject.transform.position, Quaternion.identity);
        }

        // --- 2. MOVEMENT LOGIC (Already working) ---
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y);

        // This moves the player
        moveObject.transform.Translate(move * Time.deltaTime * speed);
    }
}