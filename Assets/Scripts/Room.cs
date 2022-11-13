using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public LevelBuilder lb;
    public Spawner[] sp;
    public GameObject clutterSpawner;
    public GameObject[] clutterList;
    public GameObject[] branchList;
    public GameObject[] passageList;
    public GameObject[] arenaList;
    public GameObject deadEnd;
    private GameObject nextRoom;
    private int targetRoom;
    // Start is called before the first frame update
    void Start()
    {
        if(clutterList.Length > 0)
        {
            targetRoom = Random.Range(0, clutterList.Length);
            Instantiate(clutterList[targetRoom], clutterSpawner.transform.position, clutterSpawner.transform.rotation);

        }
        //sp = gameObject.GetComponent<Spawner>();
        if (sp.Length != 0)
        {
            if (lb.getNumRooms() > 0)
            {
                for (int i = 0; i < sp.Length; i++)
                {
                    switch (sp[i].spawnerType)
                    {
                        case Spawner.type.BRANCH:
                            targetRoom = Random.Range(0, branchList.Length);
                            nextRoom = Instantiate(branchList[targetRoom], sp[i].transform.position,
                                sp[i].transform.rotation);
                            break;

                        case Spawner.type.PASSAGE:
                            targetRoom = Random.Range(0, passageList.Length);
                            nextRoom = Instantiate(passageList[targetRoom], sp[i].transform.position,
                                sp[i].transform.rotation);
                            break;

                        case Spawner.type.ARENA:
                            targetRoom = Random.Range(0, arenaList.Length);
                            nextRoom = Instantiate(arenaList[targetRoom], sp[i].transform.position,
                                sp[i].transform.rotation);
                            break;
                    }
                    

                    


                }
                lb.updateRooms();
            }
            else
            {
                for (int i = 0; i < sp.Length; i++)
                {
                    
                    nextRoom = Instantiate(deadEnd, sp[i].transform.position, sp[i].transform.rotation);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   // public Spawner getSpawner()
    //{
      //  return sp;
    //}
    
}
