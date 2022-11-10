using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera camera;
    private float xRotation = 0f;
    public float xSensitivity = 30.0f;
    public float ySensitivity = 30.0f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // Calculate camera rotaion for looking up and down
        xRotation -= (mouseY *Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // Apply this to our camera transform
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        // Rotate Player to look left ad right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
