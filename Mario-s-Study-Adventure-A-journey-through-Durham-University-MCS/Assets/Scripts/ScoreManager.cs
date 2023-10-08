using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// MajorGame data manager
public class ScoreManager : MonoBehaviour
{
    public TMP_Text textCoin;
    
    public enum TypeCoin
    {
        Credict,
        Life,
        Module
    }

    public TypeCoin type;
    
    // Get the real-time value from the PlayerPrefs.
    void Start()
    {
        switch (type)
        {
            case TypeCoin.Credict:
                textCoin.text = "Credits: " + PlayerPrefs.GetInt("credits",0).ToString();
                break;
            case TypeCoin.Life:
                textCoin.text = "Lives: " + PlayerPrefs.GetInt("lives",3).ToString();
                break;
            case TypeCoin.Module:
                textCoin.text = "Modules: " + PlayerPrefs.GetInt("modules",0).ToString();
                break;
        }
    }
    
    
    // Update the text
    void Update()
    {
        switch (type)
        {
            case TypeCoin.Credict:
                textCoin.text = "Credits: " + PlayerPrefs.GetInt("credits",0).ToString();
                break;
            case TypeCoin.Life:
                textCoin.text = "Lives: " + PlayerPrefs.GetInt("lives",3).ToString();
                break;
            case TypeCoin.Module:
                textCoin.text = "Modules: " + PlayerPrefs.GetInt("modules", 0).ToString();
                break;
        }
    }
}
