using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when player collects credits
// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Credict,
    }

    public Type type;

    // when the credict meet player -> player collect credit
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Collect(col.gameObject);
        }
    }

    // player's credits plus one
    // and then the credit disappear
    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Credict:
                MajorGameManager.Instance.GainExtraCredits();
                break;
        }
        
        Destroy(gameObject);
    }
}
