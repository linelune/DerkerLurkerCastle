using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    //public Animator animator;
    private bool isGrounded;
    private bool isCrouched;
    private bool crouching;
    private bool isSprinting;
    private bool invulnerable = false;
    public float speed = 5f;
    private float sprintSpeed;
    private float baseSpeed;
    private bool isBlocking = false;
    private UpgradeManager uM;
    public float gravity = -9.8f;
    public float jumpHeight = 1.0f;
    public float crouchTimer;
    public int health = 100;
    public int maxHealth = 100;
    private int DerkerDamage = 0;
    public Slider healthBar;
    Vector3 impact = Vector3.zero;

    public UnityEvent DamageOverlay;
    public UnityEvent FreezeOverlay;
    public UnityEvent InvulnOverlay;
    public UnityEvent SpeedOverlay;
    public UnityEvent MoonOverlay;
    public UnityEvent DerkerOverlay;
    public UnityEvent DeathOverlay;

    public Animator playerCameraAnimator;
    private bool isDead = false;
    private float deathTime = 0.0f;

    // Audio
    private AudioManager movementAM;

    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.Find("HealthBar"))
            healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        else
            healthBar = null;
        Cursor.lockState = CursorLockMode.Locked;

        if (GameObject.FindWithTag("UpgradeManager"))
            uM = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        else
            uM = null;

        setSkills();
        baseSpeed = speed;
        sprintSpeed = speed + 2.0f;
        characterController = GetComponent<CharacterController>();
        movementAM = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        UpdateHealth();
        
        if (Keyboard.current[Key.Y].wasPressedThisFrame)
        {
            setSkills();
        }
            if (health > maxHealth - DerkerDamage)
        {
            health = maxHealth - DerkerDamage;
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

        CheckDeathEnd();
    }
    public void setSkills()
    {
        if (uM != null)
            speed = uM.getSpeed() * transform.localScale.x;
        else
            speed = 5.0f * transform.localScale.x;

        baseSpeed = speed;
        sprintSpeed = speed + 2.0f;

        if (uM != null)
            maxHealth = uM.getHealth();
        else
            maxHealth = 100;

        if (uM != null)
            jumpHeight = uM.getJump();
        else
            jumpHeight = 1f;

        Debug.Log("Speed: " + speed + ", Health: " + maxHealth + ", Jump: " + jumpHeight);
    }

    public void ProcessMove(Vector2 input)
    {
        if (isDead)
            return;

        Vector3 moveDirection = Vector3.zero;
        // Move on x and z axis
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if (transform.TransformDirection(moveDirection) == new Vector3(0.0f, 0.0f, 0.0f)) {
            movementAM.StopPlaying("FootstepsOnConcrete");
            
            //animator.SetFloat("Speed", 0); 
        }
        else {
            movementAM.Play("FootstepsOnConcrete", 0f, 1f);
            movementAM.Volume("FootstepsOnConcrete", 1.5f);
            
            //animator.SetFloat("Speed", speed); 
        }
        // apply the impact force:
        if (impact.magnitude > 0.2)
        {
            characterController.Move(impact * Time.deltaTime);
            // consumes the impact energy each cycle:
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
            //speed = baseSpeed;

        }
        else
        {
            characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

            // Move on y axis (jump and gravity)
            playerVelocity.y += gravity * Time.deltaTime;

        }
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
            //animator.SetBool("IsRunning", true);
        }
        else
        {
            speed = baseSpeed;
            movementAM.StopPlaying("FootstepsOnConcrete");
            //animator.SetBool("IsRunning", false);
        }
    }

    public void SpeedPower()
    {
        CancelInvoke("ResetSpeed");
        baseSpeed += 3;
        sprintSpeed += 3;
        if (SpeedOverlay != null)
        {
            SpeedOverlay.Invoke();
        }
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
        
        CancelInvoke("ResetInvuln");
        invulnerable = true;
        if(InvulnOverlay != null)
        {
            InvulnOverlay.Invoke();
        }
        Invoke("ResetInvuln", 15f);
    }
    void ResetInvuln()
    {
        invulnerable = false;
    }

    public void MoonPower()
    {
        
        CancelInvoke("ResetMoon");
        gravity = -3f;
        if (MoonOverlay != null)
        {
            MoonOverlay.Invoke();
        }
        Invoke("ResetMoon", 20f);
    }

    void ResetMoon()
    {   
        gravity = -9.81f;
    }

    public IEnumerator Block()
    {
        isBlocking = true;
        yield return new WaitForSeconds(1f);
        isBlocking = false; 
    }
    public void TakeDamage(int damage)
    {
        if (!isBlocking && !invulnerable)
        {
            health -= damage;
            if(DamageOverlay != null)
            {
                DamageOverlay.Invoke();
            }
            CheckHealth();
        }
        else
            Debug.Log("Blocked !");
    }

    public void Freeze()
    {
        FreezeOverlay.Invoke();
    }

    public void chargeHit()
    {

        GameObject t = GameObject.FindWithTag("Boss");
        Vector3 dir = t.transform.position - (transform.position + new Vector3(0f, 5f, 0f) );
        impact = dir.normalized * -40f;
        //if (movement.magnitude > dir.magnitude) movement = dir;
        //controller.Move(movement);
    }

    public void Derk()
    {
        DerkerDamage += 25;
        if(health > maxHealth - DerkerDamage)
        {
            health = maxHealth - DerkerDamage;
        }
        if(DerkerOverlay != null)
        {
            DerkerOverlay.Invoke();
        }
        CheckHealth();
    }

    private void CheckHealth()
    {
        Debug.Log("Player Health : " + health);

        if (health <= 0)
        {
            isDead = true;

            playerCameraAnimator.SetBool("isPlayerDead", true);

            DeathOverlay.Invoke();
        }
    }

    public bool IsPlayerAlive()
    {
        return ! isDead;
    }

    private void CheckDeathEnd()
    {
        if (!isDead)
            return;

        deathTime += Time.deltaTime;

        if (deathTime >= 2.5f)
            SceneManager.LoadScene("Death Menu");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "DeathPlane")
        {
            //die
            SceneManager.LoadScene("Out Of Time Zone");
        }
    }

    public void UpdateHealth()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
    }
}
