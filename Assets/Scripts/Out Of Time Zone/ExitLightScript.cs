using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLightScript : MonoBehaviour
{
    public Transform playerSoul;

    void Update()
    {
        if (Vector3.Distance(transform.position, playerSoul.position) < 2.0f)
        {
            SceneManager.LoadScene("Level_1");

            UIManager.menusPanel.SetActive(false);
            UIManager.inGameUI.SetActive(true);
            UIManager.pauseMenu.SetActive(false);
            UIManager.healthBarAndCoin.SetActive(true);
        }
    }
}
