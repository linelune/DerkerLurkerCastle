using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelBuilder lb;
    private LevelBuilder level;
    public GameObject bossPortal;
    private bool bossSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        level = Instantiate(lb, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossSpawned)
        {
            bossSpawned = true;
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
            
            if (spawns.Length <= 0)
            {
                bossSpawned = false;
                return;
            }

            int target = Random.Range(0, spawns.Length-1);
            Instantiate(bossPortal, spawns[target].transform.position, spawns[target].transform.rotation);
            
        }
    }
    void restartGame()
    {
        Destroy(level);
        level = Instantiate(lb, gameObject.transform.position, gameObject.transform.rotation);
    }
}
