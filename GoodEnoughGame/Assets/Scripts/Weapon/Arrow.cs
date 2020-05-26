using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float time = 2f;
    [SerializeField] private float speed;
    
    public AudioClip spawn;
    AudioSource audio;
    
    public int strength = 10;
    public string target = "";

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(spawn, 0.8F);
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
            Destroy(gameObject);
        }
        else if (other.CompareTag("Witch") && target == "mob")
        {
            Witch p = other.GetComponent<Witch>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Zombie") && target == "mob")
        {
            Zombie p = other.GetComponent<Zombie>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Skeleton") && target == "mob")
        {
            Skeleton p = other.GetComponent<Skeleton>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("GhostBoss") && target == "mob")
        {
            GhostBoss p = other.GetComponent<GhostBoss>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Snake") && target == "mob")
        {
            var p = other.GetComponent<Snake>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Scorpion") && target == "mob")
        {
            var p = other.GetComponent<Scorpion>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Phantom") && target == "mob")
        {
            var p = other.GetComponent<Phantom>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Haunted") && target == "mob")
        {
            var p = other.GetComponent<Haunted>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Chest") && target == "mob")
        {
            var p = other.GetComponent<Chest>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Sarco") && target == "mob")
        {
            var p = other.GetComponent<Sarcophagus>();
            p.TakeDamage(strength);
            Destroy(gameObject);
        }
    }
}