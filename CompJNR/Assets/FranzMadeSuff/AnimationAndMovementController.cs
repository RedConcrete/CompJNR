using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovInput;
    Vector3 currentMov;
    Vector3 currentRunMov;
    bool isMovementPressed;
    bool isRunPressed;

    public float rotaionFactorPerFrame = 10.0f;
    public float runMulti = 3.0f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovInput = context.ReadValue<Vector2>();
        currentMov.x = currentMovInput.x;
        currentMov.z = currentMovInput.y;
        currentRunMov.x = currentMovInput.x * runMulti;
        currentRunMov.z = currentMovInput.y * runMulti;
        isMovementPressed = currentMovInput.x != 0 || currentMovInput.y != 0;
    }
    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handeRotaion()
    {
        Vector3 posToLook;

        posToLook.x = currentMov.x;
        posToLook.y = 0.0f;
        posToLook.z = currentMov.z;


        Quaternion currentRot = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRot = Quaternion.LookRotation(posToLook);
            transform.rotation = Quaternion.Slerp(currentRot, targetRot, rotaionFactorPerFrame * Time.deltaTime);
        }

    }
    void handelGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMov.y = groundedGravity;
            currentRunMov.y = groundedGravity;
        }
        else
        {
            float gravity = -10;
            currentMov.y += gravity;
            currentRunMov.y += gravity;
        }
    }

    void handelAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRuninng = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRuninng)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRuninng)
        {
            animator.SetBool(isRunningHash, false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        handelAnimation();
        handeRotaion();
        handelGravity();

        if (isRunPressed)
        {
            characterController.Move(currentRunMov * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMov * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
