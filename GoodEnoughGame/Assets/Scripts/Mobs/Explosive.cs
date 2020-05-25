using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float time = 7f;
    [SerializeField] private float speed;
    public int strength = 5;

    public float betweenExplode = 2f;
    
    public AudioClip spawn;
    AudioSource audio;
    
    public GameObject proj;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(spawn, 0.8F);
        StartCoroutine("Explode");
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
    
    void ExplodeOne()
    {
        for (int i = 0; i < 360; i+=60)
        {
            var Object = Instantiate(proj, transform.position, Quaternion.Euler(Vector3.forward * i));
            var projectil = Object.GetComponent<Coin>();
            projectil.strength = strength;
            projectil.target = "player";
        }
    }
    
    IEnumerator Explode()
    {
        ExplodeOne();
        yield return new WaitForSeconds(betweenExplode);
        ExplodeOne();
        yield return new WaitForSeconds(betweenExplode);
        ExplodeOne();
    }
    
    private Quaternion RotateTowards(Vector2 target, float offset = 0f)
    {
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}