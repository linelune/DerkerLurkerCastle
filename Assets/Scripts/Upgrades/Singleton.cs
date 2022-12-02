using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Singleton : MonoBehaviour
{
    private static Singleton _instance;
    public static Singleton Instance
    {
        get { return _instance; }
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else
            {
                Destroy(value.gameObject);
            }
        }
    }
    private void Awake()
    {
        Instance = this;
    }
}