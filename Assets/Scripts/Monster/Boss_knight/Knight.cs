using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int health = 600;
    public AudioSource m_OneShotAudio;
    public AudioSource m_ConstantAudio;
    public AudioClip attack_sfx;
    public AudioClip damage_sfx;
    public AudioClip spawn_sfx;
    public AudioClip death_sfx;
    private UnityEngine.AI.NavMeshAgent nma;
    public GameObject deathPart;
    private bool dead = false;
    private Collider collider;
    public GameObject mCoinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Controller = GetComponent<CharacterController>();
        anim = model.GetComponent<Animator>();
        collider = GetComponent<Collider>();
        Target = GameObject.FindWithTag("Player");
        charge_hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (canSpawnGhost)
            {
                StartCoroutine(SpawnGhost());
            }

            var dir = Target.transform.position - transform.position;

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
                gameObject.transform.forward = new Vector3(dir.x, 0f, dir.z);
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
    }
    //Need to create/spawn hitboxes
    private IEnumerator attack()
    {
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.35f);
        m_OneShotAudio.PlayOneShot(attack_sfx, 0.5f);
        Instantiate(attack_hitbox, emitter.transform.position, emitter.transform.rotation);
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(2f);
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
        //yield return new WaitForSeconds(10f);
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
            m_OneShotAudio.PlayOneShot(damage_sfx, 0.5f);
            Invoke("resetHit", .1f);
            health -= col.gameObject.GetComponent<PlayerHitbox>().getDamage();
            //Destroy(col.gameObject);
            if (health <= 0)
            {
                StartCoroutine(die());
                //add method to spawn coins on death
            }

        }
    }
    void resetHit()
    {
        justHit = false;
    }
    IEnumerator die()
    {
        m_OneShotAudio.PlayOneShot(death_sfx);
        dead = true;
        model.SetActive(false);
        collider.enabled = false;
        Instantiate(deathPart, transform.position + new Vector3(0f,1.5f,0f), transform.rotation);
        for (int i = 0; i < 20; i++)
        {
            //Gives coins a random velocity so they fly around when the pot breaks
            GameObject c = Instantiate(mCoinPrefab, transform.position, transform.rotation);
            Rigidbody cr = c.GetComponent<Rigidbody>();
            float rx = Random.Range(-0.5f, 0.5f);
            float rz = Random.Range(-0.5f, 0.5f);
            cr.velocity = new Vector3(rx, 5f, rz);
            Debug.Log("Coin !");
        }
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Level_2");
    }
}
