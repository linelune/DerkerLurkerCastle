using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Glutony_sprite : MonoBehaviour
{
  [SerializeField]
    Transform Target;
    float Damping=1f;
    //[SerializeField] Material mMawake;
    //[SerializeField] Material mMasleep;
    //[SerializeField] Material mMfreeze;
    Animator anim;
    SpriteRenderer rend;
    Rigidbody m_Rigidbody;
    public float speed = 1f;
    private CharacterController m_Controller;
    private int health = 60;
    private bool justHit = false;
    public GameObject deathPart;
    DisplayManager mDM;
  
    Vector3 mSpawnpos;
    private bool dead = false;
    float distance;
    public float damage;
    bool awake;
    private bool canAttack = true;
    public Rigidbody projectile;
    public GameObject emitter;
    float Gravity = 9.8f;
    float velocity = 0;

    // Audio
    public AudioSource m_Audio;
    public AudioClip awake_sfx;
    public AudioClip spit_sfx;


    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        m_Controller = GetComponent<CharacterController>();
        //rend.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    //mDM=GetComponentInChildren<DisplayManager>();
    
    
    
    
    //mPlayer=GameObject.Find("Player");
    //mPI=mPlayer.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
     Target=GameObject.FindWithTag("Player").transform;
     
     
     distance=(Target.position-transform.position).magnitude;
     // < 3 is close
     // > 3 is far
     //Debug.Log(distance.ToString());
     //Scaling color by distance to mimic lighting
     if(distance < 30f)
        {
            rend.color = new Color(1 - distance/20, 1 - distance/20, 1 - distance/20, 1f);
        }
        else
        {
            rend.color = new Color(0.0f, 0.0f, 0.0f, 1f);
        }

        lookAt();
     if(distance<15f)
     {
     
     
            anim.SetBool("isAwake", true);
            awake=true;
            m_Audio.PlayOneShot(awake_sfx);
     
     
     }
     //Chase distance longer than awake distance
     if(distance > 20f)
     {
     
            anim.SetBool("isAwake", false);
            awake =false;
            m_Audio.Stop();
     }
        if (awake && !dead)
        {
            bool strafe = false;
            RaycastHit hit;
            var dir = Target.position - transform.position;
            var d = (Target.position + new Vector3(0f, 1f, 0f)) - transform.position;
            var movement = dir;
            
            //Rudimentary ai, helps path around objects
            if (Physics.Raycast(transform.position, d, out hit))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag != "Player")
                {
                    strafe = true;
                }
            }

                if (!strafe)
            {
                
                movement = dir.normalized * speed * Time.deltaTime;
                if (movement.magnitude > dir.magnitude) movement = dir;

                if (canAttack)
                {
                    canAttack = false;
                    anim.SetBool("isAttacking", true);
                }
            }
                //Strafing around objects
            else
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(transform.right), out hit, 5))
                {
                    movement = -transform.right * speed * Time.deltaTime;
                }
                else
                {
                    movement = transform.right * speed * Time.deltaTime;
                }
            }
            m_Controller.Move(movement);

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
    void Attack()
    {
        anim.SetBool("isAttacking", false);
        Rigidbody shot = Instantiate(projectile, emitter.transform.position, emitter.transform.rotation);
        shot.velocity = ((Target.position + new Vector3(0f, 1f, 0f)) - emitter.transform.position).normalized * 15f;
        Invoke("resetAttack", 3f);
    }

    void resetAttack()
    {
        canAttack = true;
    }

    void lookAt ()
  {
        //Locks x and z axis, rotates y to face camera
        //More akin to Doom/Hexen/Heretic style classic billboarding
        var delta = Target.position - transform.position;
        delta.x = delta.z = 0;
        transform.LookAt(Target.transform.position - delta);

    }

  

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttack" && !justHit)
        {
            justHit = true;
            Invoke("resetHit", 1f);
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
        anim.SetBool("isHurt", false);
        justHit = false;
    }

        void OnDestroy()
        {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            Instantiate(deathPart, transform.position, transform.rotation);
        }
    }

    IEnumerator die()
    {
        dead = true;
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}
