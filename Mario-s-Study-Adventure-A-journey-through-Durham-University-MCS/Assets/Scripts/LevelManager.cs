using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// control the levels
public class LevelManager : MonoBehaviour
{
    private int FinalScore;
    public TMP_Text Congra;

    // if its final year, load the final text
    private void Start()
    {
        if (PlayerPrefs.GetInt("year",1) == 3)
        {
            LoadCongra();
        }
    }

    // if second year, save the score, then delete the data
    // load the next year and set the year value
    public void Year2()
    {
        FinalScore = PlayerPrefs.GetInt("score1.1", 40);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("finalscore",FinalScore);
        PlayerPrefs.SetInt("year", 2);
        SceneManager.LoadScene("2.1");
    }
    
    // if third year, save the score, then delete the data
    // load the next year and set the year value
    public void Year3()
    {
        FinalScore = (PlayerPrefs.GetInt("finalscore", 40) + PlayerPrefs.GetInt("score2.1", 40) + PlayerPrefs.GetInt("score2.2", 40)) / 300;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("finalscore",FinalScore);
        PlayerPrefs.SetInt("year", 3);
        SceneManager.LoadScene("3.1");
    }
    
    // last text is base on the final score accordingly
    public void LoadCongra()
    {
        FinalScore = (PlayerPrefs.GetInt("finalscore", 40) + PlayerPrefs.GetInt("score3.1", 40) + 
                      PlayerPrefs.GetInt("score3.2", 40) + PlayerPrefs.GetInt("score3.3")) / 400;
        
        if (FinalScore > 40 && FinalScore < 60)
        {
            Congra.text = "You got Second lower class, I am pround of you! Hope you can apply time management skill much better! ";
        }
        else if (FinalScore > 60 && FinalScore < 70)
        {
            Congra.text = "You got Second upper class, I am pround of you! Hope you can apply time management skill much better! ";
        }
        else if (FinalScore > 70)
        {
            Congra.text = "You got FIRST class, you have so much good talent to study computer science and perfect time management skills!";
        }
        else
        {
            Congra.text = "You got PASS, not bad score, keep going! ";
        }
        
    }
    
    // if finished, back to menu
    public void Finish()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Start");
    }
    
}
