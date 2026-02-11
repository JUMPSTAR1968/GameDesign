using UnityEngine;

public class SphereMoves : MonoBehaviour
{



 
    [SerializeField] private GameObject moveObject;
    [SerializeField] private float speed = 7f;
    private Rigidbody rb;
    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); // Re-labeling 'y' to 'z' for clarity

        // Map input to X (horizontal) and Z (depth)
        Vector3 move = new Vector3(x, 0, z);

        // transform.Translate moves relative to the object's local facing direction    
        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, z * speed);




    }
}
