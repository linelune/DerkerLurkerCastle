using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] public static string sceneHistory { get; set; }
    public GameObject mainMenuPanel;
    public GameObject controlPanel;
    public GameObject optionsPanel;
    private void Awake()
    {
        mainMenuPanel.SetActive(true);
        controlPanel.SetActive(false);
        optionsPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Out Of Time Zone");
    }
    public void Controls()
    {
        mainMenuPanel.SetActive(false);
        controlPanel.SetActive(true);
    }
    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void ReturnFromControls()
    {
        controlPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void ReturnFromOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(controlPanel.activeSelf)
                ReturnFromControls();
            else
                ReturnFromOptions();
        }
    }

}
