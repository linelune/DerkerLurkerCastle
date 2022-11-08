using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
MeshRenderer mMR;
public Material mMat;
    // Start is called before the first frame update
    void Start()
    {
    mMR=GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
    mMR.material=mMat;
        
    }
}
