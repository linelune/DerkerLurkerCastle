using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitbox : PlayerHitbox
{
    private UpgradeManager uM;

    void Start()
    {
        damageVal = 20;

        uM = null;

        if (GameObject.FindWithTag("UpgradeManager"))
            uM = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    public override int getDamage()
    {
        return (int) (damageVal * uM.playerDamage);
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
