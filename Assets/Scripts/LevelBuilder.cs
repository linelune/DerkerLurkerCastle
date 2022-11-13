using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    //public int rooms = 10;
    public static int roomsRemaining;
    public Room[] roomList;
    public GameObject player;
    private Room lastRoom;
    private GameObject p;
    
    // Start is called before the first frame update
    void Start()
    {
        roomsRemaining = 10;
        lastRoom = Instantiate(roomList[0], gameObject.transform.position, gameObject.transform.rotation);
        PlayerSpawner ps = lastRoom.GetComponent<PlayerSpawner>();
        //p = Instantiate(player, ps.transform.position, ps.transform.rotation);
        /*
         while(roomsRemaining > 0)
        {
            int select = Random.Range(0, 3);
            lastRoom = Instantiate(roomList[select], lastRoom.getSpawner().transform.position, lastRoom.getSpawner().transform.rotation);
            
            roomsRemaining--;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getNumRooms()
    {
        return roomsRemaining;
    }
    public void updateRooms()
    {
        roomsRemaining--;
    }
}
