using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animated : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer _spriteRenderer;
    private int _frame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnEnable()
    {
        InvokeRepeating( nameof(Animate), framerate, framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

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
