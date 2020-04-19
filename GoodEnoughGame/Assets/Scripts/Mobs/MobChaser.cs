using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChaser : MonoBehaviour
{
    public float speed = 1.5f;
    public float stopping = 0f;
    public int health = 50;
    
    private GameObject target;

    private Animator animMobChaser;
    
    void Start()
    {
        animMobChaser = GetComponent<Animator>();
    }

    void Update()
    {
        target = GameObject.FindWithTag("Player");
        if (Vector2.Distance(transform.position, target.transform.position) > stopping)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        //animPlayer.SetBool("Hurt", true);
        if (health <= 0)
        {
            //animPlayer.SetBool("Dying", true);
            Destroy(gameObject);
        }
    }
}
