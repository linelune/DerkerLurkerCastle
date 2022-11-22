using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject model;
    private bool collectible = false;
    private GameObject pot;
    private AudioSource m_Audio;
    public AudioClip bounce_sfx;
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
            other.GetComponent<PlayerInteractions>().mCoins+=1;
            Debug.Log("We got coin");
            StartCoroutine(cleanup());
        }

    }
    void OnCollisionEnter(Collision col)
    {
        m_Audio.PlayOneShot(bounce_sfx, 0.1f);
    }
    private void makeCollectible()
    {
        collectible = true;
    }
    private IEnumerator cleanup()
    {
        Destroy(model);
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
