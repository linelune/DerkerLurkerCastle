using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    public enum DirectionFacing { XPOS, XNEG, ZPOS, ZNEG };

    public float distanceBetweenHeadAndWalls;

    public DirectionFacing direction = DirectionFacing.XPOS;

    public List<TentacleScript> tentacles;

    public int activeTentacleId;

    public float speed;

    void Start()
    {
        direction = DirectionFacing.XPOS;

        activeTentacleId = 0;

        tentacles[activeTentacleId].isActive = true;
    }

    void Update()
    {
        float average = 0.0f;
        Vector3 targetPosition;

        if (direction == DirectionFacing.XPOS || direction == DirectionFacing.XNEG)
        {
            for (int i = 0; i < tentacles.Count; i++)
                average += tentacles[i].tentacleTarget.position.x;

            average /= tentacles.Count;

            targetPosition = new Vector3(average, transform.position.y, transform.position.z);
        }
        else
        {
            for (int i = 0; i < tentacles.Count; i++)
                average += tentacles[i].tentacleTarget.position.z;

            average /= tentacles.Count;

            targetPosition = new Vector3(transform.position.x, transform.position.y, average);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void activeNextTentacle()
    {
        tentacles[activeTentacleId].isActive = false;

        activeTentacleId = (activeTentacleId + 1) % tentacles.Count;
        
        tentacles[activeTentacleId].isActive = true;
    }
}
