using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoliathBossScript : MonoBehaviour
{
    public GameObject goliath;
    public Collider goliathCollider;

    public AudioClip damage_sfx;
    public AudioClip shoot_sfx;

    public Rigidbody avoidable;
    public Transform emitter;
    private bool canShoot = false;

    public GameObject deathPart;
    public GameObject mCoinPrefab;

    public GameObject player;
    public CharacterController playerCharacterController;

    private bool justHit = false;
    private int health = 255;
    
    private AudioSource m_Audio;

    private Vector3 impact = Vector3.zero;

    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        goliathCollider = GetComponent<Collider>();
        playerCharacterController = player.GetComponent<CharacterController>();
        Invoke("resetShot", 3f);
    }

    void Update()
    {
        if (player.transform.position.x < transform.position.x + 5.0f)
            impact += new Vector3(30.0f, 0.0f, 0.0f);

        if (player.transform.position.x < transform.position.x + 2.0f)
            player.GetComponent<PlayerMotor>().TakeDamage(1000);

        if (impact.magnitude > 0.2)
            playerCharacterController.Move(impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, 2 * Time.deltaTime);
        if (canShoot)
        {
            canShoot = false;
            Invoke("resetShot", 5f);
            m_Audio.PlayOneShot(shoot_sfx, 0.4f);
            Rigidbody shot = Instantiate(avoidable, emitter.position, emitter.rotation);
            shot.velocity = ((player.transform.position + new Vector3(0f, 1.5f, 0f)) - emitter.transform.position).normalized * 15f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerAttack" && !justHit)
        {
            justHit = true;

            m_Audio.PlayOneShot(damage_sfx, 0.5f);
            Invoke(nameof(resetHit), .1f);

            health -= col.gameObject.GetComponent<PlayerHitbox>().getDamage();

            if (health <= 0)
                StartCoroutine(die());
        }
    }

    void resetHit()
    {
        justHit = false;
    }

    void resetShot()
    {
        canShoot = true;
    }
    IEnumerator die()
    {

        canShoot = false;

        goliathCollider.enabled = false;
        
        Instantiate(deathPart, transform.position + new Vector3(0f, 1.5f, 0f), transform.rotation);
        for (int i = 0; i < 20; i++)
        {
            //Gives coins a random velocity so they fly around when the pot breaks
            GameObject c = Instantiate(mCoinPrefab, transform.position, transform.rotation);
            Rigidbody cr = c.GetComponent<Rigidbody>();
            float rx = Random.Range(-0.5f, 0.5f);
            float rz = Random.Range(-0.5f, 0.5f);
            cr.velocity = new Vector3(rx, 5f, rz);
            Debug.Log("Coin !");
        }
        goliath.transform.position = new Vector3(-100f, 0f, 0f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Level_2");
    }
}
