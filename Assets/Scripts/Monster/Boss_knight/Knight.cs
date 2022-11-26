using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public GameObject model;
    public GameObject attack_hitbox;
    public GameObject charge_hitbox;
    public GameObject emitter;
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
    float Gravity = 9.8f;
    float velocity = 0;
    private bool justHit = false;
    private int health = 1000;
    private AudioSource m_Audio;
    public AudioClip attack_sfx;
    public AudioClip damage_sfx;
    private UnityEngine.AI.NavMeshAgent nma;
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Controller = GetComponent<CharacterController>();
        anim = model.GetComponent<Animator>();
        m_Audio = GetComponent<AudioSource>();
        Target = GameObject.FindWithTag("Player");
        charge_hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnGhost)
        {
            StartCoroutine(SpawnGhost());
        }
        
        var dir = Target.transform.position - transform.position;
        //gameObject.transform.forward = nma.nextPosition;

        if (dir.magnitude > 3f)
        {
            if (canCharge)
            {
                StartCoroutine(charge());
                chargeTarget = Target.transform.position;
                nma.SetDestination(chargeTarget);
                //canCharge = false;

            }

            anim.SetBool("isWalking", false);

            if (!isCharging)
            {
                nma.stoppingDistance = 3f;
                nma.acceleration = 8f;
                nma.speed = 3.5f;
                nma.SetDestination(Target.transform.position);
                anim.SetBool("isWalking", true);
               
            }
            else
            {
                nma.stoppingDistance = 0f;
                nma.acceleration = 20f;
                nma.speed = 21f;
                

                float dist = nma.remainingDistance;
                if (dist != Mathf.Infinity && nma.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && nma.remainingDistance == 0)
                {
                    isCharging = false;
                    anim.SetBool("isCharging", false);
                }
            }


            // m_Controller.Move(movement);
            
             
        }
        else
        {
            anim.SetBool("isCharging", false);
            isCharging = false;
            anim.SetBool("isWalking", false);
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(attack());
            }
        }
        if (!isCharging)
        {
            charge_hitbox.SetActive(false);
        }
        if (m_Controller.isGrounded)
        {
            velocity = 0;
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
            m_Controller.Move(new Vector3(0, velocity, 0));
        }
    }
    //Need to create/spawn hitboxes
    private IEnumerator attack()
    {
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.35f);
        m_Audio.PlayOneShot(attack_sfx, 0.5f);
        Instantiate(attack_hitbox, emitter.transform.position, emitter.transform.rotation);
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(4f);
        canAttack = true;
    }
    //Charge hitbox should deal damage and knock player away
    private IEnumerator charge()
    {
        canCharge = false;
        charge_hitbox.SetActive(true);
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

    void OnCollisionEnter()
    {
        anim.SetBool("isCharging", false);
        isCharging = false;
        anim.SetBool("isWalking", false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttack" && !justHit)
        {
            justHit = true;
            m_Audio.PlayOneShot(damage_sfx, 0.5f);
            Invoke("resetHit", .1f);
            health -= col.gameObject.GetComponent<PlayerHitbox>().getDamage();
            //Destroy(col.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
                //add method to spawn coins on death
            }

        }
    }
    void resetHit()
    {
        justHit = false;
    }
}
