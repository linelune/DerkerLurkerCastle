using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShield : Weapon
{
    public GameObject shieldModel;
    public GameObject shieldHitbox;
    public GameObject swordModel;
    public GameObject swordHitbox;
    private PlayerMotor pm;
    public Transform emitter;
    private Animator swordanim;
    private Animator shieldanim;
    private bool atk1 = false, atk2 = false, atk1done = false;
    // Start is called before the first frame update
    void Start()
    {
        
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMotor>();
        swordanim = swordModel.gameObject.GetComponent<Animator>();
        shieldanim = shieldModel.gameObject.GetComponent<Animator>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override IEnumerator Attack()
    {
        //Debug.Log("Attack");
        if (!atk1)
        {
            atk1 = true;
            swordanim.SetBool("isAttacking", true);
            Debug.Log("Attack 1");
           
            //Instantiate(swordHitbox, emitter.position, emitter.rotation);
            yield return new WaitForSeconds(1f);
            if (!atk2)
            {
                atk1 = false;
                swordanim.SetBool("isAttacking", false);
            }

            //swordanim.SetBool("isAttacking", false);
        }
        else if (!atk2)
        {
            atk2 = true;
       
            swordanim.SetBool("isAttacking2", true);
            
            
            Debug.Log("Attack 2");
            yield return new WaitForSeconds(.666f);
            swordanim.SetBool("isAttacking", false);
            swordanim.SetBool("isAttacking2", false);
            atk2 = atk1 = false;
        }
        /*
        else if (!atk3)
        {
            atk3 = true;
            
            Debug.Log("Attack 3");
            yield return new WaitForSeconds(2f);
        }
        */
        //atk1 = atk2 = atk1done = false;
    }

    public override IEnumerator AltAttack()
    {
        shieldanim.SetBool("isBlocking", true);
        pm.StartCoroutine("Block");
        yield return new WaitForSeconds(.1f);
        shieldanim.SetBool("isBlocking", false);
    }

    public void spawnHitbox()
    {
        Instantiate(swordHitbox, emitter.position, emitter.rotation);
    }
}
