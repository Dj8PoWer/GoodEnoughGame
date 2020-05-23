using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void StartTimer()
    {
        image.SetActive(true);
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        for (int i = 180; i >= 0; i--)
        {
            text.text = (i / 60).ToString() + " : " + (i % 60 >= 10? (i % 60).ToString() : "0" + (i % 60).ToString());
            yield return new WaitForSeconds(1f);
        }
        text.text = "";
        image.SetActive(false);

    }
}
