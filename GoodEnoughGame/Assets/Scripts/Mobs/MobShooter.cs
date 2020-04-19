using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobShooter : MonoBehaviour
{
    public float speed = 1.5f;
    public float stopping = 8f;
    public float back = 8f;
    public int health = 50;

    public float originalTime;
    private float time;
    
    private GameObject target;
    public GameObject proj;

    private Animator animMobShooter;
    
    void Start()
    {
        animMobShooter = GetComponent<Animator>();
        time = originalTime;
    }

    void Update()
    {
        target = GameObject.FindWithTag("Player");
        if (Vector2.Distance(transform.position, target.transform.position) > stopping)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        else if (Vector2.Distance(transform.position, target.transform.position) < stopping &&
                 Vector2.Distance(transform.position, target.transform.position) > back)
            transform.position = this.transform.position;
        
        else if (Vector2.Distance(transform.position, target.transform.position) < back)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -speed * Time.deltaTime);

        if (time <= 0)
        {
            var Object = Instantiate(proj, this.transform.position, Quaternion.identity);
            var projectil = Object.GetComponent<Projectile>();
            projectil.mousePos = target.transform.position;
            projectil.target = "player";

            time = originalTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
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
