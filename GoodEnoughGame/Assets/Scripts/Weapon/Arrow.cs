using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float time = 2f;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {

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
}
