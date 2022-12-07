using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] Slider mouseSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("sensitivityXnY"))
        {
            PlayerPrefs.SetFloat("sensitivityXnY", 20);
            Load();
        }
        else
            Load();
    }
    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivityXnY", mouseSlider.value);
        PlayerLook.xSensitivity = PlayerPrefs.GetFloat("sensitivityXnY");
        PlayerLook.ySensitivity = PlayerPrefs.GetFloat("sensitivityXnY");
        Save();
    }
    private void Load()
    {
        mouseSlider.value = PlayerPrefs.GetFloat("sensitivityXnY");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("sensitivityXnY", mouseSlider.value);
    }
}
