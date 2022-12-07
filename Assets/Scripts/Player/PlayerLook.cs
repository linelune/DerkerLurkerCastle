using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;

    public static float xSensitivity = PlayerPrefs.GetFloat("sensitivityXnY");
    public static float ySensitivity = PlayerPrefs.GetFloat("sensitivityXnY");

    private float xRotation = 0f;


    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Compute vertical camera rotation
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        // Apply this to our camera transform
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        
        // Rotate Player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
