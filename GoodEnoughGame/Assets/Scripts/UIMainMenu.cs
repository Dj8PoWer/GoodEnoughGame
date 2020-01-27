using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public void QuitGame()
    {

    }

    public void JoinOnlineLooby()
    {
        SceneManager.LoadScene("LoginMenu");
    }
}
