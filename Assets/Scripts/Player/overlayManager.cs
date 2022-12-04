using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class overlayManager : MonoBehaviour
{
    public Image damageOverlay;
    public Image speedOverlay;
    public Image invulnOverlay;
    public Image moonOverlay;
    public Image derkerOverlay;
    public Image deathOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        StartCoroutine(FadeImage(damageOverlay, 0f));
    }

    public void Invuln()
    {
        StopCoroutine(FadeImage(invulnOverlay, 15f));
        StartCoroutine(FadeImage(invulnOverlay, 15f));
    }
    public void Speed()
    {
        StopCoroutine(FadeImage(speedOverlay, 15f));
        StartCoroutine(FadeImage(speedOverlay, 15f));
    }
    public void Moon()
    {
        StopCoroutine(FadeImage(moonOverlay, 15f));
        StartCoroutine(FadeImage(moonOverlay, 15f));
    }
    public void Derker()
    {
        StartCoroutine(FadeImage(derkerOverlay, 0f));
    }

    public void Death()
    {
        StartCoroutine(ReverseFadeImage(deathOverlay, 1.2f));
    }

    IEnumerator FadeImage(Image img, float pUpDelay)
    {

        img.color = new Color(img.color.r, img.color.g, img.color.b, .5f);
        yield return new WaitForSeconds(pUpDelay);
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }

    IEnumerator ReverseFadeImage(Image img, float pUpDelay)
    {
        yield return new WaitForSeconds(pUpDelay);
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }
}
