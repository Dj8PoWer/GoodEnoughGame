using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{

    [SerializeField] private float time = 5f;

    public int strength = 10;
    public string target = "player";
    public GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Parent.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 25));
        transform.localPosition = new Vector3(4 + Mathf.Sin(Time.time) * 2f, 0, 0);
        transform.rotation = Quaternion.identity;
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
