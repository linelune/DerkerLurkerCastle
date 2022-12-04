using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glutony : MonoBehaviour
{
  [SerializeField]
Transform Target;
//float Damping=1f;
[SerializeField] Material mMawake;
[SerializeField] Material mMasleep;
//[SerializeField] Material mMfreeze;
DisplayManager mDM;
[SerializeField] GameObject mAcidPrefab;
Vector3 mSpawnpos;

float distance;
public float damage;
bool awake;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
    mDM=GetComponentInChildren<DisplayManager>();
    throwAcid();
    
    
    
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
     if(distance<3f)
     {
     
     mDM.mMat=mMawake;
     awake=true;
     
     
     
     }
     else
     {
     mDM.mMat=mMasleep;
     awake=false;
     }
     
    
    
        
    }
    
    void lookAt ()
  {
        //Locks x and z axis, rotates y to face camera
        //More akin to Doom/Hexen/Heretic style classic billboarding
        var delta = Target.position - transform.position;
        delta.x = delta.z = 0;
        transform.LookAt(Target.transform.position - delta);
        /*
      var delta = Target.position - transform.position;
      delta.z = 0;
      var rotation = Quaternion.LookRotation(delta);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*Damping);
  */
    }

  
  void throwAcid()
    {
    
    //instantiate
    if(awake){
    Debug.Log("acid!");
    mSpawnpos = Random.insideUnitSphere * 4+transform.position;
    mSpawnpos.y=0;
    Instantiate(mAcidPrefab,mSpawnpos,Quaternion.identity);
    }
    
    Invoke("throwAcid",3f);
    
    
    
    
    }
}
