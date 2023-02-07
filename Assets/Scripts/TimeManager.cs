using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private float _currentTime;
    public int timePassed;
    public float timeStartCW;

    public TMP_Text timeCounter;

    private void Start()
    {
        if (PlayerPrefs.HasKey("time"))
        {
            _currentTime = PlayerPrefs.GetFloat("time");
        }
        else
        {
            if (PlayerPrefs.GetInt("year",1) == 1)
            {
                _currentTime = 200f;
                PlayerPrefs.SetFloat("time",_currentTime);
            }
            else if (PlayerPrefs.GetInt("year",1) == 2)
            {
                _currentTime = 350f;
                PlayerPrefs.SetFloat("time",_currentTime);
            }
            else if (PlayerPrefs.GetInt("year",1) == 3)
            {
                _currentTime = 400f;
                PlayerPrefs.SetFloat("time",_currentTime);
            }
            
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("startCW") == 1)
        {
            timeStartCW = _currentTime;
            PlayerPrefs.SetFloat("timeStartCW", timeStartCW);
        }
        
        _currentTime -= 1 * Time.deltaTime;
        timeCounter.text = "Time: " + _currentTime.ToString("0");
        
        if (PlayerPrefs.GetInt("startCW") == 0)
        {
            timeStartCW = PlayerPrefs.GetFloat("timeStartCW");
            timePassed = Mathf.RoundToInt((timeStartCW - _currentTime) / 5);
            PlayerPrefs.SetInt("timePassed", timePassed);
        }

        if (PlayerPrefs.HasKey("time"))
        {
            PlayerPrefs.SetFloat("time",_currentTime);
        }
        
        if (_currentTime < 0)
        {
            MajorGameManager.Instance.TimePunishment();
        }
    }
}
