using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerIput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerInteractions interactions;
    private PlayerLook playerLook;
    public int playerHealth = 100;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        playerIput = new PlayerInput();
        onFoot = playerIput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        interactions = GetComponent<PlayerInteractions>();
        // Log events
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Attack.performed += ctx => interactions.Attack();
        onFoot.Block.performed += ctx => interactions.OnBlock();
    }

    // Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
    void FixedUpdate()
    {
        if (motor.IsPlayerAlive())
        {
            // Inform the player motor to move
            motor.ProcessMove(onFoot.Move.ReadValue<Vector2>());
        }
    }

    private void Update()
    {
        if(playerHealth == 0)
        {
            interactions.IsDead();
        }
    }

    private void LateUpdate()
    {
        if (motor.IsPlayerAlive())
        {
            playerLook.ProcessLook(onFoot.LookAround.ReadValue<Vector2>());
        }
    }

    // Enables the onFoot actionMap from our PlayerInput
    private void OnEnable()
    {
        onFoot.Enable();
    }

    // Disables the onFoot actionMap from our PlayerInput
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
