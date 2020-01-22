using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerMainMenu : MonoBehaviour
{
    public void QuitGame()
    {

    }

    public void JoinOnlineLooby()
    {
        SceneManager.LoadScene("LoginMenu");
    }
}
