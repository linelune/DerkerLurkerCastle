using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleBoneScript : MonoBehaviour
{
    public float maximumSpeed;

    public float poseFactor;
    public float horizontalDartFactor;

    public Transform head;

    private Vector3 distanceBetweenBoneAndHead;
    private Vector3 horizontalDartTarget;

    private void Start()
    {
        distanceBetweenBoneAndHead = transform.position - head.position;

        poseFactor *= 10.0f;
        horizontalDartFactor *= 50.0f;
    }

    private void FixedUpdate()
    {
        float speed = Vector3.Magnitude(GetComponent<Rigidbody>().velocity);

        if (speed > maximumSpeed)
        {
            float brakeSpeed = speed - maximumSpeed;

            Vector3 normalisedVelocity = GetComponent<Rigidbody>().velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

            GetComponent<Rigidbody>().AddForce(-brakeVelocity);
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -200.0f, 0.0f));

        GetComponent<Rigidbody>().AddForce(((head.position + distanceBetweenBoneAndHead) - transform.position) * poseFactor);

        horizontalDartTarget = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);

        GetComponent<Rigidbody>().AddForce((horizontalDartTarget - transform.position) * horizontalDartFactor);
    }
}
