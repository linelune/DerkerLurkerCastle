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
    private bool shootA = true;
    private bool shootR = true;
    private bool spawnClone = true;
    private bool canSwap = true;
    private bool justHit = false;
    private GameObject target;
    private Vector3 dir;

    private enum states{PROJECTILE, CLONE };
    private states state;

    private int health = 2000;
    // Start is called before the first frame update
    void Start()
    {
        state = states.PROJECTILE;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
                Invoke("ShootAvoidable", 3f);
                shootA = false;
            }
            if (shootR)
            {
                Invoke("ShootReflectable", 10f);
                shootR = false;

            }
        }
        if(state == states.CLONE)
        {
            targetHeight = 3.5f;
            if (spawnClone)
            {
                Invoke("SpawnClone", 2f);
                spawnClone = false;
            }
        }
    }
    void ShootReflectable()
    {
        if (state == states.PROJECTILE)
        {
            Instantiate(reflectable, emitter.position, emitter.rotation);
        }
            shootR = true; 
    }
    void ShootAvoidable()
    {
        shootA = true;
        if (state == states.PROJECTILE)
        {
            Rigidbody shot = Instantiate(avoidable, emitter.position, emitter.rotation);
            shot.velocity = ((target.transform.position + new Vector3(0f, 1.5f, 0f)) - emitter.transform.position).normalized * 30f;
        }
    }

    void SpawnClone()
    {
        if (state == states.CLONE)
        {
            var pos = transform.position + new Vector3(0f, 5f, 0f) + Random.insideUnitSphere * 5;
            Instantiate(clone, pos, transform.rotation);
        }
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
    public void takeDamage()
    {
        health -= 75;
    }
}
