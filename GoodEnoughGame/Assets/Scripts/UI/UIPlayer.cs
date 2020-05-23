using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public GameObject[] inventory;
    public GameObject Stats; 
    public GameObject Tree;

    public GameObject EscapeMenu;

    public  Slider slider;

    // Start is called before the first frame update

    void Start()
    {
        foreach (var obj in inventory)
            obj.SetActive(false);
        Stats.SetActive(false);
        Tree.SetActive(false);
        slider.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var obj in inventory)
            {
                obj.SetActive(!obj.activeInHierarchy);
            }
            if (!Tree.activeInHierarchy)
                Stats.SetActive(!Stats.activeInHierarchy);

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Tree.SetActive(!Tree.activeInHierarchy);

            if (!inventory[0].activeInHierarchy)
                Stats.SetActive(!Stats.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            EscapeMenu.SetActive(!EscapeMenu.activeInHierarchy);

        if (slider.IsActive())
            AudioListener.volume = slider.value;
    }

    public void ExitButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }


}
