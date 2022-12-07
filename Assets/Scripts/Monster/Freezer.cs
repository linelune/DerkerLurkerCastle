using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Depreciated, combined with ghost class

public class Freezer : MonoBehaviour
{
public bool cooldown=false;
bool freezed=false;
    private float resetval;
GameObject mPlayer;
    PlayerMotor mPI;
    // Start is called before the first frame update
    void Start()
    {
    mPlayer=GameObject.FindWithTag("Player");
    mPI=mPlayer.GetComponent<PlayerMotor>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
 public void freezePlayer()
 {
 cooldown=true;
        resetval = mPI.speed;
 mPI.speed=0;
 mPI.health--;
 Debug.Log("freeze");
 Invoke("releasePlayer",4f);
 Invoke("resetCoolDown",8f);
 }
        
        
        
        
  
  
  public void releasePlayer()
  {
  mPI.speed=resetval;
  Debug.Log("release");
  }
  
  void resetCoolDown()
  {
  cooldown=false;
  Debug.Log("reset cooldown");
  }
  
  
  
  
  
  
  
}
