using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10;

    private void Update()
    {
        if(health == 0)
        {
            Debug.Log("it works");
        }
    }
}
