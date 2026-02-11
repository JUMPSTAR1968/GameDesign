using UnityEngine;
using UnityEngine.InputSystem;

public class MoveAnim : MonoBehaviour
{
    [SerializeField] float rotationFactorPerFrame;
    [SerializeField] SaveSystem saveSystem;

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

        inputSystem.PlayerMovement.Move.performed += OnMovementInputs;
        inputSystem.PlayerMovement.Save.performed += SavePlayerDetails;

    }

    private void OnEnable() => inputSystem.PlayerMovement.Enable();

    private void OnDisable() => inputSystem.PlayerMovement.Disable();


    private void Update()
    {
        HandleRotation();
        HandleAnimation();
        controller.Move(currentInputMovement * Time.deltaTime);
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
        saveSystem.Save(transform.position, transform.rotation);
    }
}
