using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoliathBossScript : MonoBehaviour
{
    public GameObject goliath;
    public Collider goliathCollider;

    public AudioClip damage_sfx;

    public GameObject deathPart;
    public GameObject mCoinPrefab;

    public GameObject player;
    public CharacterController playerCharacterController;

    private bool justHit = false;
    private int health = 250;
    
    private AudioSource m_Audio;

    private Vector3 impact = Vector3.zero;

    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        goliathCollider = GetComponent<Collider>();
        playerCharacterController = player.GetComponent<CharacterController>();
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

    IEnumerator die()
    {
        goliath.SetActive(false);

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
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Level_2");
    }
}
