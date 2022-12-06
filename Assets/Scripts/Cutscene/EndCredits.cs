using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class EndCredits : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        //main menu
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Level_1");
        }
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //main menu
        SceneManager.LoadScene("Level_1");
    }
}
