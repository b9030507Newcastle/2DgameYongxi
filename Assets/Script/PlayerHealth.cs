using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Glint;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;

    private Renderer rd;
    private Animator am;
    private PolygonCollider2D polygonCollider2D;
    
    void Start()
    {
        rd = GetComponent<Renderer>();
        am = GetComponent<Animator>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            am.SetTrigger("Die");
            Invoke("KillPlayer",dieTime);
        }
        GlintPlayer(Glint, time);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
    }

    void KillPlayer()
    {
        Destroy(gameObject);
    }

    void GlintPlayer(int numGlint, float seconds)
    {
        StartCoroutine(DoGlint(numGlint, seconds));
    }

    IEnumerator DoGlint(int numGlint, float seconds)
    {
        for (int i = 0; i < numGlint * 2; i++)
        {
            rd.enabled = !rd.enabled;
            yield return new WaitForSeconds(seconds);
        }

        rd.enabled = true;
    }
}
