using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Playgame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("1.1");
    }
    
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
