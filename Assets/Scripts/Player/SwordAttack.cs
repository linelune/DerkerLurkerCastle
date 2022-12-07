using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyLife>().health -= damage;
            // play animation maybe or if you have a onDead() thing to animate then destroy
        }
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<Breakable>().crushed();
            // play animation maybe or if you have a onDead() thing to animate then destroy
        }
        
        
    }
}
