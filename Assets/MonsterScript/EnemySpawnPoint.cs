using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private GameObject enemy;
    private bool hasSpawned = false;// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Resets the spawn boolean on enemy death to allow for the spawner to spawn an additional enemy
        if(enemy == null)
        {
            hasSpawned = false;
        }
    }
    public bool getHasSpawned()
    {
        return hasSpawned;
    }

    public void spawnMonster(GameObject monster)
    {
        enemy = Instantiate(monster, transform.position, Quaternion.identity);
        hasSpawned = true;
    }
}
