using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public float playerSpeed = 5f;
    public int playerHealth = 100;
    public float playerJumpHeight = 1f;
    public int playerLevel = 1;
    public int coins = 0;
   
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpgradeSpeed()
    {
        
        playerSpeed += 0.5f;
        Debug.Log("Upgrading Speed: " + playerSpeed);
    }
    public void UpgradeHealth()
    {
        Debug.Log("health");
        playerHealth += 10;
    }
    public void UpgradeJump()
    {
        Debug.Log("Jump");
        playerJumpHeight += 0.2f;
    }
    public float getSpeed()
    {
        Debug.Log("Getting Speed: " + playerSpeed);
        return playerSpeed;
    }
    public int getHealth()
    {
        return playerHealth;
    }
    public float getJump()
    {
        return playerJumpHeight;
    }
}
