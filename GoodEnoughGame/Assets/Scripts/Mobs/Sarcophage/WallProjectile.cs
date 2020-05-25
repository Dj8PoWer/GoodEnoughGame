using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProjectile : MonoBehaviour
{
    float movetime = 2;
    [SerializeField] private float time = 10f;
    [SerializeField] private float speed = 8;

    public AudioClip spawn;
    AudioSource audio;

    public int strength = 10;
    public string target = "";

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(spawn, 0.8F);
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {

        if (movetime <= 0 )
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            movetime -= Time.deltaTime;

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
        }
    }
}
