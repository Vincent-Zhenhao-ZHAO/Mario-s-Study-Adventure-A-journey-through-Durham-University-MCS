using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCS : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("year",1) == 1)
            {
                if (PlayerPrefs.HasKey("learned-1"))
                {
                    col.gameObject.SetActive(false);
                    MajorGameManager.Instance.GoSecondYear();
                }
            }
            else if (PlayerPrefs.GetInt("year") == 2)
            {
                if (PlayerPrefs.HasKey("learned-2"))
                {
                    col.gameObject.SetActive(false);
                    MajorGameManager.Instance.GoThirdyear();
                }
                
            }
            else if (PlayerPrefs.GetInt("year") == 3)
            {
                if (PlayerPrefs.HasKey("learned-3"))
                {
                    col.gameObject.SetActive(false);
                    MajorGameManager.Instance.FinishThird();
                }
            }
        }
    }
}
