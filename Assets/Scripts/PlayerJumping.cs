using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float coyoteTime = 0.2f;
    PlayerInputMap _inputs;
    bool _isGrounded;
    bool _hasLanded;
    bool _isJumping;

    float _verticalMomemtum;
    [SerializeField] float _gravityStrength;

    float _jumpT = 0f;
    float _jumpLerp = 0f;
    float _jumpLerpMax = 0f;
    float _oldJumpT = 0f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] AnimationCurve _jumpCurve = null;
    public bool _canJump = true;
    bool _hasCoyoted;

    CharacterController _charaCon;
    PlayerMovement _playerMovement;
    private bool _shouldApplyGravity;
    public bool _isHoldingJump { get; private set; }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _inputs = new PlayerInputMap();
        _inputs.Movement.Jump.started += PressJump;
        _inputs.Movement.Jump.canceled += ReleaseJump;
    }

    void Start()
    {
        _verticalMomemtum = 0;
        _charaCon = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (_shouldApplyGravity && _playerMovement.CanMove) ApplyGravity();
        CheckGround();
        if (_isJumping) ApplyJump();
    }

    private void Land()
    {
        _hasLanded = true;
        _canJump = true;
        _isGrounded = true;
        _verticalMomemtum = 0f;
        _hasCoyoted = false;
        _shouldApplyGravity = false;
        CancelInvoke("Coyote");
    }

    private void ApplyGravity()
    {
        _hasLanded = false;
        _isGrounded = false;

        if (!_hasCoyoted) Invoke("Coyote", coyoteTime);
        _hasCoyoted = true;

        _verticalMomemtum -= Time.deltaTime * _gravityStrength;
        _charaCon.Move(new Vector3(0f, _verticalMomemtum, 0f));

        if (_charaCon.velocity == Vector3.zero)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, _charaCon.radius + 0.1f, Vector3.down, out hit, _charaCon.height + 0.1f))
            {
                Land();
                Vector3 collisionDirection = (hit.point - transform.position).normalized;
                Debug.DrawRay(transform.position, collisionDirection, Color.yellow);
                Vector3 intermediateDirection = Vector3.Cross(collisionDirection, Vector3.up);
                Vector3 directionInWhichToPush = Vector3.Cross(collisionDirection, intermediateDirection);
                //Debug.DrawRay(transform.position, intermediateDirection, Color.red);
                Debug.DrawRay(transform.position, directionInWhichToPush, Color.green);
                _charaCon.Move(directionInWhichToPush * Vector3.Dot(directionInWhichToPush, Vector3.down));
                _verticalMomemtum = 0;
            }
        }
    }

    void CheckGround()
    {
        if (!_isJumping)
        {
            RaycastHit groundHit;
            Debug.DrawRay(transform.position, Vector3.down * 0.52f, Color.blue);
            if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 0.52f, 1 << 3))
            {
                if (!_hasLanded) Land();
            }
            else
                _shouldApplyGravity = true;
        }
    }

    private void PressJump(InputAction.CallbackContext obj)
    {
        _jumpLerpMax = 1;
        Jump();
    }

    private void ReleaseJump(InputAction.CallbackContext obj)
    {
        _jumpLerpMax = 0.2f;
        StopJumping();
    }

    public void Jump()
    {
        if (_canJump)
        {
            _isJumping = true;
            _hasLanded = false;
            _isGrounded = false;
            _canJump = false;
            _hasCoyoted = true;
            _shouldApplyGravity = false;
        }
    }

    private void ApplyJump()
    {
        _jumpLerp += (1f / 60f) * 1f / jumpDuration;

        _jumpT = Mathf.Lerp(0f, 1f, _jumpCurve.Evaluate(_jumpLerp));
        float mario = _jumpT - _oldJumpT;
        _charaCon.Move(new Vector3(0f, mario * jumpHeight, 0f));
        _oldJumpT = _jumpT;

        if (_jumpLerp >= _jumpLerpMax)
        {
            _isJumping = false;
            _verticalMomemtum = 0f;
            _jumpLerp = 0f;
            _oldJumpT = 0f;
        }
    }

    void StopJumping()
    {
        _jumpLerpMax = 0.2f;
        if (_isJumping)
        {
            _isJumping = false;
            _verticalMomemtum = 0f;
            _jumpLerp = 0f;
            _oldJumpT = 0f;
        }
    }

    void Coyote()
    {
        if (!_isGrounded)
            _canJump = false;
    }

    #region disable inputs on Player disable to avoid weird inputs
    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
    #endregion
}
