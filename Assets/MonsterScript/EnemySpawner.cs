using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
public bool active;
    /*
    [SerializeField] GameObject mEnemyPrefab1;
    [SerializeField] GameObject mEnemyPrefab2;
    */
    public GameObject[] enemies;
    public float spawnrate=5f;
    GameObject[] mPoints;
    int num=0;
    int num2=0;
    //Added bool to prevent overlapping spawns.
    //Prevents clipping issues with multiple enemies spawning on top of one another
    //-Seth
    private bool hasSpawned = false;
    

    // Start is called before the first frame update
    void Start()
    {
    mPoints=GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
    spawn1();
    
        
    }

    // Update is called once per frame
    void Update()
    {
    /*
    num=Mathf.RoundToInt(Random.Range(0,mPoints.Length));
    num2=Mathf.RoundToInt(Random.Range(0,mPoints.Length));
      */  
    }
    
    public void spawn1()
    {
        int target = Random.Range(0, enemies.Length);
        int spawnPoint = Random.Range(0, mPoints.Length);
        if (!mPoints[spawnPoint].GetComponent<EnemySpawnPoint>().getHasSpawned())
        {
            Debug.Log(num);
            mPoints[spawnPoint].GetComponent<EnemySpawnPoint>().spawnMonster(enemies[target]);
            //Instantiate(enemies[target], mPoints[spawnPoint].transform.position, Quaternion.identity);
            //Invoke("spawn2", spawnrate);
            //hasSpawned=true;
        }
    
    }
    
    //Adjusting spawn function to reuse code.

    /*
    public void spawn2()
    {
        if (!hasSpawned)
        {
            Debug.Log(num2);
            Instantiate(mEnemyPrefab2, mPoints[num2].transform.position, Quaternion.identity);
            Invoke("spawn1", spawnrate);
            hasSpawned = true;
        }
    }
    */
    
}
