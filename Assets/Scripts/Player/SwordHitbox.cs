using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : PlayerHitbox
{
    //public int damageVal = 30;
    // Start is called before the first frame update
    void Start()
    {
        damageVal = 30;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int getDamage()
    {
        return damageVal;
    }

}
