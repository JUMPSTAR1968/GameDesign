using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // Needed for UI

public class MoveAnim : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float rotationFactorPerFrame = 15.0f;
    [SerializeField] SaveSystem saveSystem;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText; // Drag your ScoreText object here

    // Variables
    int currentScore = 0;

    // Components
    PlayerInputSystem inputSystem;
    CharacterController controller;
    Animator animator;
    Vector3 currentInputMovement;
    bool isMovementPressed;

    private void Awake()
    {
        inputSystem = new PlayerInputSystem();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // 1. LOAD GAME & GET SCORE
        // We capture the returned data from SaveSystem
        SavePosition data = saveSystem.Load();

        if (data != null)
        {
            currentScore = data.score; // Restore the score

            // Only move player if they have a saved position (prevents getting stuck at 0,0,0)
            if (data.position != Vector3.zero)
            {
                // Disable controller briefly to warp character
                controller.enabled = false;
                transform.position = data.position;
                transform.rotation = data.rotation;
                controller.enabled = true;
            }
        }

        UpdateScoreUI();

        inputSystem.PlayerMovement.Move.performed += OnMovementInputs;
        inputSystem.PlayerMovement.Save.performed += SavePlayerDetails; // Manual Save (Button)
    }

    // --- NEW: SPHERE INTERACTION ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb")) // Ensure Sphere has tag "Orb"
        {
            // 1. Add Score
            currentScore++;
            UpdateScoreUI();

            // 2. RESPAWN THE SPHERE
            // Moves the sphere to a new random spot instead of destroying it
            RespawnSphere(other.gameObject);

            // 3. AUTO-SAVE 
            // Save immediately when score changes
            saveSystem.Save(transform.position, transform.rotation, currentScore);
        }
    }

    void RespawnSphere(GameObject sphere)
    {
        // A standard Plane (Scale 1,1,1) is 10x10 units.
        // The center is usually (0,0,0), so it goes from -5 to +5 on X and Z.

        float randomX = Random.Range(-4.5f, 4.5f); // Keep it slightly inside the edge
        float randomZ = Random.Range(-4.5f, 4.5f);

        // Move the sphere to the new position
        sphere.transform.position = new Vector3(randomX, 0.5f, randomZ);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }

    // --- EXISTING MOVEMENT CODE ---

    private void OnEnable() => inputSystem.PlayerMovement.Enable();
    private void OnDisable() => inputSystem.PlayerMovement.Disable();

    private void Update()
    {
        HandleRotation();
        HandleAnimation();

        // Simple Gravity (keeps player on floor)
        Vector3 move = new Vector3(currentInputMovement.x, -2f, currentInputMovement.z);
        controller.Move(move * Time.deltaTime * 3.0f); // Adjust speed if needed
    }

    private void OnMovementInputs(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        currentInputMovement.x = inputMovement.x;
        currentInputMovement.z = inputMovement.y;
        isMovementPressed = inputMovement.x != 0 || inputMovement.y != 0;
    }

    private void HandleAnimation()
    {
        if (isMovementPressed)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentInputMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentInputMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void SavePlayerDetails(InputAction.CallbackContext context)
    {
        saveSystem.Save(transform.position, transform.rotation, currentScore);
    }
}