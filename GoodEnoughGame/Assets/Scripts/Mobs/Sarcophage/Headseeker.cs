using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headseeker : MonoBehaviour
{
    GameObject Target;

    [SerializeField] private float time = 5f;
    [SerializeField] private float speed;

    public int strength = 10;
    public string target = "";

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Collider2D>().enabled = false;
        var players = GameObject.FindGameObjectsWithTag("Player");
        float minimum = float.MaxValue;
        foreach (var play in players)
        {
            if (Vector2.Distance(transform.position, play.transform.position) < minimum)
            {
                minimum = Vector2.Distance(transform.position, play.transform.position);
                Target = play;
            }
        }

        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && target == "player")
        {
            PlayerManager p = other.GetComponent<PlayerManager>();
            p.TakeDamage(strength, PlayerManager.DmgType.Fire);
            if (speed != 0)
                StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(5);
        speed = 0;
        animator.SetTrigger("explode");
        transform.localScale = new Vector3(3, 3, 1);
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }


}
