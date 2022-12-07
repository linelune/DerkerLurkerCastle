using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitbox : PlayerHitbox
{
    // Start is called before the first frame update
    void Start()
    {
        damageVal = 20;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int getDamage()
    {
        return damageVal;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            //play fleshy hit sound
        }
        else
        {
            //play solid impact sound
        }
        Destroy(gameObject);
    }
}
