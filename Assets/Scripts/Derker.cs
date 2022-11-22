using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derker : MonoBehaviour
{
    [SerializeField]
    Transform Target;
    public float ChaseSpeed;
    public GameObject Face;
    private bool justCaught = false;
    private Vector3 pos1;
    private Vector3 pos2;
    public float m_Bobspeed = 1f;
    bool speeding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Need to add some degree of speed attenuation. maybe every minute he gets .01 faster
        if (!speeding)
        {
            Invoke("increaseSpeed", 60f);
            speeding = true;
        }
        pos1 = new Vector3(transform.position.x, 0.5f, transform.position.z);
        pos2 = new Vector3(transform.position.x, 2f, transform.position.z);
        Face.transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(m_Bobspeed * Time.time) + 1.0f) / 2.0f);
        if (!justCaught)
        {
            Target = GameObject.FindWithTag("Player").transform;
            if (Mathf.Abs((transform.position - Target.position).magnitude) > 40.0f)
            {
                teleport();
            }
            gameObject.transform.position = Vector3.MoveTowards(transform.position, Target.position, ChaseSpeed);
        }
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

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if(col.tag == "Player")
        {
            justCaught=true;
            gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            //Debug.Log("Caught Player");
            col.gameObject.GetComponent<PlayerMotor>().maxHealth -= 25;
            Debug.Log("Caught! Max health = " + col.gameObject.GetComponent<PlayerMotor>().maxHealth);
            Invoke("teleport", 20.0f);
        }
    }

    private void increaseSpeed()
    {
        ChaseSpeed += 0.001f;
        speeding = false;
    }
}
