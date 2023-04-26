using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    int _isWalkingHash;
    int _isRunningHash;

    Vector2 _currentMovInput;
    Vector3 _currentMov;
    Vector3 _currentRunMov;
    Vector3 _appliedMov;

    bool _isMovementPressed;
    bool _isRunPressed;

    public float _rotaionFactorPerFrame = 15.0f;
    public float _runMulti = 5.0f;
    int _zero = 0;

    float _g = -10f;
    float _gGrounded = -.05f;

    bool _isJumpPressed = false;
    float _initialJumpV;
    float _maxJumpH = 5.0f;
    float _maxJumpTime = 1.0f;
    bool _isJumping= false;

    int _isJumpingHash;
    bool _isJumpAnimating = false;

    int _jumpCount = 0;

    Dictionary<int, float> _initJumpVDic = new Dictionary<int, float>();
    Dictionary<int, float> _jumpG = new Dictionary<int, float>();

    Coroutine _currentJumpResRoutine = null;

    int _jumpCountHash;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");

        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;

        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;

        _playerInput.CharacterControls.Jump.started += onJump;
        _playerInput.CharacterControls.Jump.canceled += onJump;

        setupJump();
    }

    void setupJump()
    {
        float timeToApex = _maxJumpTime / 2;
        _g = (-2 * _maxJumpH) / Mathf.Pow(timeToApex, 2);
        _initialJumpV = (2 * _maxJumpH) / timeToApex;

        float secondJumpG = (-2 * _maxJumpH + 2) / Mathf.Pow((timeToApex * 1.25f), 2);
        float seconfJumpIniV =  (2 * _maxJumpH + 2) / (timeToApex * 1.25f);
        float thridJumpG =  (-2 * _maxJumpH + 4) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thridJumpIniV =   (2 * _maxJumpH + 4) / (timeToApex * 1.5f);

        _initJumpVDic.Add(1, _initialJumpV);
        _initJumpVDic.Add(2, seconfJumpIniV);
        _initJumpVDic.Add(3, thridJumpIniV);

        _jumpG.Add(0, _g);
        _jumpG.Add(1, _g);
        _jumpG.Add(2, secondJumpG);
        _jumpG.Add(3, thridJumpG);

    }

    void handelJump()
    {
        if (!_isJumping && _characterController.isGrounded && _isJumpPressed){
            if (_jumpCount < 3 && _currentJumpResRoutine != null)
            {
                StopCoroutine(_currentJumpResRoutine);
            }
            _animator.SetBool(_isJumpingHash ,true);
            _isJumpAnimating = true;
            _isJumping = true;
            _jumpCount += 1;
            _animator.SetInteger(_jumpCountHash, _jumpCount);
            _currentMov.y = _initJumpVDic[_jumpCount];
            _appliedMov.y = _initJumpVDic[_jumpCount];
        }
        else if (_characterController.isGrounded && !_isJumpPressed && _isJumping){
            _isJumping = false;
        }
    }

    void handeRotaion()
    {
        Vector3 posToLook;

        posToLook.x = _currentMov.x;
        posToLook.y = _zero;
        posToLook.z = _currentMov.z;


        Quaternion currentRot = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRot = Quaternion.LookRotation(posToLook);
            transform.rotation = Quaternion.Slerp(currentRot, targetRot, _rotaionFactorPerFrame * Time.deltaTime);
        }

    }

    void handelGravity()
    {
        bool isFalling = _currentMov.y <= 0.0f || !_isJumpPressed;
        float fallMulti = 2.0f;

        if (_characterController.isGrounded)
        {
            if (_isJumpAnimating)
            {
                _animator.SetBool(_isJumpingHash, false);
                _isJumpAnimating = false;
                _currentJumpResRoutine = StartCoroutine(jumpResetRoutine());
                if (_jumpCount == 3)
                {
                    _jumpCount = 0;
                    _animator.SetInteger(_jumpCountHash, _jumpCount);
                }
            }

            _currentMov.y = _gGrounded;
            _appliedMov.y = _gGrounded;

        }
        else if (isFalling)
        {
            float previousYV = _currentMov.y;
            _currentMov.y = _currentMov.y + (_jumpG[_jumpCount] * fallMulti * Time.deltaTime);
            _appliedMov.y = Mathf.Max((previousYV + _currentMov.y) * .5f, -20.0f);
        }
        else
        {
            float previousYV = _currentMov.y;
            _currentMov.y = _currentMov.y + (_jumpG[_jumpCount] * Time.deltaTime);
            _appliedMov.y = (previousYV + _currentMov.y) * .5f;
        }
    }

    void handelAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRuninng = _animator.GetBool(_isRunningHash);

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        if ((_isMovementPressed && _isRunPressed) && !isRuninng)
        {
            _animator.SetBool(_isRunningHash, true);
        }
        else if ((!_isMovementPressed || !_isRunPressed) && isRuninng)
        {
            _animator.SetBool(_isRunningHash, false);
        }

    }

    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        _jumpCount = 0;
    }

    void onJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        Debug.Log("JumpPressed");
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        _currentMovInput = context.ReadValue<Vector2>();
        _currentMov.x = _currentMovInput.x;
        _currentMov.z = _currentMovInput.y;
        _currentRunMov.x = _currentMovInput.x * _runMulti;
        _currentRunMov.z = _currentMovInput.y * _runMulti;
        _isMovementPressed = _currentMovInput.x != 0 || _currentMovInput.y != 0;
    }

    void onRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        handelAnimation();
        handeRotaion();
        
        if (_isRunPressed){
            _appliedMov.x = _currentRunMov.x;
            _appliedMov.z = _currentRunMov.z;

        }
        else{
            _appliedMov.x = _currentMov.x;
            _appliedMov.z = _currentMov.z;
        }

        _characterController.Move(_appliedMov * Time.deltaTime);

        handelGravity();
        handelJump();
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
