using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    public GameObject[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var obj in inventory)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
            foreach(var obj in inventory)
                obj.SetActive(!obj.activeInHierarchy);
    }
}
