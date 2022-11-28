using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    Transform Target;

    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target = GameObject.FindWithTag("Player").transform;
        distance = (Target.position - transform.position).magnitude;
        lookAt();
    }

    void lookAt()
    {

        var delta = Target.position - transform.position;
        delta.x = delta.z = 0;
        transform.LookAt(Target.transform.position - delta);
        //var rotation = Quaternion.LookRotation(delta);
        //Using slerp here causes the bilboarding to fail. The sprite should ALWAYS point at the camera, there shouldn't be any movement damping
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*Damping);
        //transform.rotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
    }
}
