using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    public Rigidbody bolt;
    public GameObject model;
    public Transform emitter;
    private Animator anim;
    private bool reloading=false;
    // Start is called before the first frame update
    void Start()
    {
        anim = model.gameObject.GetComponent<Animator>();
        anim.keepAnimatorControllerStateOnDisable = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override IEnumerator Attack()
    {
        if (!reloading)
        {
            reloading=true;
            anim.SetBool("isAttacking", true);
            Rigidbody shot = Instantiate(bolt, emitter.transform.position, emitter.transform.rotation);
            shot.velocity = (emitter.transform.forward * 50);
            yield return new WaitForSeconds(.1f);
            anim.SetBool("isAttacking", false);
            yield return new WaitForSeconds(2.9f);
            reloading = false;
        }
    }

    public override IEnumerator AltAttack()
    {
        yield return null;
    }
    void OnEnable()
    {
        //Prevent bug if the crossbow is swapped mid reload
        if (reloading)
        {
            StartCoroutine("resetReload");
            
        }
    }

    private IEnumerator resetReload()
    {

        yield return new WaitForSeconds(1.5f);
        reloading = false;
    }

}
