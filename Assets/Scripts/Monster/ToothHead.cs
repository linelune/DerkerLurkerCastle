using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToothHead : MonoBehaviour
{
    [SerializeField]
    Transform Target;
    SpriteRenderer rend;
    Animator anim;
    public float speed = 5f;
    public GameObject hitbox;
    private CharacterController m_Controller;
    float distance;
    bool justTP = false;
    bool tpLock = true;
    bool attackLock = true;
    float Gravity = 9.8f;
    float velocity = 0;
    int health = 70;
    bool justHit = false;
    public GameObject deathPart;
    private AudioSource m_Audio;
    public AudioClip attack_sfx;
    public AudioClip damage_sfx;
    public AudioClip teleport_sfx;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        m_Controller = GetComponent<CharacterController>();
        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isAttacking", false);
        Target = GameObject.FindWithTag("Player").transform;
        distance = (Target.position - transform.position).magnitude;
        //Lighting simulation
        if (distance < 30f)
        {
            rend.color = new Color(1 - distance / 20, 1 - distance / 20, 1 - distance / 20, 1f);
        }
        else
        {
            rend.color = new Color(0.0f, 0.0f, 0.0f, 1f);
        }
        if ((distance < 15f || health < 50) && distance > 1f)
        {

                //var tpLock = transform.position;
                var dir = Target.position - transform.position;
                var movement = dir.normalized * speed * Time.deltaTime;
                if (movement.magnitude > dir.magnitude) movement = dir;

                    m_Controller.Move(movement);


                

                if (distance < 2f && attackLock)
                {
                attackLock = false;
                    StartCoroutine(attack());
                    Invoke("ResetAttack", 6f);


                }
           

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
   
    IEnumerator attack()
    {

        //Need to make a coroutine to effectively time hitbox
        anim.SetBool("isAttacking", true);
        m_Audio.PlayOneShot(attack_sfx, 0.5f);
        yield return new WaitForSeconds(.4f);
        
        //Deal damage here
        Instantiate(hitbox, transform.position, transform.rotation);
    }
    void ResetAttack()
    {
        attackLock = true;
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
        justHit=false;
    }
    void OnDestroy()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            Instantiate(deathPart, transform.position, transform.rotation);
        }
    }
}
