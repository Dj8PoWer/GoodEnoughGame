using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSnow : MonoBehaviour
{
    public float time = 2f;
    public float speed;

    public int strength = 10;
    public string target = "";

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
            p.TakeDOTDamage(3 , strength);
            Destroy(gameObject);
        }
    }
}