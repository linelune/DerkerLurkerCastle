using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDerker : MonoBehaviour
{

    private float targetDistance = 20f;
    private float targetHeight = 7f;
    private float thrust = 0.75f;
    private Rigidbody rb;
    public Transform emitter;
    public GameObject reflectable;
    public Rigidbody avoidable;
    public GameObject clone;
    public GameObject cloneParticles;
    private bool shootA = true;
    private bool shootR = true;
    private bool spawnClone = true;
    private bool canSwap = true;
    private bool justHit = false;
    private GameObject target;
    private Vector3 dir;
    private SpriteRenderer rend;

    private Animator anim;
    private enum states{PROJECTILE, CLONE, DIE };
    private states state;

    private int health = 1200;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        state = states.PROJECTILE;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            StartCoroutine(die());
        }
        if(canSwap){
            canSwap = false;
            StartCoroutine(SwapState());
        }
        dir = target.transform.position - gameObject.transform.position;
        if (dir.magnitude > targetDistance)
        {
            rb.AddForce(dir.normalized * thrust );
        }
        else
        {
            rb.AddForce(-dir.normalized * thrust);
        }
        if(transform.position.y > targetHeight -1f)
        {
            rb.AddForce(-Vector3.up * thrust);
        }
        if (transform.position.y < targetHeight + 1f)
        {
            rb.AddForce(Vector3.up * thrust);
        }
        if (state == states.PROJECTILE)
        {
            targetHeight = 5f;
            if (shootA)
            {
                
                anim.SetBool("isBeaming", true);
                Invoke("ShootAvoidable", 5f);
                shootA = false;
            }
        }
        if(state == states.CLONE)
        {
            targetHeight = 3.5f;
            if (spawnClone)
            {
                anim.SetBool("isCloning", true);
                Invoke("resetClone", 2f);
                spawnClone = false;
            }
        }
    }
    void ShootReflectable()
    {

            shootR = true; 
    }
    void ShootAvoidable()
    {
        
        shootA = true;
    }

    void shootBeam()
    {
        anim.SetBool("isBeaming", false);
        if (shootR)
        {
            Instantiate(reflectable, emitter.position, emitter.rotation);
            shootR = false;
            Invoke("ShootReflectable", 10f);
        }
        else
        {
            Rigidbody shot = Instantiate(avoidable, emitter.position, emitter.rotation);
            shot.velocity = ((target.transform.position + new Vector3(0f, 1.5f, 0f)) - emitter.transform.position).normalized * 30f;
        }
        
    }

    void SpawnClone()
    {
        anim.SetBool("isCloning", false);
        if (state == states.CLONE)
        {
            var pos = transform.position + new Vector3(0f, 5f, 0f) + Random.insideUnitSphere * 5;
            Instantiate(cloneParticles, pos, transform.rotation);
            Instantiate(clone, pos, transform.rotation);
        }
        //spawnClone = true;
    }
    void resetClone()
    {
        spawnClone = true;
    }
    IEnumerator SwapState()
    {
        float delay = Random.Range(10f, 30f);
        yield return new WaitForSeconds(delay);
        if(state == states.PROJECTILE)
        {
            state = states.CLONE;
        }
        else
        {
            state = states.PROJECTILE;
        }
        canSwap = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttack" && !justHit)
        {
            justHit = true;
            //m_Audio.PlayOneShot(damage_sfx, 0.5f);
            anim.SetBool("isHurting", true);
            Invoke("resetHit", .1f);
            health -= col.gameObject.GetComponent<PlayerHitbox>().getDamage();
            //Destroy(col.gameObject);


        }
    }

    void resetHit()
    {
        justHit = false;
        anim.SetBool("isHurting", false);
    }
    public void takeDamage(int dmg)
    {
        anim.SetBool("isHurting", true);
        Invoke("resetHit", .1f);
        health -= dmg;
    }

    IEnumerator die()
    {
        state = states.DIE;
        anim.SetBool("isHurting", true);
        for (float i = 5; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, i/5);
            yield return null;
        }
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0f);
        //Load scene or something here
    }
}
