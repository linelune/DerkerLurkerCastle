using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
//GameObject mPlayer;
//PlayerInput mPI;
[SerializeField]
Transform Target;
float Damping=1f;
    SpriteRenderer rend;
    //[SerializeField] Material mMfreeze;
    DisplayManager mDM;
Freezer mFreezer;
float distance;
    public GameObject deathPart;
    public float ChaseSpeed = 0.05f;
    private int health = 20;
    private bool justHit = false;
    private bool frozen = false;
public float damage;
    private AudioSource m_Audio;
    private Animator anim;
    public AudioClip awake_sfx;
    public AudioClip freeze_sfx;
    private bool awake = false;

    GameObject mPlayer;
    PlayerMotor mPI;
    public bool cooldown = true;
    bool freezed = false;
    private float resetval;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindWithTag("Player");
        mPI = mPlayer.GetComponent<PlayerMotor>();
        
        m_Audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAsleep", true);
        rend = GetComponent<SpriteRenderer>();
        //mPlayer=GameObject.Find("Player");
        //mPI=mPlayer.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
     Target=GameObject.FindWithTag("Player").transform;
     
     
     distance=(Target.position-transform.position).magnitude;
        if (distance < 30f)
        {
            rend.color = new Color(1 - distance / 20, 1 - distance / 20, 1 - distance / 20, 1f);
        }
        else
        {
            rend.color = new Color(0.0f, 0.0f, 0.0f, 1f);
        }
        // < 3 is close
        // > 3 is far
        //Debug.Log(distance.ToString());
        lookAt();
     if(distance<15f)
     {
            if (!awake)
            {
                awake=true;
                anim.SetBool("isAsleep", false);
                m_Audio.PlayOneShot(awake_sfx, 0.5f);
            }
            if (distance > 2f)
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, Target.position + new Vector3(0f, 1.5f, 0f), ChaseSpeed);
            }

            if(cooldown && distance<3f)
            {
                //freezePlayer();
                Debug.Log("WHY");
                anim.SetBool("isAttacking", true);
                frozen=true;
     
            }
    
     }
     else
        {
            awake = false;
        }
     
    
    
        
    }
    
    void lookAt ()
  {
        
        var delta = Target.position - transform.position;
        delta.x = delta.z = 0;
        transform.LookAt(Target.transform.position - delta);
        //var rotation = Quaternion.LookRotation(delta);
        //Using slerp here causes the bilboarding to fail. The sprite should ALWAYS point at the camera, there shouldn't be any movement damping
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*Damping);
        //transform.rotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
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
                Destroy(gameObject);
                //add method to spawn coins on death
            }

        }
    }
    void resetHit()
    {
        justHit = false;
    }
    void OnDestroy()
    {
        if (frozen)
        {
            releasePlayer();
        }
        if (SceneManager.GetActiveScene().isLoaded)
        {
            Instantiate(deathPart, transform.position, transform.rotation);
        }
    }
    public void freezePlayer()
    {
        m_Audio.PlayOneShot(freeze_sfx);
        anim.SetBool("isAttacking", false);
        cooldown = false;
        resetval = mPI.speed;
        mPI.TakeDamage(5);
        mPI.speed = 0;
        mPI.health--;
        Debug.Log("freeze");
        Invoke("releasePlayer", 4f);
        Invoke("resetCoolDown", 8f);
    }

    public void releasePlayer()
    {
        mPI.speed = resetval;
        Debug.Log("release");
    }

    void resetCoolDown()
    {
        cooldown = true;
        Debug.Log("reset cooldown");
    }


}
