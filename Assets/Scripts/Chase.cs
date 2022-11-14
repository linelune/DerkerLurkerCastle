using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField]
    Transform Target;
    public float ChaseSpeed;
    private bool justCaught = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = GameObject.FindWithTag("Player").transform;
        if(Mathf.Abs((transform.position - Target.position).magnitude) > 40.0f && !justCaught)
        {
            teleport();
        }
        gameObject.transform.position = Vector3.MoveTowards(transform.position, Target.position, ChaseSpeed);
    }

    void teleport()
    {
        justCaught = false;
        float rx = Random.Range(-30.0f, 30.0f);
        //float ry = Random.Range(-30.0f, 30.0f);
        float rz = Random.Range(-30.0f, 30.0f);
        gameObject.transform.position = new Vector3(Target.position.x + rx, Target.position.y, Target.position.z + rz);
        Debug.Log("Teleported");

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Player")
        {
            justCaught=true;
            gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            Debug.Log("Caught Player");
            Invoke("teleport", 20.0f);
        }
    }
}
