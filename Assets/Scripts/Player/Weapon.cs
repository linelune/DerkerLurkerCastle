using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update


    public abstract IEnumerator Attack();

    public abstract IEnumerator AltAttack();
}