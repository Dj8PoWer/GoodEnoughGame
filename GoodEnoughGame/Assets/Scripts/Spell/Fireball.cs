﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    [SerializeField] private float speed;

    [SerializeField]
    GameObject explosion;

    public int strength = 10;
    public string target = "";

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && target == "player")
        {
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength);
            if (speed != 0)
                StartCoroutine(Explode());
        }
        else if (other.CompareTag("Witch") && target == "mob")
        {
            Witch p = other.GetComponent<Witch>();
            p.TakeDamage(strength);
            if (speed != 0)
                StartCoroutine(Explode());
        }
        else if (other.CompareTag("Zombie") && target == "mob")
        {
            Zombie p = other.GetComponent<Zombie>();
            p.TakeDamage(strength);
            if (speed != 0)
                StartCoroutine(Explode());
        }
        else if (other.CompareTag("Skeleton") && target == "mob")
        {
            Skeleton p = other.GetComponent<Skeleton>();
            p.TakeDamage(strength);
            if (speed != 0)
                StartCoroutine(Explode());
        }
        else if (other.CompareTag("GhostBoss") && target == "mob")
        {
            GhostBoss p = other.GetComponent<GhostBoss>();
            p.TakeDamage(strength);
            if (speed != 0)
                StartCoroutine(Explode());
        }
        else if (other.CompareTag("Snake") && target == "mob")
        {
            var p = other.GetComponent<Snake>();
            p.TakeDamage(strength);
            if (speed != 0)
                StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        speed = 0;
        animator.SetTrigger("explode");
        transform.localScale = new Vector3(3, 3, 1);
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}