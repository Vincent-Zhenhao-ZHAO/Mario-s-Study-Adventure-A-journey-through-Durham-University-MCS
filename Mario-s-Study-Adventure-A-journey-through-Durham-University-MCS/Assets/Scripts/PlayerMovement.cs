using System;
using UnityEngine;

// player movement
// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    private Vector2 _velocity;
    private float _inputAxis;
    
    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float Gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f),2);

    public bool OnTheGround { get; private set; }
    public bool Jump { get; private set; }
    public bool Running => Mathf.Abs(_velocity.x) > 0.15f || Mathf.Abs(_inputAxis) > 0.15f ;
    public bool Slide => (_inputAxis > 0.25f && _velocity.x < 0.25f) || (_inputAxis < 0.25f && _velocity.x > 0.25f);

    private AudioSource _jumpAudioSource;

    private void Start()
    {
        _jumpAudioSource = GetComponent<AudioSource>();
    }

    // Get the player
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizonMovement();
        
        // check if the player on the ground
        OnTheGround = _rigidbody.Raycast(Vector2.down);

        // if on the ground, player then follow rules on the ground
        if (OnTheGround)
        {
            GroundMovement();
        }
        // apply gravity depends on falling or not.
        ApplyGravity();
    }

    // Horizon move
    private void HorizonMovement()
    {
        // check which way user pressed
        _inputAxis = Input.GetAxis("Horizontal");
        // calculate horizon velocity, and make sure it has been updated over time
        _velocity.x = Mathf.MoveTowards(_velocity.x,_inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        // check if has collision with other object, set it to 0 if player hit another object
        if (_rigidbody.Raycast(Vector2.right * _velocity.x))
        {
            _velocity.x = 0f;
        }
        
        // if velocity is positive, then keep going right
        if (_velocity.x >= 0f)
        {
               transform.eulerAngles = Vector3.zero;
               
        }
        // if not, set object rotation to 180 degrees
        else if (_velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

    }
    
    // Ground movement rule
    private void GroundMovement()
    {
        // make sure the vertical speed not below 0 when on the ground
        _velocity.y = Mathf.Max(_velocity.y, 0f); 
        
        // check if jump or not
        Jump = _velocity.y > 0f;
        
        // if press button jump, then set vertical speed as jump force
        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = JumpForce;
            Jump = true;
            _jumpAudioSource.Play();
        }
    }

    // apply gravity -> physics
    private void ApplyGravity()
    {
        // to improve user experience, player has different gravity when jumping and falling.
        bool falling = _velocity.y < 0f || Input.GetButton("Jump");
        float gravityFactor = falling ? 2f : 1.5f;
        
        _velocity.y += Gravity * Time.deltaTime * gravityFactor;
        _velocity.y = Math.Max(_velocity.y, Gravity / 2f);
    }

    // move player position to the correct one
    private void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        position += _velocity * Time.fixedDeltaTime;
        
        _rigidbody.MovePosition(position);
    }

    // happens when player meets enemy or blocks
    // if player kill enemy -> jump on the enemy -> player get half jump force -> improve experience
    // if player hit blocks -> player vertical velocity became to 0.
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(col.transform,Vector2.down))
            {
                _velocity.y = JumpForce / 2f;
                Jump = true;
            }
        }
        
        if (col.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(col.transform, Vector2.up))
            {
                _velocity.y = 0f;
            }
        }
    }
    
    
}
