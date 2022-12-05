using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflectable : MonoBehaviour
{
    private GameObject boss;
    private GameObject player;
    private float movespeed = 0.01f;
    private int numbounces = 5;
    private bool target = true;
    private bool justHit = false;


    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f,1.5f,0f), movespeed);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, movespeed);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (!justHit)
        {
            justHit = true;
            Invoke("resetHit", .1f);
            if (col.gameObject.tag == "PlayerAttack")
            {
                target = false;
                movespeed += 0.01f;
                numbounces--;
            }
            if (col.gameObject.tag == "Boss")
            {
                target = true;
                numbounces--;
                movespeed += 0.01f;
                if (numbounces <= 0)
                {
                    col.gameObject.GetComponent<BossDerker>().takeDamage();
                    Destroy(gameObject);
                    //Deal damage
                }
            }
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMotor>().TakeDamage(40);
            Destroy(gameObject);
        }
        
    }
    void resetHit()
    {
        justHit = false;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMotor>().TakeDamage(40);
        }
        Destroy(gameObject);
    }
}
