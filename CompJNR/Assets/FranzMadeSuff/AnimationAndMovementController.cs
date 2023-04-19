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

    public float rotaionFactorPerFrame = 15.0f;
    public float runMulti = 5.0f;
    int zero = 0;

    float g = -10f;
    float gGrounded = -.05f;

    bool isJumpPressed = false;
    float initialJumpV;
    float maxJumpH = 10.0f;
    float maxJumpTime = 1.0f;
    bool isJumping= false;


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

        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJump();
    }


    void setupJump()
    {
        float timeToApex = maxJumpTime / 2;
        g = (-2 * maxJumpH) / Mathf.Pow(timeToApex, 2);
        initialJumpV = (2 * maxJumpH) / timeToApex;

        Debug.Log("JumpSetup");
    }

    void handelJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed){
            isJumping = true;
            currentMov.y = initialJumpV * .5f;
            currentRunMov.y = initialJumpV * .5f;
        }
        else if (characterController.isGrounded && !isJumpPressed && isJumping){
            isJumping = false;
        }
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log("JumpPressed");
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
        posToLook.y = zero;
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
        bool isFalling = currentMov.y <= 0.0f || !isJumpPressed;
        float fallMulti = 2.0f;

        if (characterController.isGrounded)
        {
            currentMov.y = gGrounded;
            currentRunMov.y = gGrounded;
        }else if (isFalling){
            float previousYV = currentMov.y;
            float newYV = currentMov.y + (g * fallMulti *Time.deltaTime);
            float nextYV = (previousYV + newYV) * .5f;
            currentMov.y = nextYV;
            currentRunMov.y = nextYV;
        }
        else
        {
            float previousYV = currentMov.y;
            float newYV = currentMov.y + (g * Time.deltaTime);
            float nextYV = (previousYV + newYV) * .5f;
            currentMov.y = nextYV;
            currentRunMov.y = nextYV;
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
        
        if (isRunPressed)
        {
            characterController.Move(currentRunMov * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMov * Time.deltaTime);
        }

        handelGravity();
        handelJump();
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
