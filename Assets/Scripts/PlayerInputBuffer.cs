using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBuffer : MonoBehaviour
{
    PlayerInputMap _inputs;
    PlayerJumping jumping;

    bool hasPressed;
    float timer;
    [SerializeField] float maxTimer;

    private void Awake()
    {
        _inputs.Movement.Jump.started += JumpBuffer;
    }

    void Start()
    {
        timer = maxTimer;
    }

    void JumpBuffer(InputAction.CallbackContext ctx)
    {

    }
}
