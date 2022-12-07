using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    [SerializeField] public static TextMeshProUGUI coinDisplay;

    private void Start()
    {
        menusPanel = GameObject.Find("MenuEnvironment");
        inGameUI = GameObject.Find("INGAMEUI AND PAUSE");
        mainMenuPanel = GameObject.Find("MainMenu");
        controlPanel = GameObject.Find("KeyBindings Variant");
        optionsPanel = GameObject.Find("Options Menu");
        UIDerker = GameObject.Find("UIDERKER");
        pauseMenu = GameObject.Find("PauseMenu");
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
