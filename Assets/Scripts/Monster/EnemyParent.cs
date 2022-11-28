using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    CharacterController controller;
    Vector3 impact = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2) controller.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void buntHit()
    {

        GameObject t = GameObject.FindWithTag("Player");
        Vector3 dir = t.transform.position +new Vector3(0f, 1.5f, 0f) - transform.position;
        impact =  dir.normalized * -20f;
        //if (movement.magnitude > dir.magnitude) movement = dir;
        //controller.Move(movement);
    }


}
