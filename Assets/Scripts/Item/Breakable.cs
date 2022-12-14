using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject mCoinPrefab;
    public GameObject[] breakableModels;
    private int content;
    public int radius=1;
    private AudioSource m_Audio;
    private GameObject pot;
    public AudioClip break_sfx;
    // Start is called before the first frame update
    void Start()
    {
        //pots contain 1-3 coins
        m_Audio = GetComponent<AudioSource>();
        content = Random.Range(1, 3);
        //Picks a random pot model when created
        int t = Random.Range(0, breakableModels.Length);
        pot = Instantiate(breakableModels[t], transform.position, transform.rotation);
        pot.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        pot.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void crushed()
    {
        m_Audio.PlayOneShot(break_sfx, 0.1f);
        
        for (int i=0;i<content;i++)
        {
            //Gives coins a random velocity so they fly around when the pot breaks
            GameObject c = Instantiate(mCoinPrefab, transform.position, transform.rotation);
            Rigidbody cr = c.GetComponent<Rigidbody>();
            float rx = Random.Range(-0.5f, 0.5f);
            float rz = Random.Range(-0.5f, 0.5f);
            cr.velocity = new Vector3(rx, 5f, rz);
            Debug.Log("Coin !");
        }
        Destroy(pot);
        GetComponent<Collider>().enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttack")
        {
           
            crushed();

        }
    }


}
