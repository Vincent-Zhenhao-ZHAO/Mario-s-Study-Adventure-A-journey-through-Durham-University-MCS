using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// menu
public class Menu : MonoBehaviour
{
    // start the game, start from the first level
    // make sure theres no history data left
    public void Playgame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("1.1");
    }
    
    // open instruction
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // exit the game
    public void Exit()
    {
        Application.Quit();
    }

    // back to the menu page
    public void BackMenu()
    {
        SceneManager.LoadScene("Start");
    }
    
    // open game story
    public void GameStory()
    {
        SceneManager.LoadScene("GameStory");
    }
}
