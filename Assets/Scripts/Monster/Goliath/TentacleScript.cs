using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleScript : MonoBehaviour
{
    enum Mode { WAIT, PLANT, PAUSE };

    public Transform tentacleTarget;
    public Transform head;

    public float speed;
    public float plantSpeed;

    public bool isActive = false;

    private Vector3 tentacleTargetOriginPosition;
    private Vector3 headOriginPosition;
    private Vector3 waitPosition;
    private Vector3 plantPosition;

    private Mode mode;

    private float distanceBetweenHeadAndWalls;

    void Start()
    {
        tentacleTargetOriginPosition = tentacleTarget.position;
        headOriginPosition = head.position;

        waitPosition = head.position + (tentacleTargetOriginPosition - head.position) * 0.6f;
        plantPosition = tentacleTargetOriginPosition;

        mode = Mode.PAUSE;

        distanceBetweenHeadAndWalls = head.gameObject.GetComponent<HeadScript>().distanceBetweenHeadAndWalls;
    }

    void Update()
    {
        if (mode == Mode.WAIT)
        {
            tentacleTarget.position = Vector3.MoveTowards(tentacleTarget.position, waitPosition, speed * Time.deltaTime);
        }

        if (mode == Mode.PLANT)
        {
            tentacleTarget.position = Vector3.MoveTowards(tentacleTarget.position, plantPosition, plantSpeed * Time.deltaTime);
        }

        if (mode == Mode.WAIT && Vector3.Distance(tentacleTarget.position, waitPosition) < 1.0f)
        {
            mode = Mode.PLANT;

            getPlantPosition();
        }

        if (mode == Mode.PLANT && Vector3.Distance(tentacleTarget.position, plantPosition) < 1.0f && isActive)
        {
            mode = Mode.PAUSE;

            head.gameObject.GetComponent<HeadScript>().activeNextTentacle();
        }

        if (mode == Mode.PAUSE && isActive)
        {
            mode = Mode.WAIT;

            waitPosition = head.position + (tentacleTargetOriginPosition - headOriginPosition) * 0.6F;
        }
    }

    private void getPlantPosition()
    {
        HeadScript.DirectionFacing direction = head.gameObject.GetComponent<HeadScript>().direction;

        if (tentacleTargetOriginPosition.y > head.position.y)
            plantPosition.y = head.position.y + Random.Range(0.0f, distanceBetweenHeadAndWalls);
        else
            plantPosition.y = head.position.y - Random.Range(0.0f, distanceBetweenHeadAndWalls / 1.5f);

        if ((direction == HeadScript.DirectionFacing.XPOS || direction == HeadScript.DirectionFacing.XNEG)
            && tentacleTargetOriginPosition.z > head.position.z) {
            plantPosition.z = head.position.z + distanceBetweenHeadAndWalls * 1.1f;
        } else if (direction == HeadScript.DirectionFacing.XPOS || direction == HeadScript.DirectionFacing.XNEG) {
            plantPosition.z = head.position.z - distanceBetweenHeadAndWalls * 1.1f;
        }

        if ((direction == HeadScript.DirectionFacing.ZPOS || direction == HeadScript.DirectionFacing.ZNEG)
            && tentacleTargetOriginPosition.x > head.position.x)
            plantPosition.x = head.position.x + distanceBetweenHeadAndWalls * 1.1f;
        else if (direction == HeadScript.DirectionFacing.ZPOS || direction == HeadScript.DirectionFacing.ZNEG)
            plantPosition.x = head.position.x - distanceBetweenHeadAndWalls * 1.1f;

        if (direction == HeadScript.DirectionFacing.XPOS)
            plantPosition.x = head.position.x + Random.Range(0.0f, distanceBetweenHeadAndWalls);
        else if (direction == HeadScript.DirectionFacing.XNEG)
            plantPosition.x = head.position.x - Random.Range(0.0f, distanceBetweenHeadAndWalls);
        else if (direction == HeadScript.DirectionFacing.ZPOS)
            plantPosition.z = head.position.z + Random.Range(0.0f, distanceBetweenHeadAndWalls);
        else if (direction == HeadScript.DirectionFacing.ZNEG)
            plantPosition.z = head.position.z - Random.Range(0.0f, distanceBetweenHeadAndWalls);
    }
}
