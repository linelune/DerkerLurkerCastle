using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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
    private bool invulnerable = false;
    public float speed = 5f;
    private float sprintSpeed;
    private float baseSpeed;
    private UpgradeManager uM;
    public float gravity = -9.8f;
    public float jumpHeight = 1.0f;
    public float crouchTimer;
    public int health = 100;
    public int maxHealth = 100;

    // Audio
    private AudioManager movementAM;

    // Start is called before the first frame update
    void Start()
    {
        uM = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        setSkills();
        baseSpeed = speed;
        sprintSpeed = speed + 2.0f;
        characterController = GetComponent<CharacterController>();
        movementAM = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Keyboard.current[Key.Y].wasPressedThisFrame)
        {
            setSkills();
        }
            if (health > maxHealth || invulnerable)
        {
            health = maxHealth;
        }
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
        //speed = baseSpeed;
    }
    public void setSkills()
    {
        speed = uM.getSpeed();
        baseSpeed = speed;
        sprintSpeed = speed + 2.0f;
        maxHealth = uM.getHealth();
        jumpHeight = uM.getJump();
        Debug.Log("Speed: " + speed + ", Health: " + maxHealth + ", Jump: " + jumpHeight);
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        // Move on x and z axis
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if (transform.TransformDirection(moveDirection) == new Vector3(0.0f, 0.0f, 0.0f)) {
            movementAM.StopPlaying("FootstepsOnConcrete");
            animator.SetFloat("Speed", 0); 
        }
        else {
            movementAM.Play("FootstepsOnConcrete", 0f, 1f);
            movementAM.Volume("FootstepsOnConcrete", 1.5f);
            animator.SetFloat("Speed", speed); 
        }
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
            speed = sprintSpeed;
            movementAM.Play("FootstepsOnConcrete", 0f, 1.5f);
            movementAM.Volume("FootstepsOnConcrete", 1.5f);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            speed = baseSpeed;
            movementAM.StopPlaying("FootstepsOnConcrete");
            animator.SetBool("IsRunning", false);
        }
    }

    public void SpeedPower()
    {
        CancelInvoke("ResetSpeed");
        baseSpeed += 3;
        sprintSpeed += 3;
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
            Invoke("ResetSpeed", 10f);
    }

    void ResetSpeed()
    {
        Debug.Log("Reset Speed");
        baseSpeed -= 3;
        sprintSpeed -= 3;
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
    }

    public void InvulnPower()
    {
        Debug.Log("Invulnerable");
        CancelInvoke("ResetInvuln");
        invulnerable = true;
        Invoke("ResetInvuln", 15f);
    }
    void ResetInvuln()
    {
        invulnerable = false;
    }

    public void MoonPower()
    {
        Debug.Log("Moon Jump!");
        CancelInvoke("ResetMoon");
        gravity = -3f;
        Invoke("ResetMoon", 20f);
    }

    void ResetMoon()
    {
        Debug.Log("Reset Moon");
        gravity = -9.81f;
    }
}
