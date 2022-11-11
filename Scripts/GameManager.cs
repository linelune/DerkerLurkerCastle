using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelBuilder lb;
    private LevelBuilder level;
    // Start is called before the first frame update
    void Start()
    {
        level = Instantiate(lb, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void restartGame()
    {
        Destroy(level);
        level = Instantiate(lb, gameObject.transform.position, gameObject.transform.rotation);
    }
}
