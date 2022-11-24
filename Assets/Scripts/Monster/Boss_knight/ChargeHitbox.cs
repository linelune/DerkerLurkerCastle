using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeHitbox : MonoBehaviour
{
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
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMotor>().chargeHit();
            col.gameObject.GetComponent<PlayerMotor>().TakeDamage(20);
        }
    }
}
