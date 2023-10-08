using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// CW manager
public class CW : MonoBehaviour
{
    public GameObject Cw1;
    public GameObject Cw2;
    public GameObject Cw3;
    public GameObject Cw1btn;
    public GameObject Cw2btn;
    public GameObject Cw3btn;

    // check if unlock next CW
    private void Update()
    {
        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            // cw1
            if (PlayerPrefs.HasKey("learning-1"))
            {
                makeBtn1();
            }

            if (PlayerPrefs.HasKey("learned-1"))
            {
                Cw1btn.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("year") == 2)
        {
            // cw1
            if (PlayerPrefs.HasKey("learning-1"))
            {
                makeBtn1();
            }
            else if (PlayerPrefs.HasKey("learning-1") && !PlayerPrefs.HasKey("learned-1"))
            {
                makeBtn1();
            }
            
            if (PlayerPrefs.HasKey("learned-1"))
            {
                Cw1btn.SetActive(false);
            }
            
            // cw2
            if (PlayerPrefs.HasKey("learning-2") && PlayerPrefs.HasKey("learned-1"))
            {
                makeBtn2();
            }
            else if (PlayerPrefs.HasKey("learning-2") && !PlayerPrefs.HasKey("learned-1"))
            {
                makeBtn2();
            }

            if (PlayerPrefs.HasKey("learned-1"))
            {
                Cw1btn.SetActive(false);
            }
            
            if (PlayerPrefs.HasKey("learned-2"))
            {
                Cw2btn.SetActive(false);
            }
            
        }

        if (PlayerPrefs.GetInt("year") == 3)
        {
            // cw1
            if (PlayerPrefs.HasKey("learning-1"))
            {
                makeBtn1();
            }
            else if (PlayerPrefs.HasKey("learning-1") && !PlayerPrefs.HasKey("learned-1"))
            {
                makeBtn1();
            }
            
            if (PlayerPrefs.HasKey("learned-1"))
            {
                Cw1btn.SetActive(false);
            }
            
            // cw2
            if (PlayerPrefs.HasKey("learning-2") && PlayerPrefs.HasKey("learned-1"))
            {
                makeBtn2();
            }
            else if (PlayerPrefs.HasKey("learning-2") && !PlayerPrefs.HasKey("learned-1"))
            {
                makeBtn2();
            }

            if (PlayerPrefs.HasKey("learned-1"))
            {
                Cw1btn.SetActive(false);
            }
            
            if (PlayerPrefs.HasKey("learned-2"))
            {
                Cw2btn.SetActive(false);
            }
            
            // cw3
            if (PlayerPrefs.HasKey("learning-3") && PlayerPrefs.HasKey("learned-2"))
            {
                makeBtn3();
            }
            else if (PlayerPrefs.HasKey("learning-3") && !PlayerPrefs.HasKey("learned-2"))
            {
                makeBtn3();
            }

            if (PlayerPrefs.HasKey("learned-1"))
            {
                Cw1btn.SetActive(false);
            }
            
            if (PlayerPrefs.HasKey("learned-2"))
            {
                Cw2btn.SetActive(false);
            }
            
            if (PlayerPrefs.HasKey("learned-3"))
            {
                Cw3btn.SetActive(false);
            }
            
        }
    }

    // make btns open
    public void makeBtn1()
    {
        Cw1btn.SetActive(true);
    }
    
    public void makeBtn2()
    {
        Cw2btn.SetActive(true);
    }
    
    public void makeBtn3()
    {
        Cw3btn.SetActive(true);
    }
    
    // make cws open, close cws
    public void openCW1()
    {
        Cw1.SetActive(true);
    }
    
    public void closeCW1()
    {
        Cw1.SetActive(false);
    }
    
    public void openCW2()
    {
        Cw2.SetActive(true);
    }
    
    public void closeCW2()
    {
        Cw2.SetActive(false);
    }
    
    public void openCW3()
    {
        Cw3.SetActive(true);
    }
    
    public void closeCW3()
    {
        Cw3.SetActive(false);
    }
    
    // load cw if meet requirements
    public void loadCW()
    {
        // year 1:
        if (PlayerPrefs.GetInt("year",1) == 1)
        {
            SceneManager.LoadScene("1.2");
        }
        
        // year 2:
        if (PlayerPrefs.GetInt("year",1) == 2)
        {
            if (!PlayerPrefs.HasKey("learning-1"))
            {
                PlayerPrefs.SetInt("learning-1", 1);
                SceneManager.LoadScene("2.2");
            }
            else if (!PlayerPrefs.HasKey("learning-2") && PlayerPrefs.HasKey("learned-1") )
            {
                PlayerPrefs.SetInt("learning-2", 1);
                SceneManager.LoadScene("2.3");
            }

            if (PlayerPrefs.GetInt("learning-1") == 1 && !PlayerPrefs.HasKey("learned-2"))
            {
                SceneManager.LoadScene("2.2");
            }
            else if (PlayerPrefs.GetInt("learning-2") == 1)
            {
                SceneManager.LoadScene("2.3");
            }
        }
        // year 3:
        if (PlayerPrefs.GetInt("year",1) == 3)
        {
            if (!PlayerPrefs.HasKey("learning-1") && !PlayerPrefs.HasKey("learned-2")  && !PlayerPrefs.HasKey("learned-3"))
            {
                PlayerPrefs.SetInt("learning-1", 1);
                SceneManager.LoadScene("3.2");
            }
            else if (!PlayerPrefs.HasKey("learning-2") && PlayerPrefs.HasKey("learned-1") && !PlayerPrefs.HasKey("learned-3"))
            {
                PlayerPrefs.SetInt("learning-2", 1);
                SceneManager.LoadScene("3.3");
            }
            else if (!PlayerPrefs.HasKey("learning-3") && PlayerPrefs.HasKey("learned-2") )
            {
                PlayerPrefs.SetInt("learning-3", 1);
                SceneManager.LoadScene("3.4");
            }

            if (PlayerPrefs.GetInt("learning-1") == 1 && !PlayerPrefs.HasKey("learned-2") && !PlayerPrefs.HasKey("learned-3") && !PlayerPrefs.HasKey("learned-1"))
            {
                SceneManager.LoadScene("3.2");
            }
            else if (PlayerPrefs.GetInt("learning-2") == 1 && !PlayerPrefs.HasKey("learned-3") && !PlayerPrefs.HasKey("learned-2"))
            {
                SceneManager.LoadScene("3.3");
            }
            else if (PlayerPrefs.GetInt("learning-3") == 1)
            {
                SceneManager.LoadScene("3.4");
            }
        }
        
    }

    // additional functions for tester easy to access different levels.
    public void Year2()
    {
        MajorGameManager.Instance.GoSecondYear();
    }
    
    public void Year3()
    {
        MajorGameManager.Instance.GoThirdyear();
    }

    public void reStart()
    {
        MajorGameManager.Instance.RetakeYear();
    }
    
}
