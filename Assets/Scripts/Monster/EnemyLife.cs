using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
public int health=100;
Rock_script mRS;
    // Start is called before the first frame update
    void Start()
    {
    mRS=GetComponent<Rock_script>();
        
    }

    // Update is called once per frame
    void Update()
    {
    if(health<=0)
    {
    if(mRS!=null)
    {
    mRS.Die();
    }
    Destroy(this);
    }
        
    }
}
