using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    [SerializeField] private float time = 3f;

    public int strength = 10;
    public string target = "";

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        animator = GetComponent<Animator>();
        StartCoroutine(Explode());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && target == "player")
        {
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength, PlayerManager.DmgType.Water);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Witch") && target == "mob")
        {
            Witch p = other.GetComponent<Witch>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Zombie") && target == "mob")
        {
            Zombie p = other.GetComponent<Zombie>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Skeleton") && target == "mob")
        {
            Skeleton p = other.GetComponent<Skeleton>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("GhostBoss") && target == "mob")
        {
            GhostBoss p = other.GetComponent<GhostBoss>();
            p.TakeDamage(strength);
        }
        else if (other.CompareTag("Snake") && target == "mob")
        {
            var p = other.GetComponent<Snake>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Scorpion") && target == "mob")
        {
            var p = other.GetComponent<Scorpion>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Phantom") && target == "mob")
        {
            var p = other.GetComponent<Phantom>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Haunted") && target == "mob")
        {
            var p = other.GetComponent<Haunted>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Chest") && target == "mob")
        {
            var p = other.GetComponent<Chest>();
            p.TakeDamage(strength);
            p.SpeedBuff(6, .5f);
        }
        else if (other.CompareTag("Sarco") && target == "mob")
        {
            var p = other.GetComponent<Sarcophagus>();
            p.TakeDamage(strength);
        }
        else if (other.CompareTag("KnightSuperBoss") && target == "mob")
        {
            var p = other.GetComponent<KnightSuperBoss>();
            p.TakeDamage(strength);
        }

    }

    IEnumerator Explode()
    {
        Debug.Log("explode");
        yield return new WaitForSeconds(time);
        animator.SetTrigger("explode");
        GetComponent<CircleCollider2D>().enabled = true;
        transform.localScale = new Vector3(3, 3, 1);
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}
