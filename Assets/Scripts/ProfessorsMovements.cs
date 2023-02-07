using UnityEngine;

public class ProfessorsMovements : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    private Vector2 _velocity;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        _rigidbody2D.WakeUp();
    }

    private void OnDisable()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.Sleep();
    }

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
