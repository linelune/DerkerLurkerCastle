using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
//GameObject mPlayer;
//PlayerInput mPI;
[SerializeField]
Transform Target;
float Damping=1f;
[SerializeField] Material mMawake;
[SerializeField] Material mMasleep;
//[SerializeField] Material mMfreeze;
DisplayManager mDM;
Freezer mFreezer;
float distance;
public float damage;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
    mDM=GetComponentInChildren<DisplayManager>();
    mFreezer=GetComponentInChildren<Freezer>();
    
    
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
      if(!mFreezer.cooldown && distance<2f)
     {
     mFreezer.freezePlayer();
     
     }
     
     }
     else
     {
     mDM.mMat=mMasleep;
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
}
