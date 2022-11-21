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
        swordanim.SetBool("isAttacking", true);
        Instantiate(swordHitbox, emitter.position, emitter.rotation);
        yield return new WaitForSeconds(.1f);
        swordanim.SetBool("isAttacking", false);
    }

    public override IEnumerator AltAttack()
    {
        shieldanim.SetBool("isBlocking", true);
        pm.StartCoroutine("Block");
        yield return new WaitForSeconds(.1f);
        shieldanim.SetBool("isBlocking", false);
    }
}
