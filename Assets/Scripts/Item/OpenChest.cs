using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
float distance;
Transform Target;
Animator mAnimator;
public GameObject mCoinPrefab;
public int content=3;
public int radius=2;
    // Start is called before the first frame update
    void Start()
    {
    mAnimator=GetComponentInChildren<Animator>();
    
        
    }

    // Update is called once per frame
    void Update()
    {
    Target=GameObject.FindWithTag("Player").transform;
     
     
     distance=(Target.position-transform.position).magnitude;
     if(distance<3f &&Input.GetButtonDown("Interact"))
     {
     StartCoroutine(openChest());
     }
        
    }
    
    public IEnumerator openChest()
    {
    mAnimator.SetBool("Open",true);
    yield return new WaitForSeconds(3);
    Debug.Log("I opened chest");
    for(int i=0;i<content;i++){
    Instantiate(mCoinPrefab,(transform.position+(Vector3)(radius * UnityEngine.Random.insideUnitCircle)),Quaternion.identity);
    yield return new WaitForSeconds(1);
    Debug.Log("Coin !");
    
    
    
    }
    }
    
   
    
    
    
}
