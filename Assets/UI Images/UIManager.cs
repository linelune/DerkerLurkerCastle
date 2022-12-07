using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static string sceneHistory { get; set; }
    public static GameObject mainMenuPanel;
    public static GameObject controlPanel;
    public static GameObject optionsPanel;
    public static GameObject menusPanel;
    public static GameObject inGameUI;
    public static GameObject UIDerker;
    public static GameObject pauseMenu;
    public static GameObject healthBarAndCoin;
    [SerializeField] public static TextMeshProUGUI coinDisplay;
    public bool calledFromMainMenu;

    private void Start()
    {
        menusPanel = GameObject.Find("MenuEnvironment"); // have main menu options and controls, walls 
        inGameUI = GameObject.Find("INGAMEUI AND PAUSE"); // healthbar coin and pause menu
        mainMenuPanel = GameObject.Find("MainMenu");
        controlPanel = GameObject.Find("KeyBindings Variant");
        optionsPanel = GameObject.Find("Options Menu");
        UIDerker = GameObject.Find("UIDERKER"); // the ghost
        pauseMenu = GameObject.Find("PauseMenu");
        healthBarAndCoin = GameObject.Find("InGameUI");
        coinDisplay = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
        UIDerker.SetActive(true);
        menusPanel.SetActive(true);
        inGameUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        controlPanel.SetActive(false);
        optionsPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Out Of Time Zone");
        menusPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        UIDerker.SetActive(false);
        inGameUI.SetActive(false);
    }
    public void Controls()
    {
        menusPanel.SetActive(true);
        UIDerker.SetActive(true);
        mainMenuPanel.SetActive(false);
        controlPanel.SetActive(true);
        inGameUI.SetActive(false);
    }
    public void Options()
    {
        menusPanel.SetActive(true);
        UIDerker.SetActive(true);
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        inGameUI.SetActive(false);
    }
    public void ReturnFromControls()
    {
        controlPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ReturnFromControlsToGame()
    {
        UIDerker.SetActive(false);
        menusPanel.SetActive(false);
        controlPanel.SetActive(false);
        inGameUI.SetActive(true);
    }
    public void ReturnFromOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ReturnFromOptionsToGame()
    {
        UIDerker.SetActive(false);
        menusPanel.SetActive(false);
        optionsPanel.SetActive(false);
        inGameUI.SetActive(true);
    }

    void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void IsFromMainMenu()
    {
        calledFromMainMenu = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menusPanel.activeSelf)
            {
                if (controlPanel.activeSelf && calledFromMainMenu)
                {
                    ReturnFromControls();
                    calledFromMainMenu = false;
                }
                else if (controlPanel.activeSelf)
                {
                    ReturnFromControlsToGame();
                }
                else if (optionsPanel.activeSelf && calledFromMainMenu)
                {
                    ReturnFromOptions();
                    calledFromMainMenu = false;
                }
                else
                {
                    ReturnFromOptionsToGame();
                }
            }
            else if(inGameUI.activeSelf)
            {
                if (pauseMenu.activeSelf)
                {
                    ResumeGame();
                }
                else
                    PauseGame();
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                healthBarAndCoin.SetActive(!inGameUI.activeSelf);
            }
        }
    }

}
