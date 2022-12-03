using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDerker : MonoBehaviour
{
    private float targetDistance = 15f;
    private float targetHeight = 5f;
    private float thrust = 0.75f;
    private Rigidbody rb;
    public Transform emitter;
    public GameObject reflectable;
    public Rigidbody avoidable;
    private bool shootA = true;
    private bool shootR = true;
    private GameObject target;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
    void ShootReflectable()
    {
        Instantiate(reflectable, emitter.position, emitter.rotation);
        shootR = true; 
    }
    void ShootAvoidable()
    {
        shootA = true;
        Rigidbody shot = Instantiate(avoidable, emitter.position, emitter.rotation);
        shot.velocity = ((target.transform.position + new Vector3(0f,1.5f,0f)) - gameObject.transform.position).normalized * 30f;
    }
}
