using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulScript : MonoBehaviour
{
    public float speed = 10.0f;

    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // TO DO - Make similar movment to level 1

        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(new Vector3(horizontalMovement, 0, verticalMovement));
    }

    private void LateUpdate()
    {
        GetComponent<PlayerLook>().ProcessLook(playerInput.OnFoot.LookAround.ReadValue<Vector2>());
    }
}
