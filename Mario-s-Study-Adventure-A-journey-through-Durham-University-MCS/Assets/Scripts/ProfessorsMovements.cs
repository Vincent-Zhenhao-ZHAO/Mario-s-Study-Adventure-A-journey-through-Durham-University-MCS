using UnityEngine;


// Enemy movements -> this object was first for professors, but changed the object in designing process.
// code was base on: https://github.com/zigurous/unity-super-mario-tutorial
public class ProfessorsMovements : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    private Vector2 _velocity;

    // First set the enemy as not moveable
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    // make visiable function
    private void OnBecameVisible()
    {
        enabled = true;
    }

    // make invisionable function
    private void OnBecameInvisible()
    {
        enabled = false;
    }

    // enable to enemy
    private void OnEnable()
    {
        _rigidbody2D.WakeUp();
    }

    // disable the enemy
    private void OnDisable()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.Sleep();
    }

    // enemy movement
    // the enemy will move left first and then meet object and move right.
    private void FixedUpdate()
    {
        _velocity.x = direction.x * speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        
        _rigidbody2D.MovePosition(_rigidbody2D.position + _velocity * Time.fixedDeltaTime);

        if (_rigidbody2D.Raycast(direction))
        {
            direction = -direction;
        }

        if (_rigidbody2D.Raycast(Vector2.down))
        {
            _velocity.y = Mathf.Max(_velocity.y, 0f);
        }
    }
}
