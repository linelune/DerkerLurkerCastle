using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_script : MonoBehaviour
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
DisplayManager mDM;
[SerializeField] GameObject mLittleRockPrefab;
Vector3 mSpawnpos;
List<GameObject> mSpawnRocks=new List<GameObject>();
GameObject lastSpawned;

float distance;
public float damage;
bool awake;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        m_Controller = GetComponent<CharacterController>();
        //rend.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    //mDM=GetComponentInChildren<DisplayManager>();
    spawn_rocks();
    
    
    
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
     if(distance<5f)
     {
     
     
            anim.SetBool("isAwake", true);
        awake=true;
     
     
     
     }
     //Chase distance longer than awake distance
     if(distance > 10f)
     {
     
            anim.SetBool("isAwake", false);
            awake =false;
     }
        if (awake)
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



    }
    
    
    void lookAt ()
  {
        //Locks x and z axis, rotates y to face camera
        //More akin to Doom/Hexen/Heretic style classic billboarding
        var delta = Target.position - transform.position;
        delta.x = delta.z = 0;
        transform.LookAt(Target.transform.position - delta);

    }
    
    public void Die()
    {
    for(int i=0;i<mSpawnRocks.Count;i++)
    {
    Destroy(mSpawnRocks[i]);
    mSpawnRocks[i]=null;
    }
    }
    
    void spawn_rocks()
    {
     if(awake){
    Debug.Log("Little rocky!");
    mSpawnpos = Random.insideUnitSphere * 4+transform.position;
    mSpawnpos.y=0;
    lastSpawned=Instantiate(mLittleRockPrefab,mSpawnpos,Quaternion.identity);
    mSpawnRocks.Add(lastSpawned);
    }
    
    Invoke("spawn_rocks",3f);
    }

  
 
}