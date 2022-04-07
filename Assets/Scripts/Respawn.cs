using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Respawn : MonoBehaviour
{
    PlayerInputMap _inputs;
    [SerializeField] Transform _spawnPoint;

    private void Awake()
    {
        _inputs = new PlayerInputMap();
        _inputs.Movement.Respawn.started += PressRespawn;
    }

    void Start()
    {
        RespawnPlayer();
    }

    void PressRespawn(InputAction.CallbackContext ctx)
    {
        RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        GameManager.instance.SetTimer(0.1f);
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
