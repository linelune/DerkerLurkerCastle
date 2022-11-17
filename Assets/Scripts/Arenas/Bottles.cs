using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bottles;

    //[SerializeField]
    private Light[] bottleLights;

    private GameObject[] activeBottles;

    // Keep track of the bottles that have been hit
    [HideInInspector]
    public static int bottlesHit = 0;

    void Awake()
    {
        for(int i=0; i < bottles.Length; i++)
        {
            bottleLights[i] = bottles[i].GetComponentInChildren<Light>();
        }

        for(int i=0; i < 3; i++)
        {
            int choice = Random.Range(0, bottleLights.Length);
            bottleLights[choice].intensity = 0.8f;
            activeBottles[i] = bottles[choice];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
