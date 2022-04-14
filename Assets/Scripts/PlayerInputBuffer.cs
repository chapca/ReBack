using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBuffer : MonoBehaviour
{
    PlayerInputMap _inputs;
    PlayerJumping jumping;
    bool check;

    float timer;
    [SerializeField] float maxTimer;

    private void Awake()
    {
        _inputs = new PlayerInputMap();
        _inputs.Movement.Jump.started += StartJumpBuffer;

        jumping = GetComponent<PlayerJumping>();
    }

    void Start()
    {
        timer = maxTimer;
    }

    void StartJumpBuffer(InputAction.CallbackContext ctx)
    {
        StopCoroutine("JumpBuffer");
        StartCoroutine("JumpBuffer");
    }

    IEnumerator JumpBuffer()
    {
        if (jumping._canJump == false)
        {
            check = true;

            yield return new WaitForSeconds(maxTimer);

            if (check == jumping._canJump)
            {
                jumping.Jump();
            }
            else
            {
                check = false;
            }
        }
        yield return null;
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
