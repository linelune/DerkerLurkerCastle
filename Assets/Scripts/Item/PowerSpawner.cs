using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{   
    public GameObject[] powers;
    // Start is called before the first frame update
    void Start()
    {
        int t = Random.Range(0, powers.Length);
        Instantiate(powers[t], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
