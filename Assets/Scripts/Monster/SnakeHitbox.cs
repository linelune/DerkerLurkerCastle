using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHitbox : MonoBehaviour
{
    bool hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && !hasHit)
        {
            //Check if player is blocking
            if (!col.gameObject.GetComponent<PlayerMotor>().getBlock())
            {
                Debug.Log("Snake has hit player");
            }
            else
            {
                Debug.Log("Blocked!");
            }
            //deal damage here
        }
    }
}
