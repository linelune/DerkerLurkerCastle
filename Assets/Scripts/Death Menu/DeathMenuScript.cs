using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    private SaveManager saveManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();
    }

    public void SaveAndContinue()
    {
        saveManager.SaveData();
        Application.Quit();
    }

    public void SaveAndQuit()
    {
        saveManager.SaveData();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Out Of Time Zone");
    }
}
