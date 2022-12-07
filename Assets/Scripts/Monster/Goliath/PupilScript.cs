using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilScript : MonoBehaviour
{
    public Transform cameraTransform;

    public Transform eyeTransform;

    void Update()
    {
        Vector3 position = cameraTransform.position - eyeTransform.position;

        position = position.normalized * (eyeTransform.localScale.x / 2.0f) * 0.04f;

        transform.position = position + eyeTransform.position;
    }
}
