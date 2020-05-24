using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public AudioSource touch;
    
    
    void Start()
    {
        //empty
    }

    void Update()
    {
        //empty
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touch.Play();
        }
    }
}
