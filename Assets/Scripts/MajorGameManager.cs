using UnityEngine;
using UnityEngine.SceneManagement;

public class MajorGameManager : MonoBehaviour
{
    public static MajorGameManager Instance { get; private set; }

    public int lives { get; private set; }
    
    public int credits { get; private set; }
    
    public int courses { get; private set; }

    private AudioSource _coinSound;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        _coinSound = GetComponent<AudioSource>();
        NewGame();
    }

    private void NewGame()
    {
        lives = PlayerPrefs.GetInt("lives",3);
        credits = PlayerPrefs.GetInt("credits",0);
        courses = PlayerPrefs.GetInt("modules",0);

        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            SceneManager.LoadScene("1.1");
        }
        else if (PlayerPrefs.GetInt("year") == 2)
        {
            SceneManager.LoadScene("2.1");
        }
        else if (PlayerPrefs.GetInt("year") == 3)
        {
            SceneManager.LoadScene("3.1");
        }
    }

    public void RetakeYear()
    {
        lives--;
        PlayerPrefs.SetInt("lives",lives);
        PlayerPrefs.DeleteKey("learning-1");
        PlayerPrefs.DeleteKey("learned-1");
        PlayerPrefs.DeleteKey("learning-2");
        PlayerPrefs.DeleteKey("learned-2");
        PlayerPrefs.DeleteKey("learning-3");
        PlayerPrefs.DeleteKey("learned-3");
        PlayerPrefs.DeleteKey("cwtime");
        PlayerPrefs.DeleteKey("startCW");
        PlayerPrefs.DeleteKey("cwscore");
        PlayerPrefs.DeleteKey("timePassed");
        PlayerPrefs.DeleteKey("modules");
        PlayerPrefs.DeleteKey("credits");
        PlayerPrefs.DeleteKey("time");
        if (lives < 0)
        {
            GameOver();
        }
        else if (lives > 0)
        {
            NewGame();
        }
    }

    public void GameOver()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("year", 1);
        SceneManager.LoadScene("GameOver");
    }

    public void GainCredits() {
        
        float chance = UnityEngine.Random.Range(0f, 1f);
        _coinSound.Play();
        if (chance < 0.2f)
        {
            credits += 0;
        }
        else if (chance < 0.5f)
        {
            credits += 1;
        }
        else if (chance < 0.8f)
        {
            credits += 2;
        }
        else
        {
            credits += 5;
        }

        if (credits >= 10)
        {
            AddModule();
            credits = 0;
        }
        
        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            if (PlayerPrefs.HasKey("learned-1"))
            {
                credits = 10;
            }
        }
        
        if (PlayerPrefs.GetInt("year",1) == 2)
        {
            if (PlayerPrefs.HasKey("learned-2"))
            {
                credits = 10;
            }
        }
        
        if (PlayerPrefs.GetInt("year",1) == 3)
        {
            if (PlayerPrefs.HasKey("learned-3"))
            {
                credits = 10;
            }
        }
        
        PlayerPrefs.SetInt("credits",credits);
    }
    public void GainExtraCredits()
    {
        _coinSound.Play();
        credits++;
        if (credits >= 10)
        {
            AddModule();
            credits = 0;
        }
        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            if (PlayerPrefs.HasKey("learned-1"))
            {
                credits = 10;
            }
        }
        
        if (PlayerPrefs.GetInt("year",1) == 2)
        {
            if (PlayerPrefs.HasKey("learned-2"))
            {
                credits = 10;
            }
        }
        
        if (PlayerPrefs.GetInt("year",1) == 3)
        {
            if (PlayerPrefs.HasKey("learned-3"))
            {
                credits = 10;
            }
        }
        
        PlayerPrefs.SetInt("credits",credits);
    }

    public void AddModule()
    {
        courses++;
        
        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            PlayerPrefs.SetInt("learning-1",1);
            courses = 1;
            
        }
        
        if (PlayerPrefs.GetInt("year",1) == 2)
        {
            if (!PlayerPrefs.HasKey("learning-1"))
            {
                PlayerPrefs.SetInt("learning-1",1);  
                courses = 1;
            }
            else if (!PlayerPrefs.HasKey("learning-2") && PlayerPrefs.HasKey("learned-1"))
            {
                PlayerPrefs.SetInt("learning-2",1); 
                courses = 2;
            }
            else if (PlayerPrefs.HasKey("learned-2"))
            {
                courses = 3;
            }
        }
        
        if (PlayerPrefs.GetInt("year",1) == 3)
        {
            if (!PlayerPrefs.HasKey("learning-1"))
            {
                PlayerPrefs.SetInt("learning-1",1); 
                courses = 1;
            }
            else if (!PlayerPrefs.HasKey("learning-2") && PlayerPrefs.HasKey("learned-1"))
            {
                PlayerPrefs.SetInt("learning-2",1);   
                courses = 2;
            }
            else if (!PlayerPrefs.HasKey("learning-3") && PlayerPrefs.HasKey("learned-2"))
            {
                PlayerPrefs.SetInt("learning-3",1);
                courses = 3;
            }
            else if (PlayerPrefs.HasKey("learned-2"))
            {
                courses = 3;
            }
        }
        PlayerPrefs.SetInt("modules",courses);
    }

    public void TimePunishment()
    {
        if (credits <= 0)
        {
            credits = 0;
        }
        else if (credits > 0)
        {
            credits -= 1;   
        }
        if (courses <= 0)
        {
            courses = 0;
        }
        else if (courses > 0)
        {
            courses -= 1;   
        }

        if (PlayerPrefs.GetInt("time") < -5)
        {
           // WaitForSeconds(3f);d
            RetakeYear();
        }
        PlayerPrefs.SetInt("credits",credits);
        PlayerPrefs.SetInt("modules",courses);
    }
    
    public void Punishment()
    {
        if (credits <= 0)
        {
            credits = 0;
        }
        else if (credits > 0)
        {
            credits -= 1;   
        }
        PlayerPrefs.SetInt("credits",credits);
    }

    public void GoSecondYear()
    {
        SceneManager.LoadScene("FirstYearCong");
    }
    
    public void GoThirdyear()
    {
        SceneManager.LoadScene("SecondYearCong");
    }
    
    public void FinishThird()
    {
        SceneManager.LoadScene("FinalCong");
    }
}
