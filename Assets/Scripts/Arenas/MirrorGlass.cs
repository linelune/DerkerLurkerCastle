using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorGlass : MonoBehaviour
{
    private AudioSource glassAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            //glassAudio.Play();
            Destroy(gameObject); 
        }
    }
}
