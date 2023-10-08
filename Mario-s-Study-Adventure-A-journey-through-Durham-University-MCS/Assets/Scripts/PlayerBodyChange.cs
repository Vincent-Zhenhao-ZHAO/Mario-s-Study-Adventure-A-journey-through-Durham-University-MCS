using UnityEngine;


// this code was made for mario body change, but modified to animnation
// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class PlayerBodyChange : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Animated run;

    // get the component
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponentInParent<PlayerMovement>();
    }
    
    // helper function to enable the SpriteRenderer
    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    // helper function to disable the SpriteRenderer
    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    // set jump, running and slide animnations when has its movements
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
