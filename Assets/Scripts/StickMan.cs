using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 ToVector2(this Vector3 @this) => new Vector2(@this.x, @this.y);
    public static Vector3 ToVector3(this Vector2 @this) => new Vector3(@this.x, @this.y, 0);
}

public class StickMan : MonoBehaviour
{
    public float MovementSpeed = 9;
    public float MovementAcceleration = 75;
    public float MovementDeceleration = 70;
    public float JumpHeight = 4;

    private BoxCollider2D _collider;
    private bool _isGrounded = false;

    private Vector2 _velocity = new Vector2();

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
            _velocity.x = Mathf.MoveTowards(_velocity.x, MovementSpeed * moveInput, MovementAcceleration * Time.deltaTime);
        else
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, MovementDeceleration * Time.deltaTime);

        if (_isGrounded)
        {
            _velocity.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                _velocity.y = Mathf.Sqrt(2 * JumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }
        _velocity.y += Physics2D.gravity.y * Time.deltaTime;

        transform.Translate(_velocity.ToVector3() * Time.deltaTime);

        var colliderSize = _collider.size * transform.localScale;

        var hits = Physics2D.OverlapBoxAll(transform.position.ToVector2(), colliderSize, 0);

        _isGrounded = false;
        foreach (var hit in hits)
        {
            if (hit == _collider)
                continue;

            var colliderDistance = hit.Distance(_collider);

            if (colliderDistance.isOverlapped)
            {
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && _velocity.y < 0)
                    _isGrounded = true;
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
        }
    }
}
