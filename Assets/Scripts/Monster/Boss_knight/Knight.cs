using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public GameObject model;
    public GameObject attack_hitbox;
    public GameObject charge_hitbox;
    public GameObject Ghost;
    public GameObject GhostParticles;
    private GameObject Target;
    private Animator anim;
    private float speed = 3f;
    private bool canAttack= true;
    private bool canCharge = true;
    private bool isCharging = false;
    private bool canSpawnGhost = true;
    Vector3 movement;
    Vector3 chargeTarget;
    private CharacterController m_Controller;
    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        anim = model.GetComponent<Animator>();
        Target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnGhost)
        {
            StartCoroutine(SpawnGhost());
        }
        
        var dir = Target.transform.position - transform.position;
        gameObject.transform.forward = dir;
        if (dir.magnitude > 3f)
        {
            if (canCharge)
            {
                StartCoroutine(charge());
                chargeTarget = Target.transform.position;
                //canCharge = false;

            }

            anim.SetBool("isWalking", false);

            if (!isCharging)
            {
                anim.SetBool("isWalking", true);
                movement = dir.normalized * speed * Time.deltaTime;
                if (movement.magnitude > dir.magnitude) movement = dir;
            }
            else
            {
                dir = chargeTarget - transform.position;
                movement = dir.normalized * (5*speed) * Time.deltaTime;
                if (movement.magnitude > dir.magnitude) movement = dir;
                Debug.Log(movement.magnitude);
                if(chargeTarget == transform.position)
                {
                    anim.SetBool("isCharging", false);
                    isCharging = false;
                }
            }


            m_Controller.Move(movement);
             
        }
        else
        {
            anim.SetBool("isWalking", false);
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(attack());
            }
        }
    }
    //Need to create/spawn hitboxes
    private IEnumerator attack()
    {
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(4f);
        canAttack = true;
    }
    //Charge hitbox should deal damage and knock player away
    private IEnumerator charge()
    {
        canCharge = false;
        anim.SetBool("isCharging", true);
        isCharging = true;
        
        yield return new WaitForSeconds(3f);

        anim.SetBool("isCharging", false);
        isCharging = false;
        yield return new WaitForSeconds(7f);
        canCharge = true;

    }
    private IEnumerator SpawnGhost()
    {
        canSpawnGhost = false;
        var pos = transform.position + new Vector3(0f, 5f, 0f) + Random.insideUnitSphere * 5;
        Instantiate(GhostParticles, pos, transform.rotation);
        yield return new WaitForSeconds(5f);
        Instantiate(Ghost, pos, transform.rotation);
        yield return new WaitForSeconds(10f);
        canSpawnGhost = true;
    }
}
