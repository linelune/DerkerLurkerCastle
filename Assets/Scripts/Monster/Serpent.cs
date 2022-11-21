using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serpent : MonoBehaviour
{
    [SerializeField]
    Transform Target;
    SpriteRenderer rend;
    Animator anim;
    Rigidbody m_Rigidbody;
    public float speed = 3f;
    public GameObject hitbox;
    private CharacterController m_Controller;
    float distance;
    bool justTP = false;
    bool tpLock = true;
    bool attackLock = true;
    float Gravity = 9.8f;
    float velocity = 0;
    int health = 50;
    bool justHit = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        m_Controller = GetComponent<CharacterController>();
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
        if (distance < 15f || health < 50)
        {
            if (!justTP)
            {
                Invoke("Teleport", 7.5f);
                justTP = true;
            }
            else
            {
                //var tpLock = transform.position;
                var dir = Target.position - transform.position;
                var movement = dir.normalized * speed * Time.deltaTime;
                if (movement.magnitude > dir.magnitude) movement = dir;
                if (tpLock)
                {
                    m_Controller.Move(movement);
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

                if (distance < 2f && tpLock && attackLock)
                {
                    StartCoroutine(attack());
                    Invoke("ResetAttack", 2f);
                    attackLock = false;

                }
                tpLock = true;
            }

        }
        

    }
    void Teleport()
    {
        //Add particle effects for teleport
        Debug.Log("Attempting Teleport");
        Vector3 targetPos = (Target.position + new Vector3(0f, 1f, 0f)) - (Target.forward * 2);
        RaycastHit hit;
        if (Physics.Raycast(targetPos, -Vector3.up, out hit))
        {
            transform.position = targetPos;
        }
        justTP = false;
        tpLock = false;
        
    }
    IEnumerator attack()
    {

        //Need to make a coroutine to effectively time hitbox
        anim.SetBool("isAttacking", true);
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
        justHit=false;
    }
}
