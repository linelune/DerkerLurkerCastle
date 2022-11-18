using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnPowerup : MonoBehaviour
{
    public GameObject model;
    private Vector3 pos1;
    private Vector3 pos2;
    public float m_Bobspeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        pos1 = new Vector3(transform.position.x, 1f, transform.position.z);
        pos2 = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        model.transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(m_Bobspeed * Time.time) + 1.0f) / 2.0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Destroy(gameObject);
            col.gameObject.GetComponent<PlayerMotor>().InvulnPower();
        }
    }
}
