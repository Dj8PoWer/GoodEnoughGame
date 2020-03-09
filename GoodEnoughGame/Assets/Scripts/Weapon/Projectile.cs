﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float time = 2f;
    [SerializeField] private float speed;
    public Vector2 mousePos;
    public Rigidbody2D projectile;

    public AudioClip spawn;
    AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Rigidbody2D>();

        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 120) - 60);
        Vector3 vect = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        Debug.Log(vect);
        Vector3 rotateVector = rotation * vect;
        Debug.Log(rotateVector);

        projectile.AddForce(rotateVector * 10);

        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(spawn, 0.8F);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVector= new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        currentVector.Normalize();
        projectile.AddForce(currentVector * Time.deltaTime * speed);

        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
