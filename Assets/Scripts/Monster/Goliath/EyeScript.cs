using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public Transform cameraTransform;

    public Transform headTransform;

    void Update()
    {
        transform.position = new Vector3(headTransform.position.x + headTransform.localScale.x / 2.0f * 0.04f, headTransform.position.y, headTransform.position.z);
    }
}
