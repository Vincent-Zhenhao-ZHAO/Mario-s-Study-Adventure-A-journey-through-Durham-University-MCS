using UnityEngine;

public class PlayerBodyChange : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Animated run;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponentInParent<PlayerMovement>();
    }
    
    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = _movement.Running;
        
        if (_movement.Jump)
        {
            _spriteRenderer.sprite = jump;
        } else if (_movement.Slide)
        {
            _spriteRenderer.sprite = slide;
        } else if (!_movement.Running)
        {
            _spriteRenderer.sprite = idle;
        }
    }
}
