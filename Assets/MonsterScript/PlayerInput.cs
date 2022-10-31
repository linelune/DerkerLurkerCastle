using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
   public float speed = 10;
    [SerializeField] private float rotationSpeed = 300;
   public int life = 20;

    private void Start()
    {
        // nothing to do here...
    }

    private void Update()
    {
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // ALTERNATIVE
        //float horizontal = 0;
        //float vertical = 0;
        //vertical += Input.GetKey(KeyCode.W) ? 1 : 0;
        //vertical += Input.GetKey(KeyCode.S) ? -1 : 0;
        //horizontal += Input.GetKey(KeyCode.A) ? -1 : 0;
        //horizontal += Input.GetKey(KeyCode.D) ? 1 : 0;

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        direction = direction.normalized;

        // Translate the gameobject
        transform.position += direction * speed * Time.deltaTime;

        // Rotate the gameobject
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), rotationSpeed * Time.deltaTime);
    }
}
