using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    public Animator animator;
    private bool isGrounded;
    private bool isCrouched;
    private bool crouching;
    private bool isSprinting;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.0f;
    public float crouchTimer;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isCrouched)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if(crouching)
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            else
                characterController.height = Mathf.Lerp(characterController.height, 2, p);

            if (p > 1)
            {
                isCrouched = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        // Move on x and z axis
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if (transform.TransformDirection(moveDirection) == new Vector3(0.0f, 0.0f, 0.0f)) { animator.SetFloat("Speed", 0); }
        else { animator.SetFloat("Speed", speed); }
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        // Move on y axis (jump and gravity)
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2.0f;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    // Jumping mechanism
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = (float)Math.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    // Crouch mechanism
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        isCrouched = true;
    }

    // Sprint mechanism
    public void Sprint()
    {
        isSprinting = !isSprinting;
        if (isSprinting)
        {
            speed = 8.0f;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            speed = 5.0f;
            animator.SetBool("IsRunning", false);
        }
    }
}
