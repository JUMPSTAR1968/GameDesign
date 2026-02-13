using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MoveAnim : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float rotationFactorPerFrame = 15.0f;
    [SerializeField] SaveSystem saveSystem;

    [Header("References")]
    public TextMeshProUGUI scoreText;
    public GameObject sphereObject; // <-- DRAG YOUR SPHERE HERE IN INSPECTOR!

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

        // 1. LOAD GAME
        SavePosition data = saveSystem.Load();

        if (data != null)
        {
            currentScore = data.score;

            // Load Player Position
            if (data.position != Vector3.zero)
            {
                controller.enabled = false;
                transform.position = data.position;
                transform.rotation = data.rotation;
                controller.enabled = true;
            }

            // Load Sphere Position (The Fix!)
            if (sphereObject != null && data.spherePos != Vector3.zero)
            {
                sphereObject.transform.position = data.spherePos;
            }
        }

        UpdateScoreUI();

        inputSystem.PlayerMovement.Move.performed += OnMovementInputs;
        inputSystem.PlayerMovement.Save.performed += SavePlayerDetails;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            currentScore++;
            UpdateScoreUI();
            RespawnSphere(other.gameObject);

            // Auto-save everything including the new sphere position
            saveSystem.Save(transform.position, transform.rotation, currentScore, sphereObject.transform.position);
        }
    }

    void RespawnSphere(GameObject sphere)
    {
        float randomX = Random.Range(-4.5f, 4.5f);
        float randomZ = Random.Range(-4.5f, 4.5f);
        sphere.transform.position = new Vector3(randomX, 0.5f, randomZ);

        // Random Color (Optional)
        sphere.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }

    private void Update()
    {
        // --- NEW INPUT SYSTEM VERSION ---
        // This checks the spacebar without crashing your game
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            saveSystem.DeleteSaveFile();
            currentScore = 0;
            UpdateScoreUI();
            Debug.Log("Reset Complete. Close and Reopen game to see effects.");
        }

        HandleRotation();
        HandleAnimation();

        Vector3 move = new Vector3(currentInputMovement.x, -2f, currentInputMovement.z);
        controller.Move(move * Time.deltaTime * 3.0f);
    }

    // --- BOILERPLATE MOVEMENT CODE ---
    private void OnEnable() => inputSystem.PlayerMovement.Enable();
    private void OnDisable() => inputSystem.PlayerMovement.Disable();

    private void OnMovementInputs(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        currentInputMovement.x = inputMovement.x;
        currentInputMovement.z = inputMovement.y;
        isMovementPressed = inputMovement.x != 0 || inputMovement.y != 0;
    }

    private void HandleAnimation()
    {
        animator.SetBool("isWalking", isMovementPressed);
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
        // Manual Save
        if (sphereObject != null)
        {
            saveSystem.Save(transform.position, transform.rotation, currentScore, sphereObject.transform.position);
        }
    }
}