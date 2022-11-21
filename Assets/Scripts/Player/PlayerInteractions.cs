using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    
    //public Animator animator;
    public Weapon[] weapons;
    private int activeWeapon = 0;
    public int mCoins;

    private void Awake()
    {
        weapons[activeWeapon].gameObject.SetActive(true);
    }
    public void Attack()
    {
        //animator.SetTrigger("OnAttack");
        // Audio
        FindObjectOfType<AudioManager>().Play("SwordHit", 0.2f);
        weapons[activeWeapon].StartCoroutine("Attack");
        //StartCoroutine(EnableCollider(sword, 1));
    }

    public void IsDead()
    {
        //animator.SetTrigger("IsDead");
        //destroy game object 
        //load main menu scene or wtvr
    }

    public void OnBlock()
    {
        //animator.SetTrigger("IsDefend");
        weapons[activeWeapon].StartCoroutine("AltAttack");
        //StartCoroutine(EnableCollider(shield,5));
    }

    private IEnumerator EnableCollider(Collider col, float timeInSec)
    {
        col.enabled = true;
        yield return new WaitForSeconds(timeInSec);
        col.enabled = false;
    }
}
