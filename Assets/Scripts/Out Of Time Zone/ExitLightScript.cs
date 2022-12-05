using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLightScript : MonoBehaviour
{
    public Transform playerSoul;


    void Start()
    {
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerSoul.position) < 2.0f)
        {
            // TO DO - Go back to life;
            SceneManager.LoadScene("Level_1");
        }
    }
}
