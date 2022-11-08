using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
public bool active;
[SerializeField] GameObject mEnemyPrefab1;
[SerializeField] GameObject mEnemyPrefab2;
public float spawnrate=5f;
GameObject[] mPoints;
int num=0;
int num2=0;


    // Start is called before the first frame update
    void Start()
    {
    mPoints=GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
    spawn1();
    
        
    }

    // Update is called once per frame
    void Update()
    {
    num=Mathf.RoundToInt(Random.Range(0,mPoints.Length));
    num2=Mathf.RoundToInt(Random.Range(0,mPoints.Length));
        
    }
    
    public void spawn1()
    {
    Debug.Log(num);
    Instantiate(mEnemyPrefab1, mPoints[num].transform.position, Quaternion.identity);
    Invoke("spawn2",spawnrate);
    
    
    }
    
    public void spawn2()
    {
    Debug.Log(num2);
     Instantiate(mEnemyPrefab2, mPoints[num2].transform.position, Quaternion.identity);
     Invoke("spawn1",spawnrate);
    
    }
    
    
}
