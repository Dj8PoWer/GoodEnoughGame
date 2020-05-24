using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    [SerializeField] private float speed = 5f;

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
            p.TakeDamage(strength);
            p.SpeedBuff(5, .7f);
            Destroy(gameObject);
        }
    }
}
