using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public Slider slider;

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    private void Update()
    {
        if (slider.enabled)
            AudioListener.volume = slider.value;
    }

    public void JoinOnlineLooby()
    {
        SceneManager.LoadScene("LoginMenu");
    }
}
