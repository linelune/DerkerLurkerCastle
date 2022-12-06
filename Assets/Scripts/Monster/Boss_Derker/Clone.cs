using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    private float targetDistance = 3.5f;
    private float targetHeight = 2.5f;
    private float thrust = 0.75f;
    private Rigidbody rb;
    public Transform emitter;
   
    public GameObject meleeHitbox;
    
    private bool canAttack = true;

    private GameObject target;
    private Vector3 dir;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dir = target.transform.position - gameObject.transform.position;
        if (dir.magnitude > targetDistance)
        {
            rb.AddForce(dir.normalized * thrust);
        }
        else
        {
            rb.AddForce(-dir.normalized * thrust);
        }
        if (transform.position.y > targetHeight - 1f)
        {
            rb.AddForce(-Vector3.up * thrust);
        }
        if (transform.position.y < targetHeight + 1f)
        {
            rb.AddForce(Vector3.up * thrust);
        }
            if (canAttack && dir.magnitude < 5f)
            {
            canAttack = false;
            StartCoroutine(atkTrigger());
                
            }

        

    }
    void Attack()
    {
        //canAttack = true;
        Instantiate(meleeHitbox, emitter.position, emitter.rotation);
        //shot.velocity = ((target.transform.position + new Vector3(0f, 1.5f, 0f)) - gameObject.transform.position).normalized * 15f;
    }
    IEnumerator atkTrigger()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(.9f);
        canAttack = true;

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttack")
        {
       
                Destroy(gameObject);
               
           

        }
    }
}
