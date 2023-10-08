using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// minigame manager
public class MiniGameManager : MonoBehaviour
{
    public Board board;
    public CanvasGroup gameover;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI preditedScoreText;
    public TextMeshProUGUI timeText;
    public int score;
    public int predictedScore;
    public int time;
    public CanvasGroup submit;
    public int totalTime = 20;

    // start the game when loading the stage
    private void Start()
    {
        NewGame();
    }

    // identify if has history data, if has, then load, if not then start new data.
    // the time will change depends on the level.
    public void NewGame()
    {
        if (PlayerPrefs.HasKey("cwscore"))
        {
            score = PlayerPrefs.GetInt("cwscore");
        }
        else
        {
            score = 0;
        }
        if (PlayerPrefs.HasKey("predictedScore"))
        {
            predictedScore = PlayerPrefs.GetInt("predictedScore");
        }
        else
        {
            predictedScore = 0;
        }
        if (PlayerPrefs.HasKey("cwtime"))
        {
            time = Mathf.Min(totalTime - PlayerPrefs.GetInt("timePassed"), PlayerPrefs.GetInt("cwtime"));
        }
        else
        {
            if (PlayerPrefs.GetInt("year") == 1)
            {
                time = 20;
            }
            else if (PlayerPrefs.GetInt("year") == 2)
            {
                time = 40;
            }
            else if (PlayerPrefs.GetInt("year") == 3)
            {
                time = 60;
            }
            
            PlayerPrefs.SetInt("cwtime",time);
        }

        if (PlayerPrefs.HasKey("startCW"))
        {
            PlayerPrefs.SetInt("startCW", 0);
        }
        else
        {
            PlayerPrefs.SetInt("startCW", 1);
        }
        
        // make gameover not visiable and not interactable
        gameover.alpha = 0f;
        gameover.interactable = false;
        
        // clean the board
        board.CleanBoard();
        
        // generate new tiles
        board.GenerateNewTile();
        board.GenerateNewTile();
        
        // start the board functions
        board.enabled = true;
        
        // enable the submit button
        submit.alpha = 1f;
        submit.interactable = true;
    }

    // update the score
    private void Update()
    {
        preditedScoreText.text = "Predicted Score: " + PlayerPrefs.GetInt("predictedScore",0).ToString();
        timeText.text = "Time to Deadline: " + PlayerPrefs.GetInt("cwtime",20).ToString();
    }

    // game over 
    // save playing history depends on different levels
    // make sure the player able to open the minigame on time and no mistake
    // also make sure the player open the minigame in order.
    // save player history score for future score calculating
    public void GameOver()
    {
        if (PlayerPrefs.GetInt("year") == 1)
        {
            PlayerPrefs.SetInt("score1.1", PlayerPrefs.GetInt("cwscore",40));
        }
        else if (PlayerPrefs.GetInt("year") == 2)
        {
            if (PlayerPrefs.HasKey("learned-1") && !PlayerPrefs.HasKey("learned-2"))
            {
                PlayerPrefs.SetInt("score2.1", PlayerPrefs.GetInt("cwscore",40));
            }
            else if (PlayerPrefs.HasKey("learned-2"))
            {
                PlayerPrefs.SetInt("score2.2", PlayerPrefs.GetInt("cwscore",40));
            }
        }
        else if (PlayerPrefs.GetInt("year") == 3)
        {
            if (PlayerPrefs.HasKey("learned-1") && !PlayerPrefs.HasKey("learned-2"))
            {
                PlayerPrefs.SetInt("score3.1", PlayerPrefs.GetInt("cwscore",40));
            }
            else if (PlayerPrefs.HasKey("learned-2") && !PlayerPrefs.HasKey("learned-3"))
            {
                PlayerPrefs.SetInt("score3.2", PlayerPrefs.GetInt("cwscore",40));
            }
            else if (PlayerPrefs.HasKey("learned-3"))
            {
                PlayerPrefs.SetInt("score3.3", PlayerPrefs.GetInt("cwscore",40));
            }
        }
        
        // when the game is over, delete all related data
        PlayerPrefs.DeleteKey("cwtime");
        PlayerPrefs.DeleteKey("startCW");
        PlayerPrefs.DeleteKey("cwscore");
        PlayerPrefs.DeleteKey("timePassed");
        PlayerPrefs.DeleteKey("predictedScore");
        PlayerPrefs.DeleteKey("timeStartCW");

        // set played history
        if (!PlayerPrefs.HasKey("learned-1"))
        {
            PlayerPrefs.SetInt("learned-1",1);
        }
        else if (PlayerPrefs.HasKey("learned-1") && !PlayerPrefs.HasKey("learned-2"))
        {
            PlayerPrefs.SetInt("learned-2",1);
        }
        else if (PlayerPrefs.HasKey("learned-2") && !PlayerPrefs.HasKey("learned-3"))
        {
            PlayerPrefs.SetInt("learned-3",1);
        }
        
        // final score will depend on the chance
        // make sure player not able to play when the game is over
        // make sure score is not too high (over 100)
        // make sure score is not too low (lower than 40)
        float possibility = UnityEngine.Random.Range(0, 1f);
        board.enabled = false;
        gameover.interactable = true;
        gameover.alpha = 0.86f;
        submit.interactable = false;
        submit.alpha = 0f;
        if (possibility < 0.3f)
        {
            score = predictedScore;
        }
        else if (possibility < 0.6f)
        {
            score = predictedScore - 10;
        }
        else
        {
            score = predictedScore + 5;
        }

        if (score >= 100)
        {
            score = UnityEngine.Random.Range(85,94);
        }

        if (score < 40)
        {
            score = UnityEngine.Random.Range(30,50);
        }
        scoreText.text = "Score: " + score.ToString();
    }

    // add score when make move
    public void AddScore(int number)
    {
        predictedScore += number;
        PlayerPrefs.SetInt("predictedScore",predictedScore);
    }

    // time descrease function, also save time in history
    public void DecreaseTime()
    {
        time--;
        if (time <= 0)
        {
            GameOver();
        }
        PlayerPrefs.SetInt("cwtime",time);
        PlayerPrefs.SetFloat("time", PlayerPrefs.GetFloat("time") - 5f);
    }

    // if game is over, will move to the scene accordingly
    public void NextScene()
    {
        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            SceneManager.LoadScene($"1.1");
        }
        else if (PlayerPrefs.GetInt("year",1) == 2)
        {
            SceneManager.LoadScene($"2.1");
        }
        else if (PlayerPrefs.GetInt("year",1) == 3)
        {
            SceneManager.LoadScene($"3.1");
        }
        
    }
}
