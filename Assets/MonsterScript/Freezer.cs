using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
public bool cooldown=false;
bool freezed=false;
GameObject mPlayer;
PlayerInput mPI;
    // Start is called before the first frame update
    void Start()
    {
    mPlayer=GameObject.FindWithTag("Player");
    mPI=mPlayer.GetComponent<PlayerInput>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
 public void freezePlayer()
 {
 cooldown=true;
 mPI.speed=0;
 mPI.life--;
 Debug.Log("freeze");
 Invoke("releasePlayer",4f);
 Invoke("resetCoolDown",8f);
 }
        
        
        
        
  
  
  void releasePlayer()
  {
  mPI.speed=10;
  Debug.Log("release");
  }
  
  void resetCoolDown()
  {
  cooldown=false;
  Debug.Log("reset cooldown");
  }
  
  
  
  
  
  
  
}
