using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossPortal : MonoBehaviour
{
    private GameObject target;
    public string bossScene;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 2.0f)
        {
            // TO DO - Go back to life;
            int t = Random.Range(0, 2);
            if (t >= 1)
            {
                SceneManager.LoadScene("Ghostknight_Boss");
            }
            else
            {
                SceneManager.LoadScene("Goliath Boss");
            }
        }
    }
}
