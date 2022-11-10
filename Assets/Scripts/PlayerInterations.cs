using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterations : MonoBehaviour
{
    private CharacterController characterController;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Attack()
    {
        animator.SetTrigger("OnAttack");
        //get sword collider
        //if collided with enemy 
        //enemy.health -= damage or smthg
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
        //set a bool getDamage to false and use it in the enemy script that inflicts damage 
        //call an ienumarator to set the bool to true after 10sec or smthg
    }
}
