using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    int speed;

    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Deplacement();
    }

    void Deplacement()
    {
        Vector2 deplacement = new Vector2(0,0);
        if (Input.GetKey(KeyCode.W))
        {
            deplacement.y++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            deplacement.x--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            deplacement.y--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            deplacement.x++;
        }
        bool diag = deplacement.x != 0 && deplacement.y != 0;
        transform.Translate(deplacement * speed * Time.deltaTime * (diag? Mathf.Sin(Mathf.Sqrt(2)/2) : 1));
        if (diag)
            renderer.material.color = Color.red;
        else
            renderer.material.color = Color.green;
    }
}
