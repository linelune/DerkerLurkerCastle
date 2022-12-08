using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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
    public void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame && activeWeapon != 0)
        {
            weapons[activeWeapon].gameObject.SetActive(false);
            activeWeapon = 0;
            weapons[activeWeapon].gameObject.SetActive(true);
        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame && activeWeapon != 1)
        {
            weapons[activeWeapon].gameObject.SetActive(false);
            activeWeapon = 1;
            weapons[activeWeapon].gameObject.SetActive(true);
        }
    }
    public void Attack()
    {
        //animator.SetTrigger("OnAttack");
        // Audio
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
