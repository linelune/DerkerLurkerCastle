using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public float playerSpeed = 5f;
    public int playerHealth = 100;
    public float playerJumpHeight = 1f;
    public GameObject upgradeMenu;
   
    // Start is called before the first frame update
    void Start()
    {
        
        upgradeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Y].wasPressedThisFrame)
        {
            if(!upgradeMenu.active)
            {

                upgradeMenu.SetActive(true);
                //Time.timeScale = 0.1f;
            }
            else
            {
                upgradeMenu.SetActive(false);
                //Time.timeScale = 1f;            
            }
        }
    }
    public void UpgradeSpeed()
    {
        Debug.Log("Upgrading Speed: " + playerSpeed);
        playerSpeed += 0.5f;
    }
    public void UpgradeHealth()
    {
        playerHealth += 10;
    }
    public void UpgradeJump()
    {
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
