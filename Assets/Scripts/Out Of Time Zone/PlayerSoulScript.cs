using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulScript : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = -9.8f;

    private PlayerInput playerInput;

    private CharacterController characterController;

    private SaveManager saveManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput = new PlayerInput();
        playerInput.Enable();

        characterController = GetComponent<CharacterController>();

        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();

        saveManager.SaveData();
    }

    private void LateUpdate()
    {
        GetComponent<PlayerLook>().ProcessHorizontalLook(playerInput.OnFoot.LookAround.ReadValue<Vector2>());
    }

    void FixedUpdate()
    {
        MoveSoul(playerInput.OnFoot.Move.ReadValue<Vector2>());
    }

    private void MoveSoul(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    }
}