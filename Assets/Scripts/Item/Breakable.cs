using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
public GameObject mCoinPrefab;
public int content=3;
public int radius=2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void crushed()
    {
    for(int i=0;i<content;i++){
    Instantiate(mCoinPrefab,(transform.position+(Vector3)(radius * UnityEngine.Random.insideUnitCircle)),Quaternion.identity);
    
    Debug.Log("Coin !");
    Destroy(this);

    }}
    
}
