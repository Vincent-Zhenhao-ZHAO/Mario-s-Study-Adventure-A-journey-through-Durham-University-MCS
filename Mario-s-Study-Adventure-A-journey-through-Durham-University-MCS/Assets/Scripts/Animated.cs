using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class Animated : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer _spriteRenderer;
    private int _frame;

    // get the SpriteRenderer
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // helper function to able to animate an make it move in framerate
    private void OnEnable()
    {
        InvokeRepeating( nameof(Animate), framerate, framerate);
    }

    // disable the animation
    private void OnDisable()
    {
        CancelInvoke();
    }

    // make animation in limited frame -> for example run can be done in 4 images and within a loop
    private void Animate()
    {
        _frame++;
        if (_frame >= sprites.Length)
        {
            _frame = 0;
        }

        if (_frame >= 0 && _frame < sprites.Length)
        {
            _spriteRenderer.sprite = sprites[_frame];
        }
        
    }
}
