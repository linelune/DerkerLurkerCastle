using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Depreciated, combined with ghost class

public class Freezer : MonoBehaviour
{
    public bool cooldown = false;
    private float resetval;
    GameObject mPlayer;
    PlayerMotor mPI;

    void Start()
    {
        mPlayer = GameObject.FindWithTag("Player");
        mPI = mPlayer.GetComponent<PlayerMotor>();
    }

    public void freezePlayer()
    {
        cooldown = true;
        resetval = mPI.speed;
        mPI.speed = 0;
        mPI.health--;

        mPI.Freeze();

        Debug.Log("Freeze !");

        Invoke("releasePlayer",4f);
        Invoke("resetCoolDown",8f);
    }

    public void releasePlayer(){
        mPI.speed = resetval;
        Debug.Log("release");
    }

    void resetCoolDown()
    {
        cooldown  =  false;
        Debug.Log("resetcooldown");
    }
}
