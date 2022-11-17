using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Animator animator;
    public Collider shield;
    public Collider sword;
    public int mCoins;

    private void Awake()
    {
        shield.enabled = false;
        sword.enabled = false;
    }
    public void Attack()
    {
        animator.SetTrigger("OnAttack");
        // Audio
        FindObjectOfType<AudioManager>().Play("SwordHit", 0.2f);
        StartCoroutine(EnableCollider(sword, 1));
    }

    public void IsDead()
    {
        animator.SetTrigger("IsDead");
        //destroy game object 
        //load main menu scene or wtvr
    }

    public void OnBlock()
    {
        animator.SetTrigger("IsDefend");
        StartCoroutine(EnableCollider(shield,5));
    }

    private IEnumerator EnableCollider(Collider col, float timeInSec)
    {
        col.enabled = true;
        yield return new WaitForSeconds(timeInSec);
        col.enabled = false;
    }
}
