using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy movement
// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class Goomba : MonoBehaviour
{
    public Sprite FlatSprite;
    public AudioClip HitSound;
    public AudioClip BeatSound;
    private AudioSource _hitAudioSource;
    private AudioSource _beatAudioSource;

    // get the audio source
    private void Start()
    {
        _hitAudioSource = GetComponent<AudioSource>();
        _hitAudioSource.clip = HitSound;

        _beatAudioSource = GetComponent<AudioSource>();
        _beatAudioSource.clip = BeatSound;
    }

    // if the enemy hit the player:
    // 1. player jump on the top -> enemy dies and make its small
    // 2. player hit by enemy, lose one credit
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.transform.DotTest(transform,Vector2.down))
            {
                _beatAudioSource.volume = 0.1f;
                _beatAudioSource.Play();
                Flatten();
            }
            else
            {
                _beatAudioSource.volume = 0.1f;
                _hitAudioSource.Play();
                MajorGameManager.Instance.Punishment();
                Destroy(gameObject);
            }
        }
    }
    
    // enemy get small when be beaten.
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<ProfessorsMovements>().enabled = false;
        GetComponent<Animated>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = FlatSprite;
        Destroy(gameObject,0.7f);
    }
}
