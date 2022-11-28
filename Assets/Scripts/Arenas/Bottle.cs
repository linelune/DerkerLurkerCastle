using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Bottles.bottlesHit++;
            Debug.Log("You hit a bottle switch!");
            if(Bottles.bottlesHit == 3)
            {

            }
            Destroy(gameObject);
        }
    }
}
