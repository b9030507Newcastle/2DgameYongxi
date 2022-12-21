using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;

    private Animator am;
    private PolygonCollider2D pc2D;
    
    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        pc2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            am.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        pc2D.enabled = true;
        StartCoroutine(disableHiBoxt());
    }

    IEnumerator disableHiBoxt()
    {
        yield return new WaitForSeconds(time);
        pc2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
