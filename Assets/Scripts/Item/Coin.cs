using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject model;
    //private bool collectible = false;
    //private GameObject pot;
    private AudioSource m_Audio;
    public AudioClip bounce_sfx;
    private int max_bounces = 4;
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 35 * Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //other.GetComponent<PlayerInteractions>().mCoins+=1;
            GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>().coins += 1;
            Debug.Log("We got coin");
            StartCoroutine(cleanup());
        }

    }
    void OnCollisionEnter(Collision col)
    {
        if (max_bounces > 0)
        {
            m_Audio.PlayOneShot(bounce_sfx, 0.1f);
            max_bounces -= 1;
        }
    }

    private IEnumerator cleanup()
    {
        Destroy(model);
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
