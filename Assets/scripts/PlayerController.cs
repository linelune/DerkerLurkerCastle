
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 5;
    public float Gravity = 9.8f;
    public Camera cam;
    public float sprintMod;
    public float jumpHeight;
    public float glideMod;
    public float crashMod;
    //public CapsuleCollider collide;
    private float velocity = 0;
    private int numJumps = 0;
    Vector3 movement;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    
    }

    void Update()
    {

        transform.rotation = cam.transform.rotation;

            // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontal *= sprintMod;
            vertical *= sprintMod;
        }
        
        movement = ((cam.transform.right * horizontal + cam.transform.forward * vertical));

        if (Input.GetKeyDown(KeyCode.Space) && numJumps < 2 && !Input.GetKey(KeyCode.F))
        {
            velocity = jumpHeight * 1.5f;
            numJumps++;
        }

        movement.y = velocity;

        movement *= Time.deltaTime;

        characterController.Move(movement);

        // Gravity
        if (characterController.isGrounded)
        {
            velocity = 0;
            numJumps = 0;

        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                velocity -= Gravity * Time.deltaTime * crashMod;
            }
                if (Input.GetKey(KeyCode.F))
                {
                    velocity -= Gravity * Time.deltaTime * glideMod;
                }
          
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.F))
            {
                velocity -= Gravity * Time.deltaTime * 1.5f;
            }//characterController.Move(new Vector3(0, velocity, 0));
        }

    }
}

