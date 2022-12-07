using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class OpeningCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    private VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }
    void Update()
    {
        //main menu
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //main menu
        SceneManager.LoadScene("MainMenu");
    }
}
