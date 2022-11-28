using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoor : MonoBehaviour
{
    public Transform doorPos;
    public float moveSpeed = 0.8f;
    public float sizeOfDoorX = 1;

    private Vector3 doorCloseTarget;
    private Vector3 doorOpenTarget;
    private float startTime;
    private float distanceToCover;
    private bool isOpen = false;


    void Start()
    {
        doorCloseTarget = doorPos.localPosition;
        doorOpenTarget = new Vector3(doorCloseTarget.x, doorCloseTarget.y, doorCloseTarget.z - 4.2f);
        distanceToCover = Vector3.Distance(doorCloseTarget, doorOpenTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        startTime = Time.time;

        if (!isOpen) return;

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / distanceToCover;
        doorPos.localPosition = Vector3.Lerp(doorPos.localPosition, doorOpenTarget, fractionOfJourney);

        Debug.Log("Door Opened");
        isOpen = true;
    }
}
