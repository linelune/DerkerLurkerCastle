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
    private bool bunting = false;

    // Audio
    private AudioSource m_Audio;
    public AudioClip shoot_sfx;

    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
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
        if (!reloading && !bunting)
        {
            reloading=true;
            anim.SetBool("isAttacking", true);
            m_Audio.PlayOneShot(shoot_sfx);
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
        if (!reloading && !bunting)
        {
            bunting = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, emitter.transform.forward, out hit, 2.5f))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyParent>().buntHit();
                    
                }
            }
            anim.SetBool("isBunting", true);
            m_Audio.PlayOneShot(shoot_sfx);
            yield return new WaitForSeconds(.1f);
            anim.SetBool("isBunting", false);
            yield return new WaitForSeconds(0.9f);
            bunting = false;
        }
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
