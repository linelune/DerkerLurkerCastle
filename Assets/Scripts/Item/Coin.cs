using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject model;
    private bool collectible = false;
    // Start is called before the first frame update
    void Start()
    {
       
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
            Destroy(gameObject);
        }
    }
    private void makeCollectible()
    {
        collectible = true;
    }
}
