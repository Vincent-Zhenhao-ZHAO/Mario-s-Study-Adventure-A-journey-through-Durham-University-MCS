using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MCS manager
public class MCS : MonoBehaviour
{
    // when player finished all courseworks and reach the MCS entrance in time limit, will load to next level.
    private void OnTriggerEnter2D(Collider2D col)
    {
        // if the MCS entrance meet Player.
        if (col.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("year",1) == 1)
            {
                if (PlayerPrefs.HasKey("learned-1") && PlayerPrefs.GetFloat("time") >= 0)
                {
                    col.gameObject.SetActive(false);
                    MajorGameManager.Instance.GoSecondYear();
                }
            }
            else if (PlayerPrefs.GetInt("year") == 2 && PlayerPrefs.GetFloat("time") >= 0)
            {
                if (PlayerPrefs.HasKey("learned-2"))
                {
                    col.gameObject.SetActive(false);
                    MajorGameManager.Instance.GoThirdyear();
                }
                
            }
            else if (PlayerPrefs.GetInt("year") == 3 && PlayerPrefs.GetFloat("time") >= 0)
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
