using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
//GameObject mPlayer;
//PlayerInput mPI;
[SerializeField]
Transform Target;
//float Damping=1f;
[SerializeField] Material mMawake;
[SerializeField] Material mMasleep;
//[SerializeField] Material mMfreeze;
DisplayManager mDM;
Freezer mFreezer;
float distance;
    public float ChaseSpeed = 0.05f;
    private int health = 20;
    private bool justHit = false;
    private bool frozen = false;
public float damage;
    private AudioSource m_Audio;
    public AudioClip awake_sfx;
    private bool awake = false;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
    mDM=GetComponentInChildren<DisplayManager>();
    mFreezer=GetComponentInChildren<Freezer>();
        m_Audio = GetComponent<AudioSource>();
    
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
     lookAt();
     if(distance<15f)
     {
            if (!awake)
            {
                awake=true;
                m_Audio.PlayOneShot(awake_sfx, 0.5f);
            }
            if (distance > 2f)
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, Target.position + new Vector3(0f, 1.5f, 0f), ChaseSpeed);
            }
            mDM.mMat=mMawake;
            if(!mFreezer.cooldown && distance<2f)
            {
                mFreezer.freezePlayer();
                frozen=true;
     
            }
     
     }
     else
        {
            mDM.mMat=mMasleep;
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
        if (frozen) {
            mFreezer.releasePlayer();
                }
    }
}
