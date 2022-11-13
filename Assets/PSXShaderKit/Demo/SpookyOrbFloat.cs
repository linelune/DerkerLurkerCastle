using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSXShaderKit
{
    public class SpookyOrbFloat : MonoBehaviour
    {
        [SerializeField]
        private float MinHeight;
        [SerializeField]
        private float MaxHeight;

        // Update is called once per frame
        void Update()
        {
            float yPos = Mathf.Lerp(MinHeight, MaxHeight, (Mathf.Sin(Time.time * 0.65f) + 1) * 0.5f);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }
}
